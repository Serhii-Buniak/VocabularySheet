using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using VocabularySheet.Application.Cambridge.Queries;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.LanguageWords;
using VocabularySheet.Application.ReversoContext.Queries;
using VocabularySheet.Application.Words.Queries;
using VocabularySheet.Domain.Pages;
using VocabularySheet.Infrastructure.HttpClients;
using WebSources.CambridgeDictionary;
using WebSources.CambridgeDictionary.Entities;
using WebSources.Common;

namespace VocabularySheet.Maui.Domain.ViewModels;

public partial class WordSearchVM : BaseViewModel
{
    private readonly IAudioManager _audioManager;
    private readonly StreamFetcherClient _fetcher;

    [ObservableProperty] string _searchWord = "";
    
    [ObservableProperty] WordModel? _word = null;
    [ObservableProperty] PublicCambridgeEntry? _originalCambridge = null;
    [ObservableProperty] PublicCambridgeEntry? _translateCambridge = null;
    [ObservableProperty] PublicReversoContextEntry? _reversoContext = null;
    [ObservableProperty] GoogleTranslatorLink _linkTranslate = GoogleTranslatorLinker.Link(WordModel.Sample.Original, WordModel.Sample.OrignalLanguage, WordModel.Sample.TranslationlLanguage);
    
    public WordSearchVM(IMediator mediator, ILogger<Domain.ViewModels.WordSearchVM> logger, IAudioManager audioManager, StreamFetcherClient fetcher) : base(mediator, logger)
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
        ReversoContext = null;
        Word = await Mediator.Send(new GetSpinWord.QueryName()
        {
            Word = SearchWord
        });
        
        var cambridge = await Mediator.Send(new GetCambridgePage.QuerySimple()
        {
            Word = SearchWord
        });
        
        var reversoContextEntry = await Mediator.Send(new GetReversoContextPage.QuerySimple()
        {
            Word = SearchWord
        });

        var localization = await Mediator.Send(new GetLanguageWord.Query());

        await Task.Run(() =>
        {
            LinkTranslate = GoogleTranslatorLinker.Link(SearchWord, localization.OriginLang, localization.TranslateLang);
            OriginalCambridge = cambridge.GetValueOrDefault(localization.OriginLang) ?? new PublicCambridgeEntry()
            {
                Word = SearchWord,
                Language = localization.OriginLang,
                TraslationLanguage = localization.TranslateLang,
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
                TraslationLanguage = localization.OriginLang,
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