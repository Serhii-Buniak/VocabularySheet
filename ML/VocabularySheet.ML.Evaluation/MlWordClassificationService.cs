using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using Microsoft.ML.Transforms.Text;
using VocabularySheet.Common;
using VocabularySheet.Common.Extensions;
using VocabularySheet.ML.Client;

namespace VocabularySheet.ML.Evaluation;

public interface IWordEvaluationService
{
    MulticlassClassificationMetrics? Evaluate();
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

    public MulticlassClassificationMetrics? Evaluate()
    {
        // Load data
        var data = _dataSets.GetArticleDataSet();
        data = PerformUnderSampling(data).ToList();
        var mlContext = new MLContext(seed: 1);

        // Define data schema
        var dataView = mlContext.Data.LoadFromEnumerable(data);

        // Split the data into training and testing sets
        var trainTestData = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);


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
        return PerformBalancedUnderSampling(filteredData);
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
        // Count the number of records for each type
        var groupByType = data.GroupBy(record => record.Type).ToList();
        var minCount = groupByType.Min(group => group.Count());

        // Perform under-sampling to balance the dataset
        var underSampledData = new List<MlArticleRecord>();

        foreach (var group in groupByType)
        {
            var underSampledGroup = group.OrderRandom().Take(minCount);
            underSampledData.AddRange(underSampledGroup);
        }

        return underSampledData;
    }

    public Task<MulticlassClassificationMetrics?> EvaluateAsync(CancellationToken cancellationToken)
    {
        return Task.Run(Evaluate, cancellationToken);
    }
}