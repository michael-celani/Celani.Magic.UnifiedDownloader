using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Celani.Magic.UnifiedDownloader.Api;

public enum SortDirection
{
    [EnumMember(Value = "none")]
    [JsonStringEnumMemberName("none")]
    None,

    [EnumMember(Value = "ascending")]
    [JsonStringEnumMemberName("ascending")]
    Ascending,

    [EnumMember(Value = "descending")]
    [JsonStringEnumMemberName("descending")]
    Descending
}