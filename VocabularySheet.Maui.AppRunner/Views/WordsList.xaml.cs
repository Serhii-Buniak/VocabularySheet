using WordsListVM = VocabularySheet.Maui.AppRunner.ViewModels.WordsListVM;

namespace VocabularySheet.Maui.AppRunner.Views;

public partial class WordsList : ContentPage
{
    private readonly WordsListVM _wordsListVm;

    public WordsList(WordsListVM wordsListVm)
    {
        InitializeComponent();
        _wordsListVm = wordsListVm;
        BindingContext = wordsListVm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Task.Run(async () => await _wordsListVm.LoadDataAsync(CancellationToken.None));
    }
}