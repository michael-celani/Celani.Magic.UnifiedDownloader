using Microsoft.EntityFrameworkCore;

namespace Celani.Magic.Downloader.Storage;

[Index(nameof(Source), nameof(SourceId))]
[Index(nameof(MagicCommanderId))]
public record StoredDeck
{
    public int Id { get; set; }

    /// <summary>
    /// The source of the deck.
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// The ID of the deck at its source.
    /// </summary>
    public string? SourceId { get; set; }

    /// <summary>
    /// The ID of the corresponding commander set.
    /// </summary>
    public int MagicCommanderId { get; set; }

    /// <summary>
    /// The corresponding commander set.
    /// </summary>
    public MagicCommander MagicCommander { get; set; } = null!;

    public List<OracleCardStoredDeck>? OracleCardStoredDecks { get; set; }

    /// <summary>
    /// The cards included in this deck.
    /// </summary>
    public List<OracleCard>? Cards { get; set; }
}