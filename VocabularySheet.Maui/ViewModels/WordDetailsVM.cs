using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using VocabularySheet.Application.Cambridge.Queries;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.LanguageWords;
using VocabularySheet.Application.Words.Queries;
using VocabularySheet.CambridgeDictionary;
using VocabularySheet.CambridgeDictionary.Entities;
using VocabularySheet.Domain.Pages;
using VocabularySheet.Infrastructure.HttpClients;
using VocabularySheet.Maui.Common;

namespace VocabularySheet.Maui.ViewModels;

[QueryProperty("Id", "Id")]
public partial class WordDetailsVM : BaseViewModel
{
    private readonly IAudioManager _audioManager;
    private readonly StreamFetcherClient _fetcher;

    [ObservableProperty] long id = 0;
    [ObservableProperty] WordModel word = WordModel.Sample with
    {
        Id = -1,
    };
    [ObservableProperty] PublicCambridgeEntry? originalCambridge = null;
    [ObservableProperty] PublicCambridgeEntry? translateCambridge = null;
    
    public WordDetailsVM(IMediator mediator, ILogger<LanguageWordVM> logger, IAudioManager audioManager, StreamFetcherClient fetcher) : base(mediator, logger)
    {
        _audioManager = audioManager;
        _fetcher = fetcher;
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
    
    [RelayCommand]
    public async Task PlayAudio(CambridgeAudioLink audioLink, CancellationToken cancellationToken)
    {
        try
        {
            IAudioPlayer player =
                _audioManager.CreatePlayer(await _fetcher.Fetch(audioLink.FullLink(), cancellationToken));

            player.Play();

        }
        catch (Exception)
        {
            // ignored
        }
    }

    public async Task LoadDataAsync()
    {
        if (Id == Word.Id)
        {
            return;
        }
        
        OriginalCambridge = null;
        TranslateCambridge = null;
        Word = await Mediator.Send(new GetSpinWord.Query()
        {
            Id = Id
        }) ?? Word;
        
        var cambridge = await Mediator.Send(new GetCambridgePage.Query()
        {
            Word = Word
        });
        
        var localization = await Mediator.Send(new GetLanguageWord.Query());
        await Task.Delay(25);
        await Task.Run(() =>
        {
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
            TranslateCambridge = cambridge.GetValueOrDefault(localization.TranslateLang) ?? new PublicCambridgeEntry()
            {
                Word = Word.Translation,
                Language = localization.TranslateLang,
                Link = CambridgeClient.WordLink(Word.Translation, localization.TranslateLang),
                Content = new CambridgeContent()
                {
                    Title = Word.Translation,
                    Blocks = new List<CambridgeWordBlock>(),
                }
            };
        });
    }
}