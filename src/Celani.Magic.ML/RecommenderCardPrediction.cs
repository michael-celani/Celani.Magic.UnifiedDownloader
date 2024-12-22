namespace Celani.Magic.ML;

public class RecommenderCardPrediction
{
    public float Score;
}

public class RecommenderCardPrediction2
{
    // Label.
    public bool Label { get; set; }

    // Predicted label.
    public bool PredictedLabel { get; set; }

    // Predicted score.
    public float Score { get; set; }
    
    // Probability of belonging to positive class.
    public float Probability { get; set; }
}
