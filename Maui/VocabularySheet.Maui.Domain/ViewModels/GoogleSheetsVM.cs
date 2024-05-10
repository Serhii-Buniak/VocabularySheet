using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CsvHelper;
using MediatR;
using Microsoft.Extensions.Logging;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.GoogleSheets.Commands;
using VocabularySheet.Application.GoogleSheets.Queries;
using VocabularySheet.Application.LanguageWords;
using VocabularySheet.Application.Words.Commands;
using VocabularySheet.Common;
using VocabularySheet.Domain.Exceptions;
using VocabularySheet.Domain.Exceptions.HttpClientExceptions;
using VocabularySheet.Infrastructure.Csv.Models;
using VocabularySheet.Maui.Domain.Common.Events;

namespace VocabularySheet.Maui.Domain.ViewModels;

public partial class GoogleSheetsVM : BaseViewModel
{

    [ObservableProperty, NotifyPropertyChangedFor(nameof(IsGoogleSheetEnable))]
    private string _googleSheetUrl = "https://docs.google.com/spreadsheets";

    [ObservableProperty, NotifyPropertyChangedFor(nameof(IsGoogleSheetEnable))]
    private string _googleScriptUrl = "https://script.google.com/macros/s";

    public event SynchronizeEvent.Handler OnSynchronize = (_, _) => Task.CompletedTask;
    public bool IsGoogleSheetEnable => SetGoogleSheetUrl.Validation.UrlRegex.IsMatch((string)GoogleSheetUrl) && SetGoogleScriptUrl.Validation.UrlRegex.IsMatch((string)GoogleScriptUrl);

    [ObservableProperty]
    private WordLanguageItem _origin = new WordLanguageItem(WordLanguage.En, "English");
    
    [ObservableProperty]
    private WordLanguageItem _translation = new WordLanguageItem(WordLanguage.En, "English");

    public ObservableCollection<WordLanguageItem> LanguageItems { get; init; } = new()
    {
        new WordLanguageItem(WordLanguage.En, "English"),
        new WordLanguageItem(WordLanguage.Ua, "Ukranian"),
        new WordLanguageItem(WordLanguage.Ru, "404?"),
    };

    [ObservableProperty, NotifyPropertyChangedFor(nameof(IsErrorVisible))]
    private string? _error;

    public bool IsErrorVisible => Error != null;


    public GoogleSheetsVM(IMediator mediator, ILogger<GoogleSheetsVM> logger) : base(mediator, logger)
    {
    }

        
    [RelayCommand]
    private async Task Save()
    {
        await Mediator.Send(new SetLanguageWord.Command()
        {
            WordLang = Origin.Key,
            TranslateLang = Translation.Key,
        });
    }
    
    [RelayCommand]
    private async Task Synchronize()
    {
        try
        {
            await Mediator.Send(new SetGoogleSheetUrl.Command() { Url = GoogleSheetUrl });
            await Mediator.Send(new SetGoogleScriptUrl.Command() { Url = GoogleScriptUrl });
            await Mediator.Send(new SynchronizeWords.Command());
            await OnSynchronize.Invoke(this, new SynchronizeEvent.Args());
            
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
        
        var configuration = await Mediator.Send(new GetLanguageWord.Query());
        Origin = LanguageItems.First(l => l.Key == configuration.OriginLang);
        Translation = LanguageItems.First(l => l.Key == configuration.TranslateLang);
    }
}
