using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VocabularySheet.ML.Evaluation.App.ViewModels;

public partial class MainViewModel : ObservableRecipient
{
    private readonly IWordEvaluationService _evaluationService;

    public SharedViewModel Shared
    {
        get;
    }

    public string FolderPath
    {
        get;
    }

    public MainViewModel()
    {
        _evaluationService = App.GetService<IWordEvaluationService>();;
        Shared = App.GetService<SharedViewModel>();;
        var folder = App.GetService<IMlDatasetsFolder>();
        FolderPath = folder.FolderPath;
    }
    
    [RelayCommand]
    public async Task RunEvaluation(CancellationToken cancellationToken)
    {
        try
        {
            Shared.Metrics = await _evaluationService.EvaluateAsync(cancellationToken);
            Shared.ErrorEvaluation = null;
        }
        catch (Exception e)
        {
            Shared.ErrorEvaluation = e.Message;
        }
    }
}
