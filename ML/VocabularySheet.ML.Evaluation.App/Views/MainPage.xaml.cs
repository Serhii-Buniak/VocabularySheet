using Microsoft.UI.Xaml.Controls;

using VocabularySheet.ML.Evaluation.App.ViewModels;

namespace VocabularySheet.ML.Evaluation.App.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
}
