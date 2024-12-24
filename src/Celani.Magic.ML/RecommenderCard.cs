using Microsoft.ML.Data;

namespace Celani.Magic.ML;

public class RecommenderCard
{
    public uint DeckId;

    [KeyType(33628)]
    public uint CardId;

    public float Label;
}