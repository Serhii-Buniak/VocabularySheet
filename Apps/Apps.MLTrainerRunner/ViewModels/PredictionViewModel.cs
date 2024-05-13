using Apps.MLTrainerRunner.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ML.Predictor;
using VocabularySheet.ML.Client;

namespace Apps.MLTrainerRunner.ViewModels;

public record ProbabilityRecordSafe
{
    public ArticleType ArticleType
    {
        get;
        init;
    }
    
    public float Probability
    {
        get;
        init;
    }
}

public record ArticleProbabilityResultSafe
{
    public string Text { get; init; } = string.Empty;
    public List<ProbabilityRecordSafe> Probabilities { get; init; } = [];
    
    public List<ProbabilityRecordSafe> OrderedList { get; init; } = [];
    
    public static ArticleProbabilityResultSafe Create(ArticleProbabilityResult result)
    {
        var probabilityRecordSafes = result.Probabilities.Select(x => new ProbabilityRecordSafe
        {
            ArticleType = x.Key,
            Probability = x.Value
        }).ToList();
        
        return new ArticleProbabilityResultSafe()
        {
            Text = result.Text,
            Probabilities = probabilityRecordSafes,
            OrderedList = probabilityRecordSafes.OrderByDescending(x => x.Probability).ToList()
        };
    }
}

public partial class PredictionViewModel : ObservableRecipient
{
    private readonly IWordClassificationService _wordClassificationService;

    public string FolderPath => MlModelsFolder.BasePath;
    public PredictionViewModel()
    {
        _wordClassificationService = App.GetService<IWordClassificationService>();
        Shared = App.GetService<SharedViewModel>();
    }

    public SharedViewModel Shared
    {
        get;
    }

    [RelayCommand]
    public void RunPrediction()
    {
        try
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(Shared.TextPrediction);
            Shared.ProbabilityPrediction = ArticleProbabilityResultSafe.Create(_wordClassificationService.GetProbability(Shared.TextPrediction));
            Shared.ErrorPrediction = null;
        }
        catch (Exception e)
        {
            Shared.ErrorPrediction = e.Message;
        }
    }
}
