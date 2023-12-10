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

namespace VocabularySheet.Maui.ViewModels;

public partial class WordSearchVM : BaseViewModel
{
    private readonly IAudioManager _audioManager;
    private readonly StreamFetcherClient _fetcher;

    [ObservableProperty] string searchWord = "";
    
    [ObservableProperty] WordModel? word = null;
    [ObservableProperty] PublicCambridgeEntry? originalCambridge = null;
    [ObservableProperty] PublicCambridgeEntry? translateCambridge = null;
    
    public WordSearchVM(IMediator mediator, ILogger<LanguageWordVM> logger, IAudioManager audioManager, StreamFetcherClient fetcher) : base(mediator, logger)
    {
        _audioManager = audioManager;
        _fetcher = fetcher;
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

    [RelayCommand]
    public async Task Search()
    {
        if (string.IsNullOrWhiteSpace(SearchWord))
        {
            return;
        }

        OriginalCambridge = null;
        TranslateCambridge = null;
        Word = await Mediator.Send(new GetSpinWord.QueryName()
        {
            Word = SearchWord
        });
        
        var cambridge = await Mediator.Send(new GetCambridgePage.QuerySimple()
        {
            Word = SearchWord
        });

        var localization = await Mediator.Send(new GetLanguageWord.Query());
        await Task.Delay(50);
        await Task.Run(() =>
        {
            OriginalCambridge = cambridge.GetValueOrDefault(localization.OriginLang) ?? new PublicCambridgeEntry()
            {
                Word = SearchWord,
                Language = localization.OriginLang,
                Link = CambridgeClient.WordLink(Word?.Original ?? SearchWord, localization.OriginLang),
                Content = new CambridgeContent()
                {
                    Title = Word?.Original ?? SearchWord,
                    Blocks = new List<CambridgeWordBlock>(),
                }
            };
            TranslateCambridge = cambridge.GetValueOrDefault(localization.TranslateLang) ?? new PublicCambridgeEntry()
            {
                Word = Word?.Translation ?? SearchWord,
                Language = localization.TranslateLang,
                Link = CambridgeClient.WordLink(Word?.Translation ?? SearchWord, localization.TranslateLang),
                Content = new CambridgeContent()
                {
                    Title = Word?.Translation ?? SearchWord,
                    Blocks = new List<CambridgeWordBlock>(),
                }
            };
        });
    }
}