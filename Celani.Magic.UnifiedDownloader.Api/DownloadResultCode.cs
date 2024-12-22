using System.Text.Json.Serialization;

namespace Celani.Magic.UnifiedDownloader.Api;

public enum DownloadResultCode
{
    [JsonStringEnumMemberName("success")]
    Success,

    [JsonStringEnumMemberName("error_unknown")]
    ErrorUnknown,

    [JsonStringEnumMemberName("error_not_found")]
    ErrorNotFound
}
