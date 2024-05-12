using WordSearchVm = VocabularySheet.Maui.AppRunner.ViewModels.WordSearchVm;

namespace VocabularySheet.Maui.AppRunner.Views;

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