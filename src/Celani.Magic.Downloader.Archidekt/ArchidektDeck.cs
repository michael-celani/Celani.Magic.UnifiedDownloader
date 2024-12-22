using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Celani.Magic.Downloader.Core;

namespace Celani.Magic.Downloader.Archidekt;

public class ArchidektDeck
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("deckFormat")]
    public required FormatType DeckFormat { get; set; }

    [JsonPropertyName("categories")]
    public required List<ArchidektCategory> Categories { get; set; }

    [JsonPropertyName("cards")]
    public required List<ArchidektCard> Cards { get; set; }
}

public enum FormatType
{
    [EnumMember(Value = "none")]
    [JsonStringEnumMemberName("none")]
    None = 0,

    [EnumMember(Value = "standard")]
    [JsonStringEnumMemberName("standard")]
    Standard = 1,

    [EnumMember(Value = "modern")]
    [JsonStringEnumMemberName("modern")]
    Modern = 2,

    [EnumMember(Value = "edh")]
    [JsonStringEnumMemberName("edh")]
    Commander = 3,

    [EnumMember(Value = "legacy")]
    [JsonStringEnumMemberName("legacy")]
    Legacy = 4,

    [EnumMember(Value = "vintage")]
    [JsonStringEnumMemberName("vintage")]
    Vintage = 5,

    [EnumMember(Value = "pauper")]
    [JsonStringEnumMemberName("pauper")]
    Pauper = 6,

    [EnumMember(Value = "custom")]
    [JsonStringEnumMemberName("custom")]
    Custom = 7,

    [EnumMember(Value = "frontier")]
    [JsonStringEnumMemberName("frontier")]
    Frontier = 8,

    [EnumMember(Value = "future_standard")]
    [JsonStringEnumMemberName("future_standard")]
    FutureStandard = 9,

    [EnumMember(Value = "penny")]
    [JsonStringEnumMemberName("penny")]
    Penny = 10,

    [EnumMember(Value = "one_v_one_commander")]
    [JsonStringEnumMemberName("one_v_one_commander")]
    OneVOneCommander = 11,

    [EnumMember(Value = "duel_commander")]
    [JsonStringEnumMemberName("duel_commander")]
    DuelCommander = 12,

    [EnumMember(Value = "brawl")]
    [JsonStringEnumMemberName("brawl")]
    Brawl = 13,

    [EnumMember(Value = "oathbreaker")]
    [JsonStringEnumMemberName("oathbreaker")]
    Oathbreaker = 14,

    [EnumMember(Value = "pioneer")]
    [JsonStringEnumMemberName("pioneer")]
    Pioneer = 15,

    [EnumMember(Value = "historic")]
    [JsonStringEnumMemberName("historic")]
    Historic = 16,

    [EnumMember(Value = "pauper_edh")]
    [JsonStringEnumMemberName("pauper_edh")]
    PauperCommander = 17,

    [EnumMember(Value = "alchemy")]
    [JsonStringEnumMemberName("alchemy")]
    Alchemy = 18,

    [EnumMember(Value = "explorer")]
    [JsonStringEnumMemberName("explorer")]
    Explorer = 19,

    [EnumMember(Value = "historic_brawl")]
    [JsonStringEnumMemberName("historic_brawl")]
    HistoricBrawl = 20,

    [EnumMember(Value = "gladiator")]
    [JsonStringEnumMemberName("gladiator")]
    Gladiator = 21,

    [EnumMember(Value = "premodern")]
    [JsonStringEnumMemberName("premodern")]
    Premodern = 22,

    [EnumMember(Value = "predh")]
    [JsonStringEnumMemberName("predh")]
    PrEDH = 23,

    [EnumMember(Value = "timeless")]
    [JsonStringEnumMemberName("timeless")]
    Timeless = 24
}
public class ArchidektCategory
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("includedInDeck")]
    public required bool IncludedInDeck { get; set; }
}

public class ArchidektCard
{
    [JsonPropertyName("quantity")]
    public required int Quantity { get; set; }

    [JsonPropertyName("card")]
    public required ArchidektCardData Card { get; set; }

    [JsonPropertyName("categories")]
    public required List<string> Categories { get; set; }

    [JsonPropertyName("companion")]
    public required bool Companion { get; set; }

    public DownloadedMagicInclude ToInclude() => new()
    {
        Quantity = Quantity,
        ScryfallId = Card.ScryfallId,
        ScryfallOracleId = Card.OracleCard.ScryfallOracleId
    };
}

public class ArchidektCardData
{
    [JsonPropertyName("uid")]
    public required string ScryfallId { get; set; }

    [JsonPropertyName("oracleCard")]
    public required ArchidektOracleCardData OracleCard { get; set; }
}

public class ArchidektOracleCardData
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("uid")]
    public required string ScryfallOracleId { get; set; }

    [JsonPropertyName("types")]
    public required List<string> Types { get; set; }

    [JsonPropertyName("subTypes")]
    public required List<string> SubTypes { get; set; }
}