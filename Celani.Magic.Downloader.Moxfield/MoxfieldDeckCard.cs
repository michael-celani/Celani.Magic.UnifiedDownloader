using System.Text.Json.Serialization;

namespace Celani.Magic.Downloader.Moxfield;
public class MoxfieldDeckCard
{
    [JsonPropertyName("quantity")]
    public required int Quantity { get; set; }

    [JsonPropertyName("card")]
    public required MoxfieldCard Card { get; set; }
}