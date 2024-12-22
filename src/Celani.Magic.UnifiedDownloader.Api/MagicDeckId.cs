using Refit;
using System.Text.Json.Serialization;

namespace Celani.Magic.UnifiedDownloader.Api;

public class MagicDeckId
{
    /// <summary>
    /// The deck ID.
    /// </summary>
    [AliasAs("id")]
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The backend.
    /// </summary>
    [AliasAs("backend")]
    [JsonPropertyName("backend")]
    public required WebsiteBackend Backend { get; set; }
}
