using GoogleSheetsVM = VocabularySheet.Maui.AppRunner.ViewModels.GoogleSheetsVM;

namespace VocabularySheet.Maui.AppRunner.Views;

public partial class GoogleSheets : ContentPage
{
    private readonly GoogleSheetsVM _googleSheetsVm;

    public GoogleSheets(GoogleSheetsVM googleSheetsVm)
    {
        InitializeComponent();
        _googleSheetsVm = googleSheetsVm;
        BindingContext = googleSheetsVm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _googleSheetsVm.LoadDataAsync();
    }
}