using Microsoft.ML;

namespace VocabularySheet.ML.Client;

public interface IWordClassificationService
{
    ArticleProbabilityResult GetProbability(string text);
}

internal class MlWordService : IWordClassificationService
{
    private static readonly string FilePath = MlModelsFolder.CreatePath(MlModelConstants.WordArticlesFileName);
    
    private readonly PredictionEngine<MlArticleRecord, ArticlePrediction> _engine;
    
    public MlWordService()
    {
        var mlContext = new MLContext(seed: 1);
        var model = mlContext.Model.Load(FilePath, out _);
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