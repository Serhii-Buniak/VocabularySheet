using VocabularySheet.Maui.ViewModels;

namespace VocabularySheet.Maui.Views;

public partial class WordDetails : ContentPage
{
    public WordDetails(WordDetailsVM wordDetails)
    {
        InitializeComponent();
        BindingContext = wordDetails;
    }
}