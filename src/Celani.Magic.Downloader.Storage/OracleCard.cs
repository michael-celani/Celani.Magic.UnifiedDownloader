using Microsoft.EntityFrameworkCore;

namespace Celani.Magic.Downloader.Storage;

[Index(nameof(Name))]
[Index(nameof(OracleId), IsUnique = true)]
public record OracleCard
{
    public int Id { get; set; }

    /// <summary>
    /// The name of the card.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The Scryfall Oracle ID.
    /// </summary>
    public required string OracleId { get; set; }

    /// <summary>
    /// The representative Scryfall ID, used for display.
    /// </summary>
    public required string RepresentativeScryfallId { get; set; }

    /// <summary>
    /// The color identity of the card.
    /// </summary>
    public required ColorIdentity ColorIdentity { get; set; }

    /// <summary>
    /// The types of the card.
    /// </summary>
    public required CardType CardTypes { get; set; }

    /// <summary>
    /// The mana cost of the card.
    /// </summary>
    public required string ManaCost { get; set; }

    /// <summary>
    /// The raw type line of the card.
    /// </summary>
    public required string TypeLine { get; set; }

    /// <summary>
    /// The Oracle text of the card.
    /// </summary>
    public required string OracleText { get; set; }

    /// <summary>
    /// All the Scryfall cards that correspond to this Oracle card.
    /// </summary>
    public List<ScryfallCard> ScryfallCards { get; set; } = [];

    /// <summary>
    /// The list of decks this card is included in.
    /// </summary>
    public List<StoredDeck> StoredDecks { get; set; } = [];
}
