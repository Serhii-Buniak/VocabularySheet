using Microsoft.ML;

namespace VocabularySheet.ML.Client;

public interface IWordClassificationService
{
    ArticleProbabilityResult GetProbability(string text);
}

internal class MlWordService : IWordClassificationService
{
    private readonly PredictionEngine<MlArticleRecord, ArticlePrediction> _engine;
    
    public MlWordService(IMlModelsFolder mlModelsFolder)
    {
        var mlContext = new MLContext(seed: 1);
        using var stream = mlModelsFolder.GetModel(MlModelConstants.WordArticlesFileName).GetAwaiter().GetResult();
        var model = mlContext.Model.Load(stream, out _);
        _engine = mlContext.Model.CreatePredictionEngine<MlArticleRecord, ArticlePrediction>(model);
    }

    public ArticleProbabilityResult GetProbability(string text)
    {
        var articleRecord = new MlArticleRecord { Text = text, Type = 0 };
        
        var prediction = _engine.Predict(articleRecord);
        
        return new ArticleProbabilityResult
        {
            Text = text,
            Probabilities = prediction.GetArticleTypesWithProbabilities()
        };
    }
}