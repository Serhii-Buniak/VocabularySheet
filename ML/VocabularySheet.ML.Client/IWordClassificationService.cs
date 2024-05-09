using Microsoft.ML;

namespace VocabularySheet.ML.Client;

public interface IWordClassificationService
{
    // ArticleType GetSemanticType(string text);
    // public List<(ArticleType Type, float Probability)> GetProbability(string text)

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

    public List<ArticleProbability> GetProbability(string text)
    {
        var articleRecord = new MlArticleRecord { Text = text, Type = 0 };
        
        var prediction = _engine.Predict(articleRecord);

        return prediction.GetArticleTypesWithProbabilities().OrderByDescending(x => x.Probability).ToList();
    }
}