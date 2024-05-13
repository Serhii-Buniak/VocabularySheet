using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.Data;
using ML.Predictor;
using Tools.Parsers;
using VocabularySheet.ML.Client;

namespace ML.Trainer;

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
    
    public async Task<MulticlassClassificationMetrics?> EvaluateAsync(CancellationToken cancellationToken)
    {
        // Load data
        List<MlArticleRecord> records = await _dataSets.GetArticleDataSet();
        records = PerformUnderSampling(records).ToList();
        string csvText = CsvParser.HeaderTab.Serialize(records);
        var csvPath = _mlDatasetsFolder.SaveAndGetPath("normalized_article_records.tsv", csvText);
        
        var ctx = new MLContext(seed: 1);
        
        // Train the model
        ctx.Log += (_, e) => {
            if (e.Source.Equals("AutoMLExperiment"))
            {
                Console.WriteLine(e.RawMessage);
            }
        };
        
        var columnInference = ctx.Auto().InferColumns(csvPath, groupColumns: false);
        
        // Create text loader
        var loader = ctx.Data.CreateTextLoader(columnInference.TextLoaderOptions);

        // Load data into IDataView
        var data = loader.Load(csvPath);
        var trainValidationData = ctx.Data.TrainTestSplit(data, testFraction: 0.2);
        
        var pipeline = ctx.Auto()
            .Featurizer(data, columnInformation: columnInference.ColumnInformation)
            .Append(ctx.Transforms.Conversion.MapValueToKey(inputColumnName: "Label", outputColumnName: "Label"))
            .Append(ctx.Auto().MultiClassification(labelColumnName: columnInference.ColumnInformation.LabelColumnName))
            .Append(ctx.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
        
        AutoMLExperiment experiment = ctx.Auto().CreateExperiment();
        
        experiment
            .SetPipeline(pipeline)
            .SetTrainingTimeInSeconds(1000)
            .SetMulticlassClassificationMetric(MulticlassClassificationMetric.MacroAccuracy, labelColumn: columnInference.ColumnInformation.LabelColumnName)
            .SetDataset(trainValidationData);
        
        TrialResult experimentResults = await experiment.RunAsync(cancellationToken);
        
        // Save the model
        var path = Path.Combine(_mlDatasetsFolder.SaveModelsPath, MlModelConstants.WordArticlesFileName);
        ctx.Model.Save(experimentResults.Model, trainValidationData.TrainSet.Schema, path);

        // Evaluate the model
        var predictions = experimentResults.Model.Transform(trainValidationData.TestSet);
        MulticlassClassificationMetrics? metrics = ctx.MulticlassClassification.Evaluate(predictions);
        var json = JsonParser.Pretty.Serialize(metrics);
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
        return sampled.GroupBy(x => x.Label).SelectMany(x =>
        {
            string[] str = x.Select(x => x.Features).ToArray();
            string joined = string.Join(" ", str);
        
            string[] words = joined.Split(" ");
            
            IEnumerable<string> chunks = Enumerable.Range(0, words.Length / 2)
                    .Select(i => string.Join(" ", words.Skip(i * 2).Take(2).Distinct()));
        
            return chunks.Select(c => new MlArticleRecord
            {
                Features = c,
                Label = x.Key
            });
        }).Where(x => !string.IsNullOrWhiteSpace(x.Features)).Distinct().ToList();
    }

    private HashSet<string> FindCommonTexts(IEnumerable<MlArticleRecord> data)
    {
        var groupByType = data.GroupBy(record => record.Label).ToList();
        var commonTexts = new HashSet<string>(groupByType.First().Select(record => record.Features));

        foreach (var group in groupByType.Skip(1))
        {
            commonTexts.IntersectWith(group.Select(record => record.Features));
        }

        return commonTexts;
    }

    private IEnumerable<MlArticleRecord> RemoveCommonTexts(IEnumerable<MlArticleRecord> data, HashSet<string> commonTexts)
    {
        var filteredData = new List<MlArticleRecord>();

        foreach (var record in data)
        {
            if (!commonTexts.Contains(record.Features))
            {
                filteredData.Add(record);
            }
        }

        return filteredData;
    }

    private IEnumerable<MlArticleRecord> PerformBalancedUnderSampling(IEnumerable<MlArticleRecord> data)
    {
        // Group the records by their classification type
        var groupByType = data.GroupBy(record => record.Label).ToList();

        // Calculate the total number of words for each group
        var totalWordsByGroup = groupByType.ToDictionary(
            group => group.Key,
            group => group.Sum(record => CountWords(record.Features))
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
                wordsCount += CountWords(record.Features);
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