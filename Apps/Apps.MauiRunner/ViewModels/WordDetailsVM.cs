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
using WebSources.Common;
using WebSources.Linker;
using WebSources.ReversoContext;
using WebSources.ReversoContext.Entities;

namespace Apps.MauiRunner.ViewModels;

[QueryProperty("Id", "Id")]
public partial class WordDetailsVm : BaseViewModel
{
    private readonly IAudioManager _audioManager;
    private readonly StreamFetcherClient _fetcher;
    private readonly WordClassificationVm _wordClassificationVM;
    [ObservableProperty] long _id = 0;
    [ObservableProperty] WordModel _prevWord = WordModel.Sample;
    [ObservableProperty] WordModel _word = WordModel.Sample;
    [ObservableProperty] WordModel _nextWord = WordModel.Sample;
 
    [ObservableProperty] PublicCambridgeEntry? _originalCambridge = null;
    [ObservableProperty] PublicCambridgeEntry? _translateCambridge = null;
    [ObservableProperty] PublicReversoContextEntry? _reversoContext = null;
    [ObservableProperty] ExternalSourceLink _translatorLink = GoogleTranslatorLinker.Link(WordModel.Sample.Original, WordModel.Sample.OrignalLanguage, WordModel.Sample.TranslationlLanguage);
    
    [ObservableProperty] private Apps.MauiRunner.ViewModels.LinkBoxVm _box;

    public WordDetailsVm(IMediator mediator, ILogger<WordDetailsVm> logger, IAudioManager audioManager, StreamFetcherClient fetcher, WordClassificationVm wordClassificationVM) : base(mediator, logger)
    {
        _audioManager = audioManager;
        _fetcher = fetcher;
        _wordClassificationVM = wordClassificationVM;
        _box = new Apps.MauiRunner.ViewModels.LinkBoxVm(mediator, logger);
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
        ReversoContext = null;

        Word = await Mediator.Send(new GetSpinWord.QueryId()
        {
            Id = wordId
        }, cancellationToken) ?? Word;

        var prevWordTask = Mediator.Send(new GetSpinWord.QueryId()
        {
            Id = (Word.Id - 1)
        }, cancellationToken);

        var nextWordTask = Mediator.Send(new GetSpinWord.QueryId()
        {
            Id = (Word.Id + 1)
        }, cancellationToken);

        var cambridgeTask = Mediator.Send(new GetCambridgePage.Query()
        {
            Word = Word
        }, cancellationToken);


        var localizationTask = Mediator.Send(new GetLanguageWord.Query(), cancellationToken);
        var reversoContextEntryTask = Mediator.Send(new GetReversoContextPage.Query()
        {
            Word = Word
        }, cancellationToken);


        var setBoxTask = Box.SetWord(Word.Original, cancellationToken);
        _wordClassificationVM.TrySet(Word.Original);

        PrevWord = await prevWordTask ?? WordModel.Sample;
        NextWord = await nextWordTask ?? WordModel.Sample;
        var cambridge = await cambridgeTask;
        var localization = await localizationTask;
        var reversoContextEntry = await reversoContextEntryTask;
        await setBoxTask;

        await Task.Run(() =>
        {
            TranslatorLink = GoogleTranslatorLinker.Link(Word.Original, localization.OriginLang, localization.TranslateLang);
            OriginalCambridge = cambridge.GetValueOrDefault(localization.OriginLang) ?? new PublicCambridgeEntry()
            {
                Word = Word.Original,
                Language = localization.OriginLang,
                TranslationLanguage = localization.TranslateLang,
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
                TranslationLanguage = localization.OriginLang,
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
                TranslationLanguage = localization.TranslateLang,
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