using System.Text.Json.Serialization;

namespace Celani.Magic.Scryfall;

public class ScryfallCardObject
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("oracle_id")]
    public string? OracleId { get; set; }

    [JsonPropertyName("type_line")]
    public string? TypeLine { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("color_identity")]
    public List<string>? ColorIdentity { get; set; }

    [JsonPropertyName("card_faces")]
    public List<ScryfallCardFaceObject>? CardFaces { get; set; }

    [JsonPropertyName("layout")]
    public required CardLayout Layout { get; set; }

    [JsonIgnore]
    public string? CompositeTypeLine => TypeLine ?? CardFaces?.FirstOrDefault()?.TypeLine;

    [JsonIgnore]
    public string? CompositeOracleId => OracleId ?? CardFaces?.FirstOrDefault()?.OracleId;
}
