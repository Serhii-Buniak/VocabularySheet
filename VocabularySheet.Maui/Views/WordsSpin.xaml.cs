using VocabularySheet.Maui.ViewModels;

namespace VocabularySheet.Maui.Views;

public partial class WordsSpin : ContentPage
{

    public WordsSpin(WordsSpinVM wordsSpinVM)
    {
        InitializeComponent();
        BindingContext = wordsSpinVM;
    }

}