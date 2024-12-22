using System.Text.Json.Serialization;

namespace Celani.Magic.Scryfall;

public enum CardLayout
{
    [JsonStringEnumMemberName("normal")]
    Normal,
    [JsonStringEnumMemberName("split")]
    Split,
    [JsonStringEnumMemberName("flip")]
    Flip,
    [JsonStringEnumMemberName("transform")]
    Transform,
    [JsonStringEnumMemberName("modal_dfc")]
    ModalDoubleFaced,
    [JsonStringEnumMemberName("meld")]
    Meld,
    [JsonStringEnumMemberName("leveler")]
    Leveler,
    [JsonStringEnumMemberName("class")]
    Class,
    [JsonStringEnumMemberName("case")]
    Case,
    [JsonStringEnumMemberName("saga")]
    Saga,
    [JsonStringEnumMemberName("adventure")]
    Adventure,
    [JsonStringEnumMemberName("mutate")]
    Mutate,
    [JsonStringEnumMemberName("prototype")]
    Prototype,
    [JsonStringEnumMemberName("battle")]
    Battle,
    [JsonStringEnumMemberName("planar")]
    Planar,
    [JsonStringEnumMemberName("scheme")]
    Scheme,
    [JsonStringEnumMemberName("vanguard")]
    Vanguard,
    [JsonStringEnumMemberName("token")]
    Token,
    [JsonStringEnumMemberName("double_faced_token")]
    DoubleFacedToken,
    [JsonStringEnumMemberName("emblem")]
    Emblem,
    [JsonStringEnumMemberName("augment")]
    Augment,
    [JsonStringEnumMemberName("host")]
    Host,
    [JsonStringEnumMemberName("art_series")]
    ArtSeries,
    [JsonStringEnumMemberName("reversible_card")]
    Reversible
}