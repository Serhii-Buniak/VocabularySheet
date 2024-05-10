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
using VocabularySheet.Maui.Domain.Common;
using WebSources.CambridgeDictionary;
using WebSources.CambridgeDictionary.Entities;
using WebSources.Common;
using WebSources.ReversoContext;
using WebSources.ReversoContext.Entities;

namespace VocabularySheet.Maui.Domain.ViewModels;

[QueryProperty("Id", "Id")]
public partial class WordDetailsVm : BaseViewModel
{
    private readonly IAudioManager _audioManager;
    private readonly StreamFetcherClient _fetcher;

    [ObservableProperty] long _id = 0;
    [ObservableProperty] WordModel _prevWord = WordModel.Sample;
    [ObservableProperty] WordModel _word = WordModel.Sample;
    [ObservableProperty] WordModel _nextWord = WordModel.Sample;
 
    [ObservableProperty] PublicCambridgeEntry? _originalCambridge = null;
    [ObservableProperty] PublicCambridgeEntry? _translateCambridge = null;
    [ObservableProperty] PublicReversoContextEntry? _reversoContext = null;
    [ObservableProperty] ExternalSourceLink _translatorLink = GoogleTranslatorLinker.Link(WordModel.Sample.Original, WordModel.Sample.OrignalLanguage, WordModel.Sample.TranslationlLanguage);
    
    [ObservableProperty] private LinkBoxVm _box;

    public WordDetailsVm(IMediator mediator, ILogger<WordDetailsVm> logger, IAudioManager audioManager, StreamFetcherClient fetcher) : base(mediator, logger)
    {
        _audioManager = audioManager;
        _fetcher = fetcher;
        _box = new LinkBoxVm(mediator, logger);
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
    public async Task PlayAudio(IHaveAudioLink audioLink, CancellationToken cancellationToken)
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
        await LoadWord(Id, CancellationToken.None);
    }

    [RelayCommand]
    public async Task LoadWord(long wordId, CancellationToken cancellationToken)
    {
        if (wordId == Word.Id)
        {
            return;
        }

        OriginalCambridge = null;
        TranslateCambridge = null;
        ReversoContext = null;

        Word = await Mediator.Send(new GetSpinWord.QueryId()
        {
            Id = wordId
        }, cancellationToken) ?? Word;

        PrevWord = await Mediator.Send(new GetSpinWord.QueryId()
        {
            Id = (Word.Id - 1)
        }, cancellationToken) ?? WordModel.Sample;

        NextWord = await Mediator.Send(new GetSpinWord.QueryId()
        {
            Id = (Word.Id + 1)
        }, cancellationToken) ?? WordModel.Sample;

        var cambridge = await Mediator.Send(new GetCambridgePage.Query()
        {
            Word = Word
        }, cancellationToken);

        var localization = await Mediator.Send(new GetLanguageWord.Query(), cancellationToken);

        var reversoContextEntry = await Mediator.Send(new GetReversoContextPage.Query()
        {
            Word = Word
        }, cancellationToken);
        
        await Box.SetWord(Word.Original, cancellationToken);
        await Task.Run(() =>
        {
            TranslatorLink = GoogleTranslatorLinker.Link(Word.Original, localization.OriginLang, localization.TranslateLang);
            OriginalCambridge = cambridge.GetValueOrDefault(localization.OriginLang) ?? new PublicCambridgeEntry()
            {
                Word = Word.Original,
                Language = localization.OriginLang,
                TraslationLanguage = localization.TranslateLang,
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
                TraslationLanguage = localization.OriginLang,
                Link = CambridgeClient.WordLink(Word.Translation, localization.TranslateLang),
                Content = new CambridgeContent()
                {
                    Title = Word.Translation,
                    Blocks = new List<CambridgeWordBlock>(),
                }
            };
            ReversoContext = reversoContextEntry ?? new PublicReversoContextEntry()
            {
                Word = Word.Original,
                Language = localization.OriginLang,
                TraslationLanguage = localization.TranslateLang,
                Link = ReversoContextClient.WordLink(Word.Original, localization.OriginLang, localization.TranslateLang),
                Content = new ReversoContextContent()
                {
                    Title = Word.Original, 
                    CategoryGroups = new List<ReversoContextCetegoryGroup>(),
                    Examples= new List<ReversoContextExample>(),
                }
            };
        }, cancellationToken);
    }
}