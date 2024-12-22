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
            LabelColumnName = "Label",
            LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass,
            Alpha = 0.0025,
            Lambda = 0.025,
            C = 0.00001,
            ApproximationRank = 200,
            NumberOfIterations = 20
        };

        var estimator = context.Transforms.Conversion.MapValueToKey("DeckId", maximumNumberOfKeys: 10000000).AppendCacheCheckpoint(context).Append(
            context.Recommendation().Trainers.MatrixFactorization(options));
        var model = estimator.Fit(trainingDataView);

        // Save the model:
        context.Model.Save(model, trainingDataView.Schema, outputStream);
    }

    public static void WriteCardRecModel2(MagicContext magicContext, Stream outputStream)
    {
        var mlContext = new MLContext();

        var deckCards = magicContext.OracleCardStoredDeck.Join(
                magicContext.Decks,
                include => include.StoredDecksId,
                deck => deck.Id,
                (include, deck) => new RecommenderCard2
                {
                    Label = true,
                    DeckId = new[] {(float) include.StoredDecksId},
                    CardId = new [] {(float) include.CardsId},
                    CommanderId = new[] {(float) deck.MagicCommanderId}
                }
            ).AsNoTracking().Take(100_000);

        var trainingData = mlContext.Data.LoadFromEnumerable(deckCards);

        var pipeline = mlContext.BinaryClassification.Trainers
            .FieldAwareFactorizationMachine(
            [
                nameof(RecommenderCard2.DeckId), 
                nameof(RecommenderCard2.CardId),
                nameof(RecommenderCard2.CommanderId) 
            ],
            nameof(RecommenderCard2.Label));

        var model = pipeline.Fit(trainingData);

        var engine = mlContext.Model.CreatePredictionEngine<RecommenderCard2, RecommenderCardPrediction2>(model);

        mlContext.Model.Save(model, trainingData.Schema, outputStream);
    }
}
