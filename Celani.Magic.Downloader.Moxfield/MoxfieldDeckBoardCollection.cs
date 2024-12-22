using System.Text.Json.Serialization;

namespace Celani.Magic.Downloader.Moxfield;

public class MoxfieldDeckBoardCollection
{
    [JsonPropertyName("mainboard")]
    public MoxfieldDeckBoard? Mainboard { get; set; }

    [JsonPropertyName("sideboard")]
    public MoxfieldDeckBoard? Sideboard { get; set; }

    [JsonPropertyName("maybeboard")]
    public MoxfieldDeckBoard? Maybeboard { get; set; }

    [JsonPropertyName("commanders")]
    public MoxfieldDeckBoard? Commanders { get; set; }

    [JsonPropertyName("companions")]
    public MoxfieldDeckBoard? Companions { get; set; }

    [JsonPropertyName("signatureSpells")]
    public MoxfieldDeckBoard? SignatureSpells { get; set; }

    [JsonPropertyName("attractions")]
    public MoxfieldDeckBoard? Attractions { get; set; }

    [JsonPropertyName("stickers")]
    public MoxfieldDeckBoard? Stickers { get; set; }

    [JsonPropertyName("contraptions")]
    public MoxfieldDeckBoard? Contraptions { get; set; }

    [JsonPropertyName("planes")]
    public MoxfieldDeckBoard? Planes { get; set; }

    [JsonPropertyName("schemes")]
    public MoxfieldDeckBoard? Schemes { get; set; }

    [JsonPropertyName("tokens")]
    public MoxfieldDeckBoard? Tokens { get; set; }
}