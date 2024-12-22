using System.Text.Json.Serialization;

namespace Celani.Magic.UnifiedDownloader.Api;

public class DownloadResultSuccess
{
    [JsonPropertyName("deck")]
    public required MagicDeck Deck { get; set; }
}
