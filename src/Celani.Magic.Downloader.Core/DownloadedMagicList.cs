using System.Text.Json.Serialization;

namespace Celani.Magic.Downloader.Core;

/// <summary>
/// Identifies a Magic decklist.
/// </summary>
public record DownloadedMagicList
{
    /// <summary>
    /// The deck ID.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("mainboard")]
    public List<DownloadedMagicInclude> Mainboard { get; set; } = [];

    [JsonPropertyName("sideboard")]
    public List<DownloadedMagicInclude> Sideboard { get; set; } = [];

    [JsonPropertyName("maybeboard")]
    public List<DownloadedMagicInclude> Maybeboard { get; set; } = [];

    [JsonPropertyName("commanders")]
    public List<DownloadedMagicInclude> Commanders { get; set; } = [];

    [JsonPropertyName("companions")]
    public List<DownloadedMagicInclude> Companions { get; set; } = [];

    [JsonPropertyName("signature_spells")]
    public List<DownloadedMagicInclude> SignatureSpells { get; set; } = [];

    [JsonPropertyName("attractions")]
    public List<DownloadedMagicInclude> Attractions { get; set; } = [];

    [JsonPropertyName("stickers")]
    public List<DownloadedMagicInclude> Stickers { get; set; } = [];

    [JsonPropertyName("contraptions")]
    public List<DownloadedMagicInclude> Contraptions { get; set; } = [];

    [JsonPropertyName("planes")]
    public List<DownloadedMagicInclude> Planes { get; set; } = [];

    [JsonPropertyName("schemes")]
    public List<DownloadedMagicInclude> Schemes { get; set; } = [];

    [JsonPropertyName("tokens")]
    public List<DownloadedMagicInclude> Tokens { get; set; } = [];
}
