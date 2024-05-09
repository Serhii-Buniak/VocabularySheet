using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.ML.Data;

namespace VocabularySheet.ML.Evaluation.App.ViewModels;

public partial class SharedViewModel : ObservableRecipient
{
    [ObservableProperty] private MulticlassClassificationMetrics? _metrics;
    [ObservableProperty] private string? _errorEvaluation;

    [ObservableProperty]
    private ArticleProbabilityResultSafe _probabilityPrediction = new ArticleProbabilityResultSafe();
    [ObservableProperty] private string? errorPrediction;
    
    [ObservableProperty] private string _textPrediction = string.Empty;

}