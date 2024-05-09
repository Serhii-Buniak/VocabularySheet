using Microsoft.UI.Xaml.Controls;

using VocabularySheet.ML.Evaluation.App.ViewModels;

namespace VocabularySheet.ML.Evaluation.App.Views;

public sealed partial class PredictionPage : Page
{
    public PredictionViewModel ViewModel
    {
        get;
    }

    public PredictionPage()
    {
        ViewModel = App.GetService<PredictionViewModel>();
        InitializeComponent();
    }
}
