using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Windows.Input;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.Words.Queries;
using VocabularySheet.Maui.Services;

namespace VocabularySheet.Maui.ViewModels;

public partial class WordsSpinVM : BaseViewModel
{
    private static readonly Random rng = new();
    private readonly ITextToSpeechService _textToSpeechService;

    private GetSpinWords.Query QueryParameters => new()
    {
        FromIndex = FromIndex,
        ToIndex = ToIndex,
        IsOriginalMode = IsOriginalMode,
        IsTranslationMode = IsTranslationMode,
    };

    [ObservableProperty, NotifyPropertyChangedFor(nameof(StartCommandCanExecute))]
    private int fromIndex = 1;
    [ObservableProperty]
    private int toIndex = 1;
    [ObservableProperty, NotifyPropertyChangedFor(nameof(StartCommandCanExecute))]
    private bool isOriginalMode = true;
    [ObservableProperty, NotifyPropertyChangedFor(nameof(StartCommandCanExecute))]
    private bool isTranslationMode = false;


    [ObservableProperty, NotifyPropertyChangedFor(nameof(StartCommandCanExecute))]
    private bool isStarted = false;

    [ObservableProperty]
    private bool isPaused;

    [ObservableProperty]
    private WordSpinDto word = WordSpinDto.Sample;

    [ObservableProperty]
    private bool isDescriptionVisible = true;

    [ObservableProperty]
    private bool isTranslationVisible = true;

    [ObservableProperty]
    private double delayInSeconds = 1;

    public WordsSpinVM(IMediator mediator, ILogger<WordsSpinVM> logger, ITextToSpeechService textToSpeechService) : base(mediator, logger)
    {
        _textToSpeechService = textToSpeechService;
    }

    public async Task ResetIndex()
    {
        int max = await Mediator.Send(new GetWordsSpinMaxIndex.Query());

        if (max == 0)
        {
            FromIndex = 0;
            ToIndex = 0;
        }
        else
        {
            ToIndex = max;
        }

    }

    public bool StartCommandCanExecute
    {
        get
        {
            if (IsStarted)
            {
                return true;
            }

            if (FromIndex == 0)
            {
                return false;
            }

            if (IsOriginalMode || IsTranslationMode)
            {
                return true;
            }

            return false;
        }
    }




    [RelayCommand(IncludeCancelCommand = true, CanExecute = nameof(StartCommandCanExecute))]
    public async Task Start(CancellationToken cancellationToken)
    {
        IsStarted = true;
        try
        {
            IEnumerable<WordSpinDto> words = await GetWordsListAsync(cancellationToken);

            foreach (WordSpinDto word in words)
            {
                await WaitPause(cancellationToken);
                await NextWord(word, cancellationToken);
            }
        }
        catch (TaskCanceledException ex)
        {
            Logger.LogInformation("StartCommand throw {@Type} with message {@Message}:", ex.GetType().Name, ex.Message);
        }
        finally
        {
            IsPaused = false;
            IsStarted = false;
        }
    }

    [RelayCommand]
    public void Pause()
    {
        IsPaused = true;
    }

    [RelayCommand]
    public void Resume()
    {
        IsPaused = false;
    }


    [RelayCommand]
    public async Task TextSpeech()
    {
        LocaleAndText value = await _textToSpeechService.GetLocaleAndTextForTextAsync(Word.Original);

        await TextToSpeech.SpeakAsync(value.Text, new SpeechOptions()
        {
            Locale = value.Locale,
        });
    }

    private async Task NextWord(WordSpinDto word, CancellationToken cancellationToken)
    {
        Word = word;
        await Task.Delay(TimeSpan.FromSeconds(DelayInSeconds), cancellationToken);
    }

    private async Task WaitPause(CancellationToken cancellationToken)
    {
        while (IsPaused)
        {
            await Task.Delay(100, cancellationToken);
        }
    }
    private async Task<IEnumerable<WordSpinDto>> GetWordsListAsync(CancellationToken cancellationToken)
    {
        var words = await Mediator.Send(QueryParameters, cancellationToken);
        return words.OrderBy(a => rng.Next()).ToList();
    }
}
