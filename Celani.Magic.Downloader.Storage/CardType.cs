namespace Celani.Magic.Downloader.Storage;

[Flags]
public enum CardType
{
    None = 0,
    Artifact = 1 << 0,
    Battle = 1 << 1,
    Conspiracy = 1 << 2,
    Creature = 1 << 3,
    Dungeon = 1 << 4,
    Enchantment = 1 << 5,
    Instant = 1 << 6,
    Kindred = 1 << 7,
    Land = 1 << 8,
    Phenomenon = 1 << 9,
    Plane = 1 << 10,
    Planeswalker = 1 << 11,
    Scheme = 1 << 12,
    Sorcery = 1 << 13,
    Vanguard = 1 << 14
}

public static class CardTypeTools
{
    public static CardType FromTypeLine(string? typeLine)
    {
        if (typeLine is null) return CardType.None;

        CardType cardType = CardType.None;

        foreach (var type in typeLine.Split(" "))
        {
            if (Enum.TryParse(type, true, out CardType parsedType))
            {
                cardType |= parsedType;
            }
        }

        return cardType;
    }

    private static readonly CardType[] PriorityOrder = [
        CardType.Planeswalker,
        CardType.Land,
        CardType.Battle,
        CardType.Creature,
        CardType.Artifact,
        CardType.Enchantment,
        CardType.Instant,
        CardType.Sorcery
    ];

    public static CardType GetHighestPriorityCardType(CardType cardTypes)
    {
        foreach (var priority in PriorityOrder)
        {
            if (cardTypes.HasFlag(priority))
            {
                return priority;
            }
        }

        // If none of the prioritized types are found, return the first set flag
        return Enum.GetValues<CardType>().FirstOrDefault(type => cardTypes.HasFlag(type));
    }
}