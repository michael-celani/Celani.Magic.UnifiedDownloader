namespace Celani.Magic.Downloader.Storage;

public record ScryfallMetadata
{
    public int Id { get; set; }

    /// <summary>
    /// When this was last updated.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Where this was downloaded from.
    /// </summary>
    public Uri DownloadUri { get; set; } = default!;
}
