using System.Text.Json.Serialization;

namespace Celani.Magic.Scryfall;

public class BulkData
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("type")]
    public required BulkDataType DataType { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTimeOffset UpdatedAt { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("size")]
    public required long Size { get; set; }

    [JsonPropertyName("download_uri")]
    public required Uri DownloadUri { get; set; }

    [JsonPropertyName("content_type")]
    public required string ContentType { get; set; }

    [JsonPropertyName("content_encoding")]
    public required string ContentEncoding { get; set; }
}
