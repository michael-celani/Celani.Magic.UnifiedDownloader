using System.Text.Json.Serialization;

namespace Celani.Magic.UnifiedDownloader.Api;

/// <summary>
/// Identifies a Magic decklist.
/// </summary>
public record MagicDeck
{
    /// <summary>
    /// The deck ID.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The backend.
    /// </summary>
    [JsonPropertyName("backend")]
    public required WebsiteBackend Backend { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("mainboard")]
    public IList<MagicInclude>? Mainboard { get; set; }

    [JsonPropertyName("sideboard")]
    public IList<MagicInclude>? Sideboard { get; set; }

    [JsonPropertyName("maybeboard")]
    public IList<MagicInclude>? Maybeboard { get; set; }

    [JsonPropertyName("commanders")]
    public IList<MagicInclude>? Commanders { get; set; }

    [JsonPropertyName("companions")]
    public IList<MagicInclude>? Companions { get; set; }

    [JsonPropertyName("signature_spells")]
    public IList<MagicInclude>? SignatureSpells { get; set; }

    [JsonPropertyName("attractions")]
    public IList<MagicInclude>? Attractions { get; set; }

    [JsonPropertyName("stickers")]
    public IList<MagicInclude>? Stickers { get; set; }

    [JsonPropertyName("contraptions")]
    public IList<MagicInclude>? Contraptions { get; set; }

    [JsonPropertyName("planes")]
    public IList<MagicInclude>? Planes { get; set; }

    [JsonPropertyName("schemes")]
    public IList<MagicInclude>? Schemes { get; set; }

    [JsonPropertyName("tokens")]
    public IList<MagicInclude>? Tokens { get; set; }
}
