using Microsoft.EntityFrameworkCore;

namespace Celani.Magic.Downloader.Storage;

[Index(nameof(ScryfallId), IsUnique = true)]
public class ScryfallCard
{
    /// <summary>
    /// The ID for the given card.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The Scryfall ID of the card.
    /// </summary>
    public required string ScryfallId { get; set; }

    /// <summary>
    /// The corresponding Oracle card.
    /// </summary>
    public OracleCard? OracleCard { get; set; }
}
