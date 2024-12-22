using Refit;

namespace Celani.Magic.UnifiedDownloader.Api;

internal interface IMagicDownloaderApi
{
    [Get("/decks/{backend}/{id}")]
    Task<ApiResponse<DownloadResult>> GetDeckAsync(WebsiteBackend backend, string id);

    [Get("/decks/search")]
    Task<ApiResponse<SearchResult>> SearchDecksAsync(SearchRequest request);
}
