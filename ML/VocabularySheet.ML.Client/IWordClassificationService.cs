using Catalyst;
using Catalyst.Models;
using Microsoft.ML;
using Mosaik.Core;
using VocabularySheet.Common.Extensions;

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
        var articleRecord = new MlArticleRecord
        {
            Features = string.Join(" ", text
                .ToLowerInvariant()
                .KeepOnlyLettersAndSpaces()
                .Split(" ")
                .Select(x => x.Lemmatize())
            ),
            Label = 0 };
        
        var prediction = _engine.Predict(articleRecord);
        
        return new ArticleProbabilityResult
        {
            Text = text,
            Probabilities = prediction.GetArticleTypesWithProbabilities()
        };
    }
}

public static class MlStringExtensions
{
    static MlStringExtensions()
    {
        English.Register();
    }

    public static string Lemmatize(this string text, Language language = Language.English)
    {
        var lemetizer = LemmatizerStore.Get(language);
        var token = new SingleToken(text, language);   
        return lemetizer.GetLemma(token);
    }
}
