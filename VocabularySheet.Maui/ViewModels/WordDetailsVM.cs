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
using VocabularySheet.CambridgeDictionary;
using VocabularySheet.CambridgeDictionary.Entities;
using VocabularySheet.Domain.Pages;
using VocabularySheet.Infrastructure.HttpClients;
using VocabularySheet.Maui.Common;
using VocabularySheet.Parsing.Common;
using VocabularySheet.ReversoContext;
using VocabularySheet.ReversoContext.Entities;

namespace VocabularySheet.Maui.ViewModels;

[QueryProperty("Id", "Id")]
public partial class WordDetailsVM : BaseViewModel
{
    private readonly IAudioManager _audioManager;
    private readonly StreamFetcherClient _fetcher;

    [ObservableProperty] long id = 0;
    [ObservableProperty] WordModel? prevWord = null;
    [ObservableProperty] WordModel word = WordModel.Sample;
    [ObservableProperty] WordModel? nextWord = null;
 
    [ObservableProperty] PublicCambridgeEntry? originalCambridge = null;
    [ObservableProperty] PublicCambridgeEntry? translateCambridge = null;
    [ObservableProperty] PublicReversoContextEntry? reversoContext = null;
    [ObservableProperty] GoogleTranslatorLink translatorLink = GoogleTranslatorLinker.Link(WordModel.Sample.Original, WordModel.Sample.OrignalLanguage, WordModel.Sample.TranslationlLanguage);

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
        await LoadWord(Id);
    }

    [RelayCommand]
    public async Task LoadWord(long wordId)
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
        }) ?? Word;

        PrevWord = await Mediator.Send(new GetSpinWord.QueryId()
        {
            Id = (Word.Id - 1)
        });

        NextWord = await Mediator.Send(new GetSpinWord.QueryId()
        {
            Id = (Word.Id + 1)
        });

        var cambridge = await Mediator.Send(new GetCambridgePage.Query()
        {
            Word = Word
        });

        var localization = await Mediator.Send(new GetLanguageWord.Query());

        var reversoContextEntry = await Mediator.Send(new GetReversoContextPage.Query()
        {
            Word = Word
        });
        
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
        });
    }
}