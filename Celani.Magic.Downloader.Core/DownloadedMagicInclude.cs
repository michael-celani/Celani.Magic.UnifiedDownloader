namespace Celani.Magic.Downloader.Core;

public record DownloadedMagicInclude
{
    /// <summary>
    /// The quantity of the included card.
    /// </summary>
    public required int Quantity { get; set; }

    /// <summary>
    /// The Scryfall ID of the included card.
    /// </summary>
    public required string ScryfallId { get; set; }

    /// <summary>
    /// The Oracle ID of the included card.
    /// </summary>
    public required string ScryfallOracleId { get; set; }
}
