﻿using Microsoft.ML;
using Microsoft.ML.Data;
using VocabularySheet.ML.Client;

namespace VocabularySheet.ML.Evaluation;

public class MlWordEvaluationService
{
    public MulticlassClassificationMetrics? Evaluate()
    {
        // Load data
        var data = MlDataSets.Article; 
        var mlContext = new MLContext(seed: 1);

        // Define data schema
        IDataView? dataView = mlContext.Data.LoadFromEnumerable(data);

        // Split the data into training and testing sets
        var trainTestData = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
        
        // Separate train and test data
        var trainData = trainTestData.TrainSet;
        var testData = trainTestData.TestSet;

        // Define preprocessing pipeline
        var preprocessingPipeline = mlContext.Transforms.Text
            .NormalizeText("NormalizedText", nameof(MlArticleRecord.Text))
            .Append(mlContext.Transforms.Text.TokenizeIntoWords("Tokens", "NormalizedText"))
            .Append(mlContext.Transforms.Text.NormalizeText("Tokens", keepNumbers: false, keepPunctuations: false))
            .Append(mlContext.Transforms.Text.RemoveDefaultStopWords("Tokens"))
            .Append(mlContext.Transforms.Text.FeaturizeText("Features", "Tokens"))
            .Append(mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(MlArticleRecord.Type)));

        var averagedPerceptronBinaryTrainer = mlContext.BinaryClassification.Trainers.AveragedPerceptron(numberOfIterations: 10);
        IEstimator<ITransformer> trainer = mlContext.MulticlassClassification.Trainers.OneVersusAll(averagedPerceptronBinaryTrainer);
        
        // Define data preparation pipeline
        var dataPipeline = preprocessingPipeline.Append(trainer)
            // .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy())
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        // Train the model
        var model = dataPipeline.Fit(trainData);
        
        // Save the model
        var modelPath = MlModelsFolder.CreatePath(MlModelConstants.WordArticlesFileName);
        mlContext.Model.Save(model, trainTestData.TrainSet.Schema, modelPath);
        
        // Evaluate the model
        var predictions = model.Transform(testData);
        MulticlassClassificationMetrics? metrics = mlContext.MulticlassClassification.Evaluate(predictions);
        
        return metrics;
    }
    
    public Task<MulticlassClassificationMetrics?> EvaluateAsync(CancellationToken cancellationToken)
    {
        return Task.Run(Evaluate, cancellationToken);
    }
}