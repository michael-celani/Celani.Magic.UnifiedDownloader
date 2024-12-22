using System.Text.Json.Serialization;

namespace Celani.Magic.Scryfall;

public enum BulkDataType
{
    [JsonStringEnumMemberName("oracle_cards")]
    OracleCards,

    [JsonStringEnumMemberName("unique_artwork")]
    UniqueArtwork,

    [JsonStringEnumMemberName("default_cards")]
    DefaultCards,

    [JsonStringEnumMemberName("all_cards")]
    AllCards,

    [JsonStringEnumMemberName("rulings")]
    Rulings,
}
