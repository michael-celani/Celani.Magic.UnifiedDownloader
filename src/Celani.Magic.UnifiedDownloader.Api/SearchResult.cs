using System.Text.Json.Serialization;

namespace Celani.Magic.UnifiedDownloader.Api;

public class SearchResult
{
    [JsonPropertyName("transaction_id")]
    public required string TransactionId { get; set; }

    [JsonPropertyName("result_code")]
    public required SearchResultCode ResultCode { get; set; }

    [JsonPropertyName("data")]
    public SearchResultSuccess? Data { get; set; }

    [JsonPropertyName("error")]
    public SearchResultFailure? Error { get; set; }
}
