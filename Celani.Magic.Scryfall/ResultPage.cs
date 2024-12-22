using System.Text.Json.Serialization;

namespace Celani.Magic.Scryfall;

public class ResultPage<T>
{
    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }

    [JsonPropertyName("next_page")]
    public Uri? NextPage { get; set; }

    [JsonPropertyName("data")]
    public List<T> Data { get; set; } = [];
}
