using VocabularySheet.Maui.ViewModels;

namespace VocabularySheet.Maui.Views;

public partial class WordSearch : ContentPage
{
    private readonly WordSearchVM _wordDetails;

    public WordSearch(WordSearchVM wordDetails)
    {
        InitializeComponent();
        _wordDetails = wordDetails;
        BindingContext = wordDetails;
    }
}