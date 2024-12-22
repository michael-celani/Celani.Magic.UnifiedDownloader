using System.Text.Json.Serialization;

namespace Celani.Magic.UnifiedDownloader.Api;

public class SearchResultFailure
{
    [JsonPropertyName("messages")]
    public required string[] Messages { get; set; }
}
