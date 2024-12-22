using Celani.Magic.Downloader.Core;
using Microsoft.Extensions.Logging;

namespace Celani.Magic.Downloader.Archidekt;

public class ArchidektMagicDownloader(IArchidektApi archidekt, ILogger<ArchidektMagicDownloader> logger) : IMagicDownloader
{
    public string Backend => "Archidekt";

    private IArchidektApi Archidekt { get; } = archidekt;

    private ILogger<ArchidektMagicDownloader> Logger { get; } = logger;

    public async Task<DownloadedMagicList> DownloadDeckAsync(string id)
    {
        // Download the deck from Archidekt.
        var deckResult = await Archidekt.GetDeckAsync(id);

        // Get all the category names considered included:
        var inCategories = deckResult.Categories.Where(x => x.IncludedInDeck).Select(x => x.Name).ToHashSet();

        // For non-Companions, group them by category:
        var cardLookup = deckResult.Cards.Select(card => 
            (card, cat: GroupCategories(deckResult, inCategories, card))
        ).ToLookup(tup => tup.cat, tup => tup.card);

        return new DownloadedMagicList
        {
            Id = id,
            Name = deckResult.Name,
            Source = Backend,

            Mainboard = cardLookup["mainboard"].Select(card => card.ToInclude()).ToList(),
            Sideboard = cardLookup["sideboard"].Select(card => card.ToInclude()).ToList(),
            Maybeboard = cardLookup["maybeboard"].Select(card => card.ToInclude()).ToList(),
            Commanders = cardLookup["commanders"].Select(card => card.ToInclude()).ToList(),
            Companions = cardLookup["companions"].Select(card => card.ToInclude()).ToList(),

            // These are less common:
            Attractions = cardLookup["attractions"].Select(card => card.ToInclude()).ToList(),
            Contraptions = cardLookup["contraptions"].Select(card => card.ToInclude()).ToList(),
            Planes = cardLookup["planes"].Select(card => card.ToInclude()).ToList(),
            Schemes = cardLookup["schemes"].Select(card => card.ToInclude()).ToList(),
            SignatureSpells = cardLookup["signatureSpells"].Select(card => card.ToInclude()).ToList(),
            Stickers = cardLookup["stickers"].Select(card => card.ToInclude()).ToList(),
            Tokens = cardLookup["tokens"].Select(card => card.ToInclude()).ToList(),
        };
    }

    private static string GroupCategories(ArchidektDeck deck, HashSet<string> inCategories, ArchidektCard card)
    {
        var mainCategory = card.Categories.First();
        
        // Assume the maybeboard is a true maybeboard:
        if (mainCategory == "Maybeboard") return "maybeboard";

        // If the card is marked as a companion, it's a companion:
        if (card.Companion) return "companions";

        var types = card.Card.OracleCard.Types.ToHashSet();
        var subTypes = card.Card.OracleCard.SubTypes.ToHashSet();
        
        if (mainCategory == "Commander")
        {
            // Commanders may include signature spells:
            if (deck.DeckFormat is FormatType.Oathbreaker && !types.Contains("Planeswalker")) return "signatureSpells";

            return "commanders";
        }

        if (subTypes.Contains("Attraction")) return "attractions";
        if (subTypes.Contains("Contraption")) return "contraptions";
        if (types.Contains("Plane")) return "planes";
        if (types.Contains("Scheme")) return "schemes";
        if (types.Contains("Stickers")) return "stickers";
        if (types.Contains("Token")) return "tokens";
        if (mainCategory == "Sideboard") return "sideboard";

        if (inCategories.Contains(mainCategory)) return "mainboard";
        return "maybeboard";
    }
}