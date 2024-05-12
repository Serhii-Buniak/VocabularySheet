using VocabularySheet.Maui.Domain.ViewModels;

namespace VocabularySheet.Maui.Domain.Views;

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