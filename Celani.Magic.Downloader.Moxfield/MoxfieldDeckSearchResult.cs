using System.Text.Json.Serialization;

namespace Celani.Magic.Downloader.Moxfield;

public class MoxfieldDeckSearchResult
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("publicId")]
    public string PublicId { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}
