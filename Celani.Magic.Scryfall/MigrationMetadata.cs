using System.Text.Json.Serialization;

namespace Celani.Magic.Scryfall;

public class MigrationMetadata
{
    [JsonPropertyName("oracle_id")]
    public string? OracleId { get; set; }
}