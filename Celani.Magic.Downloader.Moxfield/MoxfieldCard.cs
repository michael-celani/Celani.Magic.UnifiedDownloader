using System.Text.Json.Serialization;

namespace Celani.Magic.Downloader.Moxfield;

public class MoxfieldCard
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("uniqueCardId")]
    public string? UniqueCardId { get; set; }

    [JsonPropertyName("scryfall_id")]
    public required string ScryfallId { get; set; }

    [JsonPropertyName("color_identity")]
    public List<string>? ColorIdentity { get; set; }

    [JsonPropertyName("oracle_text")]
    public string? OracleText { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}