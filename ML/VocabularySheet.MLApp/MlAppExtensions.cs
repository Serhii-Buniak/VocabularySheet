using Microsoft.ML;

namespace VocabularySheet.MLApp;

public interface IWordClassificationService
{
    // ArticleType GetSemanticType(string text);
    // public List<(ArticleType Type, float Probability)> GetSemanticTypesWithProbabilities(string text)

}

public record ArticleProbability(ArticleType Type, float Probability);

public class MlWordService : IWordClassificationService
{
    public const string FileName = "wordArticles.zip";
    private static readonly string BaseFolderPath = Path.Combine(MlFolder.ModelsPath, FileName);
    
    private readonly PredictionEngine<MlArticleRecord, ArticlePrediction> _predictionEngine;

    public MlWordService()
    {
        var mlContext = new MLContext();
        ITransformer trainedModel = mlContext.Model.Load(BaseFolderPath, out _);
        
        _predictionEngine = mlContext.Model.CreatePredictionEngine<MlArticleRecord, ArticlePrediction>(trainedModel);
    }

    public List<ArticleProbability> GetSemanticTypesWithProbabilities(string text)
    {
        var mlContext = new MLContext(seed: 1);
        var model = mlContext.Model.Load(BaseFolderPath, out var schema);

        var engine = mlContext.Model.CreatePredictionEngine<MlArticleRecord, ArticlePrediction>(model);

        var articleRecord = new MlArticleRecord { Text = text, Type = 0 };
        var prediction = engine.Predict(articleRecord);

        return prediction.GetArticleTypesWithProbabilities().OrderByDescending(x => x.Probability).ToList();
    }

    
    // public ArticleType GetSemanticType(string text)
    // {
    //     var articleRecord = new MlArticleRecord { Text = text, Type = 0 };
    //     var prediction = _predictionEngine.Predict(articleRecord);
    //
    //     return prediction.PredictedType;
    //
    //     // if (Enum.TryParse(prediction.PredictedLabel, out ArticleType semanticType))
    //     // {
    //     //     return semanticType;
    //     // }
    //     //
    //     // throw new InvalidOperationException("Failed to determine semantic type.");
    // }
}


public static class MlAppExtensions
{
    
}