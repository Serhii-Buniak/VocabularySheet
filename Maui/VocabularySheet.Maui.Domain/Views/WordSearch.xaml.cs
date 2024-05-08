using WordSearchVM = VocabularySheet.Maui.Domain.ViewModels.WordSearchVM;

namespace VocabularySheet.Maui.Domain.Views;

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