using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Celani.Magic.UnifiedDownloader.Api;

public enum SortOrder
{
    [EnumMember(Value = "none")]
    [JsonStringEnumMemberName("none")]
    None,

    [EnumMember(Value = "updated")]
    [JsonStringEnumMemberName("updated")]
    Updated
}
