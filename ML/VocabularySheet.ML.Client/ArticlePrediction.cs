using Microsoft.ML.Data;

namespace VocabularySheet.ML.Client;

public record ArticleProbabilityResult
{
    public required string Text { get; init; }
    public required Dictionary<ArticleType, float> Probabilities { get; init; }
    
    public List<KeyValuePair<ArticleType, float>> OrderedList => Probabilities.OrderByDescending(x => x.Value).ToList();
}

public record ArticlePrediction
{
    [ColumnName("PredictedLabel")]
    public int PredictedLabel { get; set; }

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


public record MlArticleRecord
{
    public required string Text { get; init; }
    public required int Type { get; init; }
    
    public ArticleType GetArticleType() => (ArticleType)Type;
}
