using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Text;
using VocabularySheet.Common;
using VocabularySheet.Common.Extensions;
using VocabularySheet.ML.Client;

namespace VocabularySheet.ML.Evaluation;

public interface IWordEvaluationService
{
    Task<MulticlassClassificationMetrics?> EvaluateAsync(CancellationToken cancellationToken);
}

internal class MlWordEvaluationService : IWordEvaluationService
{
    private readonly IMlDatasetsFolder _mlDatasetsFolder;
    private readonly MlDataSets _dataSets;

    public MlWordEvaluationService(IMlDatasetsFolder mlDatasetsFolder, MlDataSets dataSets)
    {
        _mlDatasetsFolder = mlDatasetsFolder;
        _dataSets = dataSets;
    }

    // var pipeline = mlContext.Transforms.Text
    //     .NormalizeText("Text", nameof(MlArticleRecord.Text))
    //     .Append(mlContext.Transforms.Text.TokenizeIntoWords("Text"))
    //     .Append(mlContext.Transforms.Text.RemoveDefaultStopWords("Text"))
    //     .Append(mlContext.Transforms.Conversion.MapValueToKey("Text"))
    //     .Append(mlContext.Transforms.Text.ProduceNgrams("Text"))
    //     .Append(mlContext.Transforms.NormalizeLpNorm("Text"))
    //     .Append(mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(MlArticleRecord.Type)))
    //     .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(featureColumnName: "Text"))
    //     .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

    
    public async Task<MulticlassClassificationMetrics?> EvaluateAsync(CancellationToken cancellationToken)
    {
        // Load data
        var data = await _dataSets.GetArticleDataSet();
        data = PerformUnderSampling(data).ToList();
        var mlContext = new MLContext(seed: 1);

        // Define data schema
        var dataView = mlContext.Data.LoadFromEnumerable(data);

        // Split the data into training and testing sets
        var trainTestData = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.000001);


        // Separate train and test data
        var trainData = trainTestData.TrainSet;
        var testData = trainTestData.TestSet;

        // Define preprocessing pipeline
        var preprocessingPipeline = mlContext.Transforms.Text
            .NormalizeText("NormalizedText", nameof(MlArticleRecord.Text))
            .Append(mlContext.Transforms.Text.TokenizeIntoWords("Tokens", "NormalizedText"))
            .Append(mlContext.Transforms.Text.NormalizeText("Tokens"))
            .Append(mlContext.Transforms.Text.RemoveDefaultStopWords("Tokens"))
            .Append(mlContext.Transforms.Text.FeaturizeText("Features", "Tokens"))
            .Append(mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(MlArticleRecord.Type)));
        
        IEstimator<ITransformer> trainer = mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy();

        // Define data preparation pipeline
        var dataPipeline = preprocessingPipeline.Append(trainer)
            // .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy())
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
        
        // Train the model
        var model = dataPipeline.Fit(trainData);

        // Save the model
        var path = Path.Combine(_mlDatasetsFolder.SaveModelsPath, MlModelConstants.WordArticlesFileName);
        mlContext.Model.Save(model, trainTestData.TrainSet.Schema, path);

        // Evaluate the model
        var predictions = model.Transform(testData);
        MulticlassClassificationMetrics? metrics = mlContext.MulticlassClassification.Evaluate(predictions);
        var json = Json.Pretty.Serialize(metrics);
        return metrics;
    }

    private IEnumerable<MlArticleRecord> PerformUnderSampling(List<MlArticleRecord> data)
    {
        // Step 1: Find common texts that appear in every type
        var commonTexts = FindCommonTexts(data);

        // Step 2: Remove common texts from each type
        var filteredData = RemoveCommonTexts(data, commonTexts);

        // Step 3: Perform under-sampling to balance the dataset
        var sampled = PerformBalancedUnderSampling(filteredData);

        return sampled.GroupBy(x => x.Type).SelectMany(x =>
        {
            string[] str = x.Select(x => x.Text).ToArray();
            string joined = string.Join(" ", str);

            string[] words = joined.Split(" ");
            
            IEnumerable<string> chunks = Enumerable.Range(0, words.Length / 2)
                    .Select(i => string.Join(" ", words.Skip(i * 2).Take(2)));

            return chunks.Select(c => new MlArticleRecord
            {
                Text = c,
                Type = x.Key
            });
        }).Where(x => !string.IsNullOrWhiteSpace(x.Text)).Distinct().ToList();
    }

    private HashSet<string> FindCommonTexts(IEnumerable<MlArticleRecord> data)
    {
        var groupByType = data.GroupBy(record => record.Type).ToList();
        var commonTexts = new HashSet<string>(groupByType.First().Select(record => record.Text));

        foreach (var group in groupByType.Skip(1))
        {
            commonTexts.IntersectWith(group.Select(record => record.Text));
        }

        return commonTexts;
    }

    private IEnumerable<MlArticleRecord> RemoveCommonTexts(IEnumerable<MlArticleRecord> data, HashSet<string> commonTexts)
    {
        var filteredData = new List<MlArticleRecord>();

        foreach (var record in data)
        {
            if (!commonTexts.Contains(record.Text))
            {
                filteredData.Add(record);
            }
        }

        return filteredData;
    }

    private IEnumerable<MlArticleRecord> PerformBalancedUnderSampling(IEnumerable<MlArticleRecord> data)
    {
        // Group the records by their classification type
        var groupByType = data.GroupBy(record => record.Type).ToList();

        // Calculate the total number of words for each group
        var totalWordsByGroup = groupByType.ToDictionary(
            group => group.Key,
            group => group.Sum(record => CountWords(record.Text))
        );

        // Determine the minimum total word count across all groups
        var minTotalWordCount = totalWordsByGroup.Min(kv => kv.Value);

        // Perform under-sampling to balance the dataset based on total word count
        var underSampledData = new List<MlArticleRecord>();

        foreach (var group in groupByType)
        {
            var wordsCount = 0;
            foreach (MlArticleRecord record in group)
            {
                underSampledData.Add(record);
                wordsCount += CountWords(record.Text);
                if (wordsCount >= minTotalWordCount)
                {
                    break;
                }
            }
        }

        return underSampledData;
    }
    private int CountWords(string text)
    {
        return text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    }
}