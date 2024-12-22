using Refit;
using System.Text.Json.Serialization;

namespace Celani.Magic.UnifiedDownloader.Api;

internal class SearchRequest
{
    [AliasAs("backend")]
    [JsonPropertyName("backend")]
    public WebsiteBackend? Backend { get; set; }

    [AliasAs("dir")]
    [JsonPropertyName("dir")]
    public SortDirection? SortDirection { get; set; } = Api.SortDirection.Ascending;

    [AliasAs("order")]
    [JsonPropertyName("order")]
    public SortOrder? SortOrder { get; set; } = Api.SortOrder.Updated;
}
