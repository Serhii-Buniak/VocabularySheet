using VocabularySheet.Maui.ViewModels;

namespace VocabularySheet.Maui.Views;

public partial class LanguageWord : ContentPage
{
    private readonly LanguageWordVM _languageWordVm;

    public LanguageWord(LanguageWordVM languageWordVm)
    {
        InitializeComponent();
        _languageWordVm = languageWordVm;
        BindingContext = languageWordVm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _languageWordVm.LoadDataAsync();
    }
}