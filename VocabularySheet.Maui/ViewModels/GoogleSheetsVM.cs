using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CsvHelper;
using MediatR;
using System.Linq;
using VocabularySheet.Application.GoogleSheets.Commands;
using VocabularySheet.Application.GoogleSheets.Queries;
using VocabularySheet.Application.Words.Commands;
using VocabularySheet.Application.Words.Queries;
using VocabularySheet.Domain.Exceptions;
using VocabularySheet.Domain.Exceptions.HttpClientExceptions;
using VocabularySheet.Infrastructure.Csv.Models;

namespace VocabularySheet.Maui.ViewModels;

public partial class GoogleSheetsVM : BaseViewModel
{
    [ObservableProperty, NotifyPropertyChangedFor(nameof(IsGoogleSheetEnable))]
    private string googleSheetUrl = "https://docs.google.com/spreadsheets";

    public bool IsGoogleSheetEnable => SetGoogleSheetUrl.Validation.UrlRegex.IsMatch(GoogleSheetUrl);


    [ObservableProperty, NotifyPropertyChangedFor(nameof(IsErrorVisible))]
    private string? error;

    public bool IsErrorVisible => Error != null;


    public GoogleSheetsVM(IMediator mediator) : base(mediator)
    {

    }

    [RelayCommand]
    private async Task Synchronize()
    {
        try
        {
            await Mediator.Send(new SetGoogleSheetUrl.Command() { Url = GoogleSheetUrl });
            await Mediator.Send(new SynchronizeWords.Command());
            Error = null;
        }
        catch(HeaderValidationException)
        {
            var props = typeof(WordCsv).GetProperties().Select(prop => prop.Name);
            Error = $"Google sheet have to contain this columns: {string.Join(", " ,props)}.";
        }  
        catch(HttpClientNotFoundException)
        {
            Error = $"Google sheet not found.";
        }   
        catch(HttpClientInternetServerException)
        {
            Error = $"No internet connection.";
        }
        catch (GoogleSheetNotPublicException ex)
        {
            Error = ex.Message;
        }    
    }

    public async Task LoadDataAsync()
    {
        string url = await Mediator.Send(new GetGoogleSheetUrl.Query());

        if (!string.IsNullOrWhiteSpace(url))
        {
            GoogleSheetUrl = url;
        }
    }

}
