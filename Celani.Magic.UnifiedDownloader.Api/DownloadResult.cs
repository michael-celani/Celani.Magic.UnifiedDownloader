using System.Text.Json.Serialization;

namespace Celani.Magic.UnifiedDownloader.Api;

public class DownloadResult
{
    [JsonPropertyName("result_code")]
    public required DownloadResultCode ResultCode { get; set; }

    [JsonPropertyName("data")]
    public DownloadResultSuccess? Data { get; set; }

    [JsonPropertyName("error")]
    public DownloadResultFailure? Error { get; set; }
}
