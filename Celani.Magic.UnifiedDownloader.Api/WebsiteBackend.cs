using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Celani.Magic.UnifiedDownloader.Api;

public enum WebsiteBackend
{
    [EnumMember(Value = "moxfield")]
    [JsonStringEnumMemberName("moxfield")]
    Moxfield,

    [EnumMember(Value = "archidekt")]
    [JsonStringEnumMemberName("archidekt")]
    Archidekt
}
