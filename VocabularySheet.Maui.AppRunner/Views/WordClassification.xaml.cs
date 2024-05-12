using WordClassificationVm = VocabularySheet.Maui.AppRunner.ViewModels.WordClassificationVm;

namespace VocabularySheet.Maui.AppRunner.Views;

public partial class WordClassification : ContentPage
{
    private readonly WordClassificationVm _wordClassificationVm;

    public WordClassification(WordClassificationVm wordClassificationVm)
    {
        InitializeComponent();
        _wordClassificationVm = wordClassificationVm;
        BindingContext = _wordClassificationVm;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _wordClassificationVm.LoadDataAsync();
    }
}