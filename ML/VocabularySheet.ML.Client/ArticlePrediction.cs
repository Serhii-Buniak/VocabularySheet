using Microsoft.ML.Data;

namespace VocabularySheet.ML.Client;

public record ArticleProbability(ArticleType Type, float Probability);

public record ArticlePrediction
{
    [ColumnName("PredictedLabel")]
    public int PredictedLabel { get; set; }

    [ColumnName("Score")] 
    public float[] Probabilities { get; set; } = [];

    public List<ArticleProbability> GetArticleTypesWithProbabilities()
    {
        var result = new List<ArticleProbability>();
        for (int i = 0; i < Probabilities.Length; i++)
        {
            var type = (ArticleType)(i + 1);
            result.Add(new ArticleProbability(type, Probabilities[i]));
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
