using Celani.Magic.Downloader.Core;
using Celani.Magic.Downloader.Storage;
using Microsoft.EntityFrameworkCore;

namespace Celani.Magic.Ingestion.Console;

public static class MagicIngestor
{
    public static async Task InjestDeckAsync(MagicContext context, DownloadedMagicList downloadedDeck)
    {
        var commander = await HandleCommanderAsync(context, downloadedDeck);
        var deck = await HandleDeckAsync(context, commander, downloadedDeck);
    }

    private static async Task<StoredDeck> HandleDeckAsync(MagicContext context, MagicCommander commander, DownloadedMagicList deck)
    {
        // Does this deck exist already?
        var storedDeck = await context.Decks
            .FirstOrDefaultAsync(d => d.Source == deck.Source && d.SourceId == deck.Id);

        if (storedDeck is not null)
        {
            context.Decks.Remove(storedDeck);
        }

        var includes = deck.Mainboard.Select(x => x.ScryfallOracleId).ToHashSet();
        
        var cards = await context.OracleCards
            .Where(x => includes.Contains(x.OracleId))
            .ToListAsync();

        if (cards.Count != includes.Count)
        {
            throw new InvalidOperationException("Not all cards were found in the database.");
        }

        var newDeck = new StoredDeck
        {
            Source = deck.Source,
            SourceId = deck.Id,
            MagicCommander = commander,
            Cards = cards
        };

        context.Decks.Update(newDeck);

        return newDeck;
    }

    private static async Task<MagicCommander> HandleCommanderAsync(MagicContext context, DownloadedMagicList deck)
    {
        var commanders = deck.Commanders;

        // Ensure that there is at least one commander:
        if (commanders is null || commanders.Count == 0)
        {
            throw new InvalidOperationException("Deck must have at least one commander.");
        }

        // Ensure that there are at most two commanders:
        if (commanders.Count > 2)
        {
            throw new InvalidOperationException("Deck must have at most two commanders.");
        }

        // Get the oracle IDs:
        var commander = commanders[0];
        var partner = commanders.Count == 2 ? commanders[1] : null;

        return await context.GetMagicCommanderAsync(commander.ScryfallOracleId, partner?.ScryfallOracleId);
    }
}
