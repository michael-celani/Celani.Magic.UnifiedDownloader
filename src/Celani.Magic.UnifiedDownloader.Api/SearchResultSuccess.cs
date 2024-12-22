using System.Text.Json.Serialization;

namespace Celani.Magic.UnifiedDownloader.Api;

public class SearchResultSuccess
{
    [JsonPropertyName("deck_ids")]
    public required List<MagicDeckId> DeckIds { get; set; }
}
