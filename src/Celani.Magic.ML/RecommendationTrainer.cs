using Celani.Magic.Downloader.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Trainers;

namespace Celani.Magic.ML;

public static class RecommendationTrainer
{
    public static void WriteCardRecModel(MagicContext magicContext, Stream outputStream)
    {
        var context = new MLContext();

        var deckCards = magicContext.OracleCardStoredDeck.Select(
            pair => new RecommenderCard
            {
                DeckId = (uint) pair.StoredDecksId,
                CardId = (uint) pair.CardsId,
                Label = 1
            }).AsNoTracking();

        var trainingDataView = context.Data.LoadFromEnumerable(deckCards);

        var options = new MatrixFactorizationTrainer.Options
        {
            MatrixColumnIndexColumnName = nameof(RecommenderCard.DeckId),
            MatrixRowIndexColumnName = nameof(RecommenderCard.CardId),
            LabelColumnName = nameof(RecommenderCard.Label),
            LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass,
            Alpha = 0.0025,
            Lambda = 0.025,
            C = 0.001,
            ApproximationRank = 256,
            NumberOfIterations = 20,
            LearningRate = 0.1,
        };

        var estimator = context.Transforms.Conversion.MapValueToKey(nameof(RecommenderCard.DeckId), maximumNumberOfKeys: 10000000)
            //.Append(context.Transforms.Conversion.MapValueToKey(nameof(RecommenderCard.CardId), maximumNumberOfKeys: 10000000))
            .AppendCacheCheckpoint(context)
            .Append(context.Recommendation().Trainers.MatrixFactorization(options));

        var model = estimator.Fit(trainingDataView);

        // Save the model:
        context.Model.Save(model, trainingDataView.Schema, outputStream);
    }
}
