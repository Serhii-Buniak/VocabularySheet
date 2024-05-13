using WordSearchVm = Apps.MauiRunner.ViewModels.WordSearchVm;

namespace Apps.MauiRunner.Views;

public partial class WordSearch : ContentPage
{
    private readonly WordSearchVm _wordDetails;

    public WordSearch(WordSearchVm wordDetails)
    {
        InitializeComponent();
        _wordDetails = wordDetails;
        BindingContext = wordDetails;
    }
}