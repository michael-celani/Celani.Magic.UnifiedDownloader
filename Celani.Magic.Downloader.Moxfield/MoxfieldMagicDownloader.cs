using Celani.Magic.Downloader.Core;
using Celani.Magic.Downloader.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Celani.Magic.Downloader.Moxfield;

public class MoxfieldMagicDownloader(IMoxfieldApi moxfield, IDbContextFactory<MagicContext> scryfallContextFactory, ILogger<MoxfieldMagicDownloader> logger) : IMagicDownloader
{
    public string Backend => "moxfield";

    private IMoxfieldApi Moxfield { get; } = moxfield;

    private IDbContextFactory<MagicContext> ScryfallContextFactory { get; } = scryfallContextFactory;

    private ILogger<MoxfieldMagicDownloader> Logger { get; } = logger;

    public async Task<DownloadedMagicList> DownloadDeckAsync(string id)
    {
        using var scryfallContext = await ScryfallContextFactory.CreateDbContextAsync();

        var deckResult = await Moxfield.GetDeckAsync(id);

        return new DownloadedMagicList
        {
            Id = id,
            Name = deckResult.Name,
            Source = Backend,
            Mainboard = ToIncludeList(deckResult.Boards.Mainboard, scryfallContext),
            Sideboard = ToIncludeList(deckResult.Boards.Sideboard, scryfallContext),
            Maybeboard = ToIncludeList(deckResult.Boards.Maybeboard, scryfallContext),
            Commanders = ToIncludeList(deckResult.Boards.Commanders, scryfallContext),
            Companions = ToIncludeList(deckResult.Boards.Companions, scryfallContext),
            Attractions = ToIncludeList(deckResult.Boards.Attractions, scryfallContext),
            Contraptions = ToIncludeList(deckResult.Boards.Contraptions, scryfallContext),
            Planes = ToIncludeList(deckResult.Boards.Planes, scryfallContext),
            Schemes = ToIncludeList(deckResult.Boards.Schemes, scryfallContext),
            SignatureSpells = ToIncludeList(deckResult.Boards.SignatureSpells, scryfallContext),
            Stickers = ToIncludeList(deckResult.Boards.Stickers, scryfallContext),
            Tokens = ToIncludeList(deckResult.Boards.Tokens, scryfallContext),
        };
    }

    private static List<DownloadedMagicInclude> ToIncludeList(MoxfieldDeckBoard? board, MagicContext scryfallContext)
    {
        if (board is null) return [];

        var scryIds = board.Cards.Values
            .Select(x => x.Card.ScryfallId)
            .ToHashSet();

        var dictionary = (from card in scryfallContext.ScryfallCards
                  where scryIds.Contains(card.ScryfallId)
                  select card)
                 .Include(card => card.OracleCard)
                 .ToDictionary(card => card.ScryfallId);

        if (dictionary.Count != scryIds.Count)
        {
            throw new InvalidOperationException("Not all cards were found.");
        }

        return board.Cards.Values.Select(x =>
            {
                var oracleCard = dictionary[x.Card.ScryfallId].OracleCard!;

                return new DownloadedMagicInclude
                {
                    Quantity = x.Quantity,
                    ScryfallId = x.Card.ScryfallId,
                    ScryfallOracleId = oracleCard.OracleId
                };
            }
        ).ToList();
    }
}
