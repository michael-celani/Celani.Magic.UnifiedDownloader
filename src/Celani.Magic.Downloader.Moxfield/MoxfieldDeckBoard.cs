using System.Text.Json.Serialization;

namespace Celani.Magic.Downloader.Moxfield;

public class MoxfieldDeckBoard
{
    [JsonPropertyName("cards")]
    public required Dictionary<string, MoxfieldDeckCard> Cards { get; set; }
}