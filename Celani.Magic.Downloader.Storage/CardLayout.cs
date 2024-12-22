using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Celani.Magic.Downloader.Storage;

public enum CardLayout
{
    [EnumMember(Value = "normal")]
    [JsonStringEnumMemberName("normal")]
    Normal,
    [EnumMember(Value = "split")]
    [JsonStringEnumMemberName("split")]
    Split,
    [EnumMember(Value = "flip")]
    [JsonStringEnumMemberName("flip")]
    Flip,
    [EnumMember(Value = "transform")]
    [JsonStringEnumMemberName("transform")]
    Transform,
    [EnumMember(Value = "modal_dfc")]
    [JsonStringEnumMemberName("modal_dfc")]
    ModalDoubleFaced,
    [EnumMember(Value = "meld")]
    [JsonStringEnumMemberName("meld")]
    Meld,
    [EnumMember(Value = "leveler")]
    [JsonStringEnumMemberName("leveler")]
    Leveler,
    [EnumMember(Value = "class")]
    [JsonStringEnumMemberName("class")]
    Class,
    [EnumMember(Value = "case")]
    [JsonStringEnumMemberName("case")]
    Case,
    [EnumMember(Value = "saga")]
    [JsonStringEnumMemberName("saga")]
    Saga,
    [EnumMember(Value = "adventure")]
    [JsonStringEnumMemberName("adventure")]
    Adventure,
    [EnumMember(Value = "mutate")]
    [JsonStringEnumMemberName("mutate")]
    Mutate,
    [EnumMember(Value = "prototype")]
    [JsonStringEnumMemberName("prototype")]
    Prototype,
    [EnumMember(Value = "battle")]
    [JsonStringEnumMemberName("battle")]
    Battle,
    [EnumMember(Value = "planar")]
    [JsonStringEnumMemberName("planar")]
    Planar,
    [EnumMember(Value = "scheme")]
    [JsonStringEnumMemberName("scheme")]
    Scheme,
    [EnumMember(Value = "vanguard")]
    [JsonStringEnumMemberName("vanguard")]
    Vanguard,
    [EnumMember(Value = "token")]
    [JsonStringEnumMemberName("token")]
    Token,
    [EnumMember(Value = "double_faced_token")]
    [JsonStringEnumMemberName("double_faced_token")]
    DoubleFacedToken,
    [EnumMember(Value = "emblem")]
    [JsonStringEnumMemberName("emblem")]
    Emblem,
    [EnumMember(Value = "augment")]
    [JsonStringEnumMemberName("augment")]
    Augment,
    [EnumMember(Value = "host")]
    [JsonStringEnumMemberName("host")]
    Host,
    [EnumMember(Value = "art_series")]
    [JsonStringEnumMemberName("art_series")]
    ArtSeries,
    [EnumMember(Value = "reversible_card")]
    [JsonStringEnumMemberName("reversible_card")]
    Reversible
}