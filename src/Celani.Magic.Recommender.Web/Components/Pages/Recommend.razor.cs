using System.Diagnostics;
using Celani.Magic.Downloader.Storage;
using Celani.Magic.ML;
using Celani.Magic.Recommender.Web.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ML;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace Celani.Magic.Recommender.Web.Components.Pages;

public record RecommendResult
{
    public required OracleCard Card { get; set; }

    public int Inclusions { get; set; }

    public float Score { get; set; }
}

public record CommandResult
{
    public required MagicCommander Commander { get; set; }

    public float Score { get; set; }
}

public partial class Recommend(
    IDbContextFactory<MagicContext> magicDb, 
    PredictionEnginePool<RecommenderCard, RecommenderCardPrediction> predictionEngine)
{
    private Dictionary<CardType, List<RecommendResult>>? cards;

    private List<RecommendResult>? hotTakes;

    private List<RecommendResult>? topCards;

    private StoredDeck? myDeck;

    private int commanderCount;

    private bool notFound;

    [Parameter]
    public string? Source {get; set;}

    [Parameter]
    public string? DeckId { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;

        using var magicContext = magicDb.CreateDbContext();

        myDeck = await magicContext.Decks
            .Where(x => x.Source == Source && x.SourceId == DeckId)
            .Include(x => x.MagicCommander)
                .ThenInclude(x => x.Commander)
            .Include(x => x.MagicCommander)
                .ThenInclude(x => x.Partner)
            .Include(x => x.Cards)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (myDeck is null) {
            notFound = true;
            StateHasChanged();
            return;
        }

        commanderCount = await magicContext.Decks.Where(deck => deck.MagicCommanderId == myDeck.MagicCommanderId).CountAsync();

        Dictionary<CardType, PriorityQueue<OracleCard, float>> priorities = [];
        PriorityQueue<OracleCard, float> topCardsQueue = new();
        Dictionary<int, (OracleCard, float)> lookup = [];

        // Create a priority queue for the top cards.
        foreach (var value in Enum.GetValues<CardType>())
        {
            priorities.Add(value, new());
        }

        // Make a list of the cards that the user already owns.
        var owned = myDeck.Cards!.Select(x => x.Id).ToHashSet();

        var validCards = await magicContext.OracleCards.Where(card => 
            !owned.Contains(card.Id) && 
            card.ColorIdentity == (card.ColorIdentity & myDeck.MagicCommander.ColorIdentity))
        .AsNoTracking()
        .ToArrayAsync();

        var watch = Stopwatch.StartNew();

        var dataView = new MLContext().Data.LoadFromEnumerable(validCards.Select(card => new RecommenderCard
        {
            DeckId = (uint) myDeck.Id,
            CardId = (uint) card.Id
        }));

        var predictions = predictionEngine.GetModel().Transform(dataView).GetColumn<float>("Score");

        var oracleCounts = await magicContext.CommanderCardCounts
            .Where(cardCount => cardCount.CommanderId == myDeck.MagicCommanderId)
            .AsNoTracking()
            .ToDictionaryAsync(cardCount => cardCount.CardId, cardCount => cardCount.Count);

        PriorityQueue<OracleCard, float> hottakeQueue = new();
        Dictionary<int, int> hottakeCounts = [];

        foreach (var (card, score) in validCards.Zip(predictions))
        {
            if (float.IsNaN(score)) continue;

            // Add the card to the priority queue.
            var cardType = CardTypeTools.GetHighestPriorityCardType(card.CardTypes);
            priorities[cardType].Enqueue(card, -score);
            topCardsQueue.Enqueue(card, -score);
            lookup[card.Id] = (card, -score);

            if (!oracleCounts.TryGetValue(card.Id, out var count)) count = 0;

            if (count < commanderCount / 10)
            {
                hottakeQueue.Enqueue(card, -score);
                hottakeCounts[card.Id] = count;
            }
        }

        watch.Stop();

        // hot takes
        hotTakes = hottakeQueue.DequeueN(20).Select(x => new RecommendResult
        {
            Card = x.Item1,
            Score = -x.Item2,
            Inclusions = oracleCounts.TryGetValue(x.Item1.Id, out var count) ? count : 0
        }).ToList();

        // top cards
        topCards = topCardsQueue.DequeueN(10).Select(x => new RecommendResult
        {
            Card = x.Item1,
            Score = -x.Item2,
            Inclusions = oracleCounts.TryGetValue(x.Item1.Id, out var count) ? count : 0
        }).ToList();

        // categories
        cards = Enum.GetValues<CardType>().ToDictionary(x => x, _ => new List<RecommendResult>());

        foreach (var key in Enum.GetValues<CardType>())
        {
            foreach (var (card, score) in priorities[key].DequeueN(10))
            {
                var res = new RecommendResult
                {
                    Card = card,
                    Score = -score,
                    Inclusions = oracleCounts.TryGetValue(card.Id, out var count) ? count : 0
                };

                cards[key].Add(res);
            }
        }

        StateHasChanged();
    }
}