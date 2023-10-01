using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.Words.Queries;
using VocabularySheet.Domain.Extensions;
using VocabularySheet.Maui.Common.Events;
using VocabularySheet.Maui.Common.Services;

namespace VocabularySheet.Maui.ViewModels;

public partial class WordsSpinVM : BaseViewModel
{
    public event ClipboardEvent.Handler OnClipboard = (_, _) => Task.CompletedTask;

    private readonly TextToSpeechService _textToSpeechService;
    public int MaxIndex { get; set; }

    private GetSpinWords.Query QueryParameters => new()
    {
        FromIndex = FromIndex,
        ToIndex = ToIndex,
        IsOriginalMode = IsOriginalMode,
        IsTranslationMode = IsTranslationMode,
    };

    [ObservableProperty, NotifyPropertyChangedFor(nameof(StartCommandCanExecute))]
    private int _fromIndex;
    [ObservableProperty]
    private int _toIndex;

    public bool IsIndexesValid => FromIndex != 0 && ToIndex != 0; 
    
    [ObservableProperty, NotifyPropertyChangedFor(nameof(StartCommandCanExecute))]
    private bool _isOriginalMode = true;
    [ObservableProperty, NotifyPropertyChangedFor(nameof(StartCommandCanExecute))]
    private bool _isTranslationMode = false;


    [ObservableProperty, NotifyPropertyChangedFor(nameof(StartCommandCanExecute))]
    private bool _isStarted = false;

    [ObservableProperty]
    private bool _isPaused;

    [ObservableProperty]
    private WordSpinDto _word = WordSpinDto.Sample;

    [ObservableProperty]
    private bool _isDescriptionVisible = true;

    [ObservableProperty]
    private bool _isTranslationVisible = true;

    [ObservableProperty]
    private double _delayInSeconds = 1;

    public WordsSpinVM(IMediator mediator, ILogger<WordsSpinVM> logger, TextToSpeechService textToSpeechService) : base(mediator, logger)
    {
        _textToSpeechService = textToSpeechService;
    }

    public async Task SetMaxIndex()
    {
        MaxIndex = await Mediator.Send(new GetWordsSpinMaxIndex.Query(), CancellationToken.None);
    }
    
    public async Task HandleSynchronize()
    {
        await SetMaxIndex();
       
        if (!IsIndexesValid || MaxIndex < FromIndex)
        {
            ResetIndex();
        }
        else
        {
            ShiftFromLine(0);
            ShiftToLine(0);
        }
    }
    
    public void ResetSpin()
    {
        StartCancelCommand.Execute(this);
    }

    public void ResetIndex()
    {
        if (MaxIndex == 0)
        {
            FromIndex = 0;
            ToIndex = 0;
        }
        else
        {
            FromIndex = 1;
            ToIndex = MaxIndex;  
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
            List<WordSpinDto> words = await GetWordsListAsync(cancellationToken);

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
    public async Task CopyToClipboard(string text)
    {
        await Clipboard.Default.SetTextAsync(text);
        await OnClipboard.Invoke(this, new ClipboardEvent.Args()
        {
            Text = text,
        });
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

    [RelayCommand]
    public void ShiftDelay(double shiftDelay)
    {
        double newDelay = DelayInSeconds + shiftDelay;

        const double min = 0.2;
        bool biggerThanMin = newDelay < min;
        if (biggerThanMin)
        {
            newDelay = min;
        }

        DelayInSeconds = Math.Round(newDelay, 2);
    }

    [RelayCommand]
    public void ShiftFromLine(int shiftFromLine)
    {
        int newFromLine = FromIndex + shiftFromLine;

        const int min = 1;
        bool lessThanMin = newFromLine < min;
        if (lessThanMin)
        {
            newFromLine = min;
        }

        bool biggerThanToLine = newFromLine > ToIndex;
        if (biggerThanToLine)
        {
            newFromLine = ToIndex;
        }

        FromIndex = newFromLine;
    }

    [RelayCommand]
    public void ShiftToLine(int shiftToLine)
    {
        int newToLine = ToIndex + shiftToLine;

        bool lessThanFromLine = newToLine < FromIndex;
        if (lessThanFromLine)
        {
            newToLine = FromIndex;
        }

        int max = MaxIndex;
        bool biggerThanMax = newToLine > max;
        if (biggerThanMax)
        {
            newToLine = max;
        }

        ToIndex = newToLine;
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
    private async Task<List<WordSpinDto>> GetWordsListAsync(CancellationToken cancellationToken)
    {
        var words = await Mediator.Send(QueryParameters, cancellationToken);
        return words.OrderRandom().ToList();
    }
}

