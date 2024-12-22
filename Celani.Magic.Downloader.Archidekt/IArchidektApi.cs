using Refit;

namespace Celani.Magic.Downloader.Archidekt;

public interface IArchidektApi
{
    [Get("/decks/{publicId}/")]
    Task<ArchidektDeck> GetDeckAsync(string publicId); 
}
