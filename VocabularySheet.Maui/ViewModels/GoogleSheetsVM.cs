using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CsvHelper;
using MediatR;
using Microsoft.Extensions.Logging;
using VocabularySheet.Application.GoogleSheets.Commands;
using VocabularySheet.Application.GoogleSheets.Queries;
using VocabularySheet.Application.Words.Commands;
using VocabularySheet.Domain.Exceptions;
using VocabularySheet.Domain.Exceptions.HttpClientExceptions;
using VocabularySheet.Infrastructure.Csv.Models;

namespace VocabularySheet.Maui.ViewModels;

public partial class GoogleSheetsVM : BaseViewModel
{

    [ObservableProperty, NotifyPropertyChangedFor(nameof(IsGoogleSheetEnable))]
    private string googleSheetUrl = "https://docs.google.com/spreadsheets";

    [ObservableProperty, NotifyPropertyChangedFor(nameof(IsGoogleSheetEnable))]
    private string googleScriptUrl = "https://script.google.com/macros/s";

    public bool IsGoogleSheetEnable => SetGoogleSheetUrl.Validation.UrlRegex.IsMatch(GoogleSheetUrl) && SetGoogleScriptUrl.Validation.UrlRegex.IsMatch(GoogleScriptUrl);


    [ObservableProperty, NotifyPropertyChangedFor(nameof(IsErrorVisible))]
    private string? error;

    public bool IsErrorVisible => Error != null;


    public GoogleSheetsVM(IMediator mediator, ILogger<GoogleSheetsVM> logger) : base(mediator, logger)
    {
    }

    [RelayCommand]
    private async Task Synchronize()
    {
        try
        {
            await Mediator.Send(new SetGoogleSheetUrl.Command() { Url = GoogleSheetUrl });
            await Mediator.Send(new SetGoogleScriptUrl.Command() { Url = GoogleScriptUrl });
            await Mediator.Send(new SynchronizeWords.Command());
            Error = null;
        }
        catch (FluentValidationException)
        {
            Error = $"Google sheet url invalid";
        }
        catch (HeaderValidationException)
        {
            var props = typeof(WordCsv).GetProperties().Select(prop => prop.Name);
            Error = $"Google sheet have to contain this columns: {string.Join(", ", props)}.";
        }
        catch (HttpClientNotFoundException)
        {
            Error = $"Google sheet not found.";
        }
        catch (HttpClientInternetServerException)
        {
            Error = $"No internet connection.";
        }
        catch (HttpClientException ex)
        {
            Error = ex.Message;
        }
    }

    public async Task LoadDataAsync()
    {
        string sheetUrl = await Mediator.Send(new GetGoogleSheetUrl.Query());
        string scriptUrl = await Mediator.Send(new GetGoogleScriptUrl.Query());

        if (!string.IsNullOrWhiteSpace(sheetUrl))
        {
            GoogleSheetUrl = sheetUrl;
        }

        if (!string.IsNullOrWhiteSpace(scriptUrl))
        {
            GoogleScriptUrl = scriptUrl;
        }
    }

}
