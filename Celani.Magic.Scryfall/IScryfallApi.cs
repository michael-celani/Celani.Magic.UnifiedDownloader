using Refit;

namespace Celani.Magic.Scryfall;

public interface IScryfallApi
{
    [Get("/bulk-data")]
    Task<ResultPage<BulkData>> GetBulkDataAsync();

    [Get("/migrations")]
    Task<ResultPage<Migration>> GetMigrationsAsync([Query] int? page = null);

    [Get("/cards/search")]
    Task<ResultPage<ScryfallCardObject>> SearchCardsAsync([Query] ScryfallCardSearchParameters parameters);

    public async IAsyncEnumerable<Migration> GetAllMigrationsAsync()
    {
        ResultPage<Migration> page;
        var pageNumber = 1;

        do
        {
            page = await GetMigrationsAsync(pageNumber);

            foreach (var migration in page.Data) yield return migration;

            pageNumber++;
        } while (page.HasMore);
    }

    public async IAsyncEnumerable<ScryfallCardObject> GetAllCardsAsync(ScryfallCardSearchParameters parameters)
    {
        ResultPage<ScryfallCardObject> page;
        var pageNumber = 1;

        do
        {
            page = await SearchCardsAsync(parameters with { Page = pageNumber });

            foreach (var card in page.Data) yield return card;

            pageNumber++;
        } while (page.HasMore);
    }
}
