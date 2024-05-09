using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Text;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace VocabularySheet.MLApp;

public enum ArticleType
{
    Business = 1,
    Entertainment = 2,
    Food = 3,
    Graphics = 4,
    Historical = 5,
    Medical = 6,
    Politics = 7,
    Space = 8,
    Sport = 9,
    Technologie = 10,
}

internal record MlArticleRecord
{
    public required string Text { get; init; }
    public required int Type { get; init; }
    
    public ArticleType GetArticleType() => (ArticleType)Type;
    public static List<MlArticleRecord> CreateList(MlArticlesFiles files)
    {
        return
        [
            ..files.Business.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Business
            }),
            ..files.Entertainment.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Entertainment
            }),
            ..files.Food.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Food
            }),
            ..files.Graphics.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Graphics
            }),
            ..files.Historical.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Historical
            }),
            ..files.Medical.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Medical
            }),
            ..files.Politics.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Politics
            }),
            ..files.Space.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Space
            }),
            ..files.Sport.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Sport
            }),
            ..files.Technologie.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Technologie
            }),
        ];
    }
}

internal record MlArticlesFiles
{
    public required string[] Business { get; init; }
    public required string[] Entertainment { get; init; }
    public required string[] Food { get; init; }
    public required string[] Graphics { get; init; }
    public required string[] Historical { get; init; }
    public required string[] Medical { get; init; }
    public required string[] Politics { get; init; }
    public required string[] Space { get; init; }
    public required string[] Sport { get; init; }
    public required string[] Technologie { get; init; }
}

internal class MlWordClassificationService
{
    public static List<MlArticleRecord> GetDataSet()
    {
        Dictionary<string, MlFolderEntry> folders = MlFolder.GetFoldersPath("document-classification");
        var articlesFiles = new MlArticlesFiles
        {
            Business = MlFolder.GetFilesPath(folders["business"].Path).Select(x => x.Value.Content).ToArray(),
            Entertainment = MlFolder.GetFilesPath(folders["entertainment"].Path).Select(x => x.Value.Content).ToArray(),
            Food = MlFolder.GetFilesPath(folders["food"].Path).Select(x => x.Value.Content).ToArray(),
            Graphics = MlFolder.GetFilesPath(folders["graphics"].Path).Select(x => x.Value.Content).ToArray(),
            Historical = MlFolder.GetFilesPath(folders["historical"].Path).Select(x => x.Value.Content).ToArray(),
            Medical = MlFolder.GetFilesPath(folders["medical"].Path).Select(x => x.Value.Content).ToArray(),
            Politics = MlFolder.GetFilesPath(folders["politics"].Path).Select(x => x.Value.Content).ToArray(),
            Space = MlFolder.GetFilesPath(folders["space"].Path).Select(x => x.Value.Content).ToArray(),
            Sport = MlFolder.GetFilesPath(folders["sport"].Path).Select(x => x.Value.Content).ToArray(),
            Technologie = MlFolder.GetFilesPath(folders["technologie"].Path).Select(x => x.Value.Content).ToArray(),
        };

        return MlArticleRecord.CreateList(articlesFiles);
    }
    
    public static void Evaluation()
    {
        // Load data
        var data = GetDataSet(); 
        var mlContext = new MLContext(seed: 1);

        // Define data schema
        IDataView? dataView = mlContext.Data.LoadFromEnumerable(GetDataSet());

        // Split the data into training and testing sets
        var trainTestData = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.01);

        // Separate train and test data
        var trainData = trainTestData.TrainSet;
        var testData = trainTestData.TestSet;

        // Define preprocessing pipeline
        var preprocessingPipeline = mlContext.Transforms.Text
            .NormalizeText("NormalizedText", nameof(MlArticleRecord.Text))
            .Append(mlContext.Transforms.Text.TokenizeIntoWords("Tokens", "NormalizedText"))
            .Append(mlContext.Transforms.Text.NormalizeText("Tokens", keepNumbers: false, keepPunctuations: false))
            .Append(mlContext.Transforms.Text.RemoveDefaultStopWords("Tokens"))
            .Append(mlContext.Transforms.Text.FeaturizeText("Features", "Tokens"));

        // Define data preparation pipeline
        var dataPipeline = preprocessingPipeline
            .Append(mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(MlArticleRecord.Type)))
            .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy());

        // Train the model
        var model = dataPipeline.Fit(trainData);

        // Save the model
        var modelPath = MlFolder.CreateModelsPath(MlWordService.FileName);
        mlContext.Model.Save(model, trainTestData.TrainSet.Schema, modelPath);
        
        // Evaluate the model
        var predictions = model.Transform(testData);
        var metrics = mlContext.MulticlassClassification.Evaluate(predictions);

        Console.WriteLine(JsonSerializer.Serialize(metrics));
        Console.WriteLine();
        Console.WriteLine("ConfusionTable");
        Console.WriteLine(metrics.ConfusionMatrix.GetFormattedConfusionTable());
    }
}


internal record ArticlePrediction
{
    [ColumnName("PredictedLabel")]
    public uint PredictedLabel { get; set; }
    
    public ArticleType PredictedType => (ArticleType)PredictedLabel;
}
