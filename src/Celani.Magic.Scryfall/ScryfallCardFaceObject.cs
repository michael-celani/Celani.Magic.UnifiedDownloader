using System.Text.Json.Serialization;

namespace Celani.Magic.Scryfall;

public class ScryfallCardFaceObject
{
    [JsonPropertyName("type_line")]
    public string? TypeLine { get; set; }

    [JsonPropertyName("oracle_id")]
    public string? OracleId { get; set; }

    [JsonPropertyName("mana_cost")]
    public string? ManaCost { get; set; }

    [JsonPropertyName("oracle_text")]
    public string? OracleText { get; set; }
}
