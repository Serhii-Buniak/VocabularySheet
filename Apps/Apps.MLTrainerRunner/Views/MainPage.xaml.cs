using Apps.MLTrainerRunner.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Apps.MLTrainerRunner.Views;

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
