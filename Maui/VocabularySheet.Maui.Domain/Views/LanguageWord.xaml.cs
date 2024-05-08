using LanguageWordVM = VocabularySheet.Maui.Domain.ViewModels.LanguageWordVM;

namespace VocabularySheet.Maui.Domain.Views;

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