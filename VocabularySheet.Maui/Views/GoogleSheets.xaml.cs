using VocabularySheet.Maui.ViewModels;

namespace VocabularySheet.Maui.Views;

public partial class GoogleSheets : ContentPage
{
    private readonly GoogleSheetsVM _googleSheetsVM;

    public GoogleSheets(GoogleSheetsVM googleSheetsVM)
    {
        InitializeComponent();
        _googleSheetsVM = googleSheetsVM;
        BindingContext = googleSheetsVM;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _googleSheetsVM.LoadDataAsync();
    }
}