using Refit;
using System.Runtime.Serialization;

namespace Celani.Magic.Scryfall;

public record ScryfallCardSearchParameters
{
    [AliasAs("q")]
    public required string Query {get; set;}

    [AliasAs("order")]
    public ScryfallCardSearchSortOrder Order { get; set; }

    [AliasAs("page")]
    public int Page { get; set; }
}

public enum ScryfallCardSearchSortOrder
{
    [EnumMember(Value = "name")]
    Name,

    [EnumMember(Value = "set")]
    Set,

    [EnumMember(Value = "released")]
    Released,

    [EnumMember(Value = "rarity")]
    Rarity,

    [EnumMember(Value = "color")]
    Color,

    [EnumMember(Value = "usd")]
    Usd,

    [EnumMember(Value = "tix")]
    Tix,

    [EnumMember(Value = "eur")]
    Eur,

    [EnumMember(Value = "cmc")]
    ManaValue,

    [EnumMember(Value = "power")]
    Power,

    [EnumMember(Value = "toughness")]
    Toughness,

    [EnumMember(Value = "edhrec")]
    EdhrecRank,

    [EnumMember(Value = "penny")]
    Penny,

    [EnumMember(Value = "artist")]
    Artist,

    [EnumMember(Value = "review")]
    Review
}