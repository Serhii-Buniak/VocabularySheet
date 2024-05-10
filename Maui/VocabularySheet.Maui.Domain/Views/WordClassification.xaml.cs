using VocabularySheet.Maui.Domain.ViewModels;

namespace VocabularySheet.Maui.Domain.Views;

public partial class WordClassification : ContentPage
{
    private readonly WordClassificationVm _wordClassificationVm;

    public WordClassification(WordClassificationVm wordClassificationVm)
    {
        InitializeComponent();
        _wordClassificationVm = wordClassificationVm;
        BindingContext = _wordClassificationVm;
    }
}