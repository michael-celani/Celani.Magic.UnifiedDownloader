using Microsoft.EntityFrameworkCore;

namespace Celani.Magic.Downloader.Storage;

public class MagicContext(DbContextOptions<MagicContext> options) : DbContext(options)
{
    /// <summary>
    /// Metadata about when cards were last updated.
    /// </summary>
    public DbSet<ScryfallMetadata> Metadata { get; set; } = default!;

    /// <summary>
    /// All scryfall cards.
    /// </summary>
    public DbSet<ScryfallCard> ScryfallCards { get; set; } = default!;

    /// <summary>
    /// All oracle cards.
    /// </summary>
    public DbSet<OracleCard> OracleCards { get; set; } = default!;

    /// <summary>
    /// All commander pairs.
    /// </summary>
    public DbSet<MagicCommander> Commanders { get; set; } = default!;

    /// <summary>
    /// Records of each downloaded deck.
    /// </summary>
    public DbSet<StoredDeck> Decks { get; set; } = default!;

    /// <summary>
    /// Card counts.
    /// </summary>
    public DbSet<CommanderCardCount> CommanderCardCounts { get; set; } = default!;

    /// <summary>
    /// A list of bindings between oracle cards and stored decks.
    /// </summary>
    public DbSet<OracleCardStoredDeck> OracleCardStoredDeck { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OracleCard>()
            .HasMany(e => e.StoredDecks)
            .WithMany(e => e.Cards)
            .UsingEntity<OracleCardStoredDeck>();
    }

    /// <summary>
    /// Look up a Magic commander by the commander and partner IDs.
    /// </summary>
    /// <param name="commanderId"></param>
    /// <param name="partnerId"></param>
    /// <returns>The magic commander.</returns>
    public async Task<MagicCommander> GetMagicCommanderAsync(string commanderId, string? partnerId)
    {
        MagicCommander? commander;

        if (partnerId is null)
        {
            commander = await Commanders.FirstOrDefaultAsync(c => c.Commander.OracleId == commanderId && c.Partner!.OracleId == null);
        }
        else
        {
            // Ensure that the commander and partner IDs are in order:
            (commanderId, partnerId) = commanderId.CompareTo(partnerId) < 0 ? (commanderId, partnerId) : (partnerId, commanderId);

            commander = await Commanders.FirstOrDefaultAsync(c => c.Commander.OracleId == commanderId && c.Partner!.OracleId == partnerId);
        }

        if (commander is null)
        {
            commander = new MagicCommander
            {
                Commander = await OracleCards.FirstAsync(c => c.OracleId == commanderId),
                Partner = partnerId is not null ? await OracleCards.FirstAsync(c => c.OracleId == partnerId) : null
            };
            Commanders.Add(commander);
            await SaveChangesAsync();
        }

        return commander;
    }

    public async Task UpdateCardCountsAsync()
    {
        await Database.ExecuteSqlAsync($"""
            INSERT INTO CommanderCardCounts (CommanderId, CardId, Count)
            SELECT "d"."MagicCommanderId" AS "CommanderId", "o"."CardsId" AS "CardId", COUNT(*) AS "Count"
            FROM "OracleCardStoredDeck" AS "o" 
            INNER JOIN "Decks" AS "d" ON "o"."StoredDecksId" = "d"."Id"
            GROUP BY "d"."MagicCommanderId", "o"."CardsId"
            ON CONFLICT (CommanderId, CardId) DO
            UPDATE SET Count = Count
        """);
    }
}
