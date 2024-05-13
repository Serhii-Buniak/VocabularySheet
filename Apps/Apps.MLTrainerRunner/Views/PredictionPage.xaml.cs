using Apps.MLTrainerRunner.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Apps.MLTrainerRunner.Views;

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
