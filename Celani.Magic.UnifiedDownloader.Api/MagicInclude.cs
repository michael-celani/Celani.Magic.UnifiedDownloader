using System.Text.Json.Serialization;

namespace Celani.Magic.UnifiedDownloader.Api;

public record MagicInclude
{
    /// <summary>
    /// The quantity of the included card.
    /// </summary>
    [JsonPropertyName("quantity")]
    public required int Quantity { get; set; }
    
    /// <summary>
    /// The Scryfall ID of the included card.
    /// </summary>
    [JsonPropertyName("scryfall_id")]
    public required string ScryfallId { get; set; }

    /// <summary>
    /// The Oracle ID of the included card.
    /// </summary>
    [JsonPropertyName("scryfall_oracle_id")]
    public required string ScryfallOracleId { get; set; }
}
