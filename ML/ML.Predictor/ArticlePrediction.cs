using Microsoft.ML.Data;
using Tools.Parsers;
using VocabularySheet.ML.Client;

namespace ML.Predictor;

public record ArticleProbabilityResult
{
    public required string Text { get; init; }
    public required Dictionary<ArticleType, float> Probabilities { get; init; }
    
    public List<KeyValuePair<ArticleType, float>> OrderedList => Probabilities.OrderByDescending(x => x.Value).ToList();
}

public record ArticlePrediction
{
    [ColumnName("PredictedLabel")]
    public float PredictedLabel { get; set; }

    [ColumnName("Score")] 
    public float[] Probabilities { get; set; } = [];
    
    public Dictionary<ArticleType, float> GetArticleTypesWithProbabilities()
    {
        var result = new Dictionary<ArticleType, float>();
        for (int i = 0; i < Probabilities.Length; i++)
        {
            var type = (ArticleType)(i + 1);
            result[type] = Probabilities[i];
        }
        return result;
    }
}


public record MlArticleRecord : ICsvAutoFile<MlArticleRecord>
{
    
    [ColumnName("Features")]
    public string Features
    {
        get;
        set;
    } = string.Empty;
    
    [ColumnName("Label")]
    public float Label { get; set; }
    
    public ArticleType GetArticleType() => (ArticleType)Label;
}
