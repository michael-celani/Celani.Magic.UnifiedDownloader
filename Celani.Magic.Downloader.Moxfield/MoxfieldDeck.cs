using System.Text.Json.Serialization;

namespace Celani.Magic.Downloader.Moxfield;

public class MoxfieldDeck
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("publicId")]
    public required string PublicId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("boards")]
    public required MoxfieldDeckBoardCollection Boards { get; set; }
}
