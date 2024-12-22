namespace Celani.Magic.Downloader.Storage;

public record OracleCardStoredDeck
{
    public int CardsId { get; set; }

    public int StoredDecksId { get; set; }

    public OracleCard? Cards { get; set; }

    public StoredDeck? StoredDecks { get; set; }
}