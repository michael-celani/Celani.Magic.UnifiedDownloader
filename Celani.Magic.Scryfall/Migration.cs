using System.Text.Json.Serialization;

namespace Celani.Magic.Scryfall;

public class Migration
{
    [JsonPropertyName("performed_at")]
    public DateOnly PerformedAt { get; set; }

    [JsonPropertyName("migration_strategy")]
    public MigrationStrategy MigrationStrategy { get; set; }

    [JsonPropertyName("old_scryfall_id")]
    public string OldScryfallId { get; set; } = "";

    [JsonPropertyName("new_scryfall_id")]
    public string? NewScryfallId { get; set; }

    [JsonPropertyName("metadata")]
    public MigrationMetadata? Metadata { get; set; }
}