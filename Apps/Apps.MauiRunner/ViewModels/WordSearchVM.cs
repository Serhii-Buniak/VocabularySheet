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

    [ObservableProperty] string _searchWord = "";
    
    [ObservableProperty] WordModel? _word = null;
    [ObservableProperty] PublicCambridgeEntry? _originalCambridge = null;
    [ObservableProperty] PublicCambridgeEntry? _translateCambridge = null;
    [ObservableProperty] PublicReversoContextEntry? _reversoContext = null;

    [ObservableProperty] private Apps.MauiRunner.ViewModels.LinkBoxVm _box;

    public WordSearchVm(IMediator mediator, ILogger<WordSearchVm> logger, IAudioManager audioManager, StreamFetcherClient fetcher) : base(mediator, logger)
    {
        _audioManager = audioManager;
        _fetcher = fetcher;
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
        using var stream = await FileSystem.OpenAppPackageFileAsync("AboutAssets.txt");

        using var reader = new StreamReader(stream);
        var data = await reader.ReadToEndAsync(cancellationToken);
        if (string.IsNullOrWhiteSpace(SearchWord))
        {
            return;
        }

        OriginalCambridge = null;
        TranslateCambridge = null;
        ReversoContext = null;
        Word = await Mediator.Send(new GetSpinWord.QueryName()
        {
            Word = SearchWord
        }, cancellationToken);
        
        var cambridge = await Mediator.Send(new GetCambridgePage.QuerySimple()
        {
            Word = SearchWord
        }, cancellationToken);
        
        var reversoContextEntry = await Mediator.Send(new GetReversoContextPage.QuerySimple()
        {
            Word = SearchWord
        }, cancellationToken);

        var localization = await Mediator.Send(new GetLanguageWord.Query(), cancellationToken);
        await Box.SetWord(SearchWord, cancellationToken);
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
        });
    }
}