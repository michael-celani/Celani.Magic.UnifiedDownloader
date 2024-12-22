using System.Text.Json.Serialization;

namespace Celani.Magic.Scryfall;

public class ScryfallCardFaceObject
{
    [JsonPropertyName("type_line")]
    public string? TypeLine { get; set; }

    [JsonPropertyName("oracle_id")]
    public string? OracleId { get; set; }
}
