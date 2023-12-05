using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using VocabularySheet.Application.Cambridge.Queries;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.LanguageWords;
using VocabularySheet.Application.Words.Queries;
using VocabularySheet.CambridgeDictionary;
using VocabularySheet.CambridgeDictionary.Entities;
using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Domain.Pages;
using VocabularySheet.Maui.Common;

namespace VocabularySheet.Maui.ViewModels;

[QueryProperty("Id", "Id")]
public partial class WordDetailsVM : BaseViewModel
{
    [ObservableProperty] long id = 0;
    [ObservableProperty] WordModel word = WordModel.Sample;
    [ObservableProperty] PublicCambridgeEntry? originalCambridge = null;
    [ObservableProperty] PublicCambridgeEntry? translateCambridge = null;
    
    public WordDetailsVM(IMediator mediator, ILogger<LanguageWordVM> logger) : base(mediator, logger)
    {
    }
    
    [RelayCommand]
    public async Task GoBack()
    {
        await Shell.Current.GoToBack();
    }
    
    [RelayCommand]
    public async Task OpenLink(string link)
    {
        if (!string.IsNullOrWhiteSpace(link))
        {
            await Launcher.OpenAsync(link);
        }
    }

    public async Task LoadDataAsync()
    {
        Word = await Mediator.Send(new GetSpinWord.Query()
        {
            Id = Id
        }) ?? Word;

        var cambridge = await Mediator.Send(new GetCambridgePage.Query()
        {
            Word = Word
        });
        
        var localization = await Mediator.Send(new GetLanguageWord.Query());

        OriginalCambridge = cambridge.GetValueOrDefault(localization.OriginLang) ?? new PublicCambridgeEntry()
        {
            Word = Word.Original,
            Language = localization.OriginLang,
            Link = CambridgeClient.WordLink(Word.Original, localization.OriginLang),
            Content = new CambridgeContent()
            {
                Title = Word.Original,
                Blocks = new List<CambridgeWordBlock>(),
            }
        };
        TranslateCambridge = cambridge.GetValueOrDefault(localization.TranslateLang);
    }
}