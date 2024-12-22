using System.Text.Json.Serialization;

namespace Celani.Magic.Downloader.Moxfield;

public class MoxfieldCardSearchResult
{
    [JsonPropertyName("hasMore")]
    public bool HasMore { get; set; }

    [JsonPropertyName("totalCards")]
    public int TotalCards { get; set; }

    [JsonPropertyName("data")]
    public List<MoxfieldCard> Cards { get; set; } = new();
}