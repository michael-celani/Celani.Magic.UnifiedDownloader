using System.Text.Json.Serialization;

namespace Celani.Magic.UnifiedDownloader.Api;

public enum SearchResultCode
{
    [JsonStringEnumMemberName("sucess")]
    Success,

    [JsonStringEnumMemberName("error_unknown")]
    ErrorUnknown,

    [JsonStringEnumMemberName("error_not_found")]
    ErrorNotFound
}
