using Microsoft.ML.Data;

namespace Celani.Magic.ML;

public class RecommenderCard
{
    public uint DeckId;

    [KeyType(33628)]
    public uint CardId;

    public float Label;
}

public class RecommenderCard2
{
    public bool Label { get; set; }

    [VectorType(1)]
    public float[] DeckId { get; set; }

    [VectorType(1)]
    public float[] CardId { get; set; }

    [VectorType(1)]
    public float[] CommanderId { get; set; }
}