using Microsoft.ML;

namespace VocabularySheet.MLApp;

public interface IWordClassificationService
{
    ArticleType GetSemanticType(string text);
}

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

    public ArticleType GetSemanticType(string text)
    {
        var articleRecord = new MlArticleRecord { Text = text, Type = 0 };
        var prediction = _predictionEngine.Predict(articleRecord);

        return prediction.PredictedType;

        // if (Enum.TryParse(prediction.PredictedLabel, out ArticleType semanticType))
        // {
        //     return semanticType;
        // }
        //
        // throw new InvalidOperationException("Failed to determine semantic type.");
    }
}


public static class MlAppExtensions
{
    
}