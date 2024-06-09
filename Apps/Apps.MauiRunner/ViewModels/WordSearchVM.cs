using Application.Common.Cambridge.Queries;
using Application.Common.Commons.Dtos;
using Application.Common.LanguageWords;
using Application.Common.ReversoContext.Queries;
using Application.Common.Words.Queries;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.WebSources;
using Infrastructure.Data.HttpClients;
using MediatR;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using WebSources.CambridgeDictionary;
using WebSources.CambridgeDictionary.Entities;

namespace Apps.MauiRunner.ViewModels;

public partial class WordSearchVm : BaseViewModel
{
    private readonly IAudioManager _audioManager;
    private readonly StreamFetcherClient _fetcher;
    private readonly WordClassificationVm _wordClassificationVM;
    [ObservableProperty] string _searchWord = "";
    
    [ObservableProperty] WordModel? _word = null;
    [ObservableProperty] PublicCambridgeEntry? _originalCambridge = null;
    [ObservableProperty] PublicCambridgeEntry? _translateCambridge = null;
    [ObservableProperty] PublicReversoContextEntry? _reversoContext = null;

    [ObservableProperty] private Apps.MauiRunner.ViewModels.LinkBoxVm _box;

    public WordSearchVm(IMediator mediator, ILogger<WordSearchVm> logger, IAudioManager audioManager, StreamFetcherClient fetcher, WordClassificationVm wordClassificationVM) : base(mediator, logger)
    {
        _audioManager = audioManager;
        _fetcher = fetcher;
        _wordClassificationVM = wordClassificationVM;
        _box = new Apps.MauiRunner.ViewModels.LinkBoxVm(mediator, logger);
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
    public async Task Search(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(SearchWord))
        {
            return;
        }

        OriginalCambridge = null;
        TranslateCambridge = null;
        ReversoContext = null;

        var wordTask = Mediator.Send(new GetSpinWord.QueryName()
        {
            Word = SearchWord
        }, cancellationToken);
        
        var cambridgeTask = Mediator.Send(new GetCambridgePage.QuerySimple()
        {
            Word = SearchWord
        }, cancellationToken);

        var reversoContextEntryTask = Mediator.Send(new GetReversoContextPage.QuerySimple()
        {
            Word = SearchWord
        }, cancellationToken);

        var localizationTask = Mediator.Send(new GetLanguageWord.Query(), cancellationToken);

        var boxTask = Box.SetWord(SearchWord, cancellationToken);

        Word = await wordTask;
        var reversoContextEntry = await reversoContextEntryTask;
        var cambridge = await cambridgeTask;
        var localization = await localizationTask;
        await boxTask;
        _wordClassificationVM.TrySet(SearchWord);
        await Task.Run(() =>
        {
            OriginalCambridge = cambridge.GetValueOrDefault(localization.OriginLang) ?? new PublicCambridgeEntry()
            {
                Word = SearchWord,
                Language = localization.OriginLang,
                TranslationLanguage = localization.TranslateLang,
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
                TranslationLanguage = localization.OriginLang,
                Link = CambridgeClient.WordLink(Word?.Translation ?? SearchWord, localization.TranslateLang),
                Content = new CambridgeContent()
                {
                    Title = Word?.Translation ?? SearchWord,
                    Blocks = new List<CambridgeWordBlock>(),
                }
            };

            ReversoContext = reversoContextEntry;
        }, cancellationToken);
    }
}