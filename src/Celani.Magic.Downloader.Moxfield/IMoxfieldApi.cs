using Refit;

namespace Celani.Magic.Downloader.Moxfield;

public interface IMoxfieldApi
{
    [Get("/v3/decks/all/{publicId}")]
    Task<MoxfieldDeck> GetDeckAsync(string publicId);

    [Get("/v2/decks/search")]
    Task<MoxfieldSearchResult> SearchAsync(MoxfieldSearchParameters parameters);

    [Get("/v2/cards/search")]
    Task<MoxfieldCardSearchResult> SearchCardsAsync(MoxfieldCardSearchParameters parameters);

    public async Task<MoxfieldCard> GetCardByNameAsync(string cardName)
    {
        // Search for the card from Moxfield:
        var query = $"(({cardName}))";

        var cardSearch = new MoxfieldCardSearchParameters
        {
            Query = query,
            PageNumber = 1
        };

        var cardSearchResult = await SearchCardsAsync(cardSearch);

        if (cardSearchResult.Cards.Count < 1)
        {
            throw new InvalidOperationException("Card not found.");
        }

        return cardSearchResult.Cards[0];
    }

    public async IAsyncEnumerable<string> GetDeckIdsAsync(MoxfieldSearchParameters searchParams)
    {
        // Search for the decks:
        MoxfieldSearchResult searchResults;

        do
        {
            searchResults = await SearchAsync(searchParams);

            foreach (var result in searchResults.Data) yield return result.PublicId;

            searchParams.PageNumber++;
        } while (searchParams.PageNumber <= searchResults.TotalPages);
    }
}
