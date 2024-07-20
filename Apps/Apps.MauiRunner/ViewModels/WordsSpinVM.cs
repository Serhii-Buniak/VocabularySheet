using System.Collections.ObjectModel;
using System.ComponentModel;
using Application.Common.Commons.Dtos;
using Application.Common.Words.Commands;
using Application.Common.Words.Queries;
using Apps.MauiRunner.Common.Events;
using Apps.MauiRunner.Common.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.WordModels;
using MediatR;
using Microsoft.Extensions.Logging;
using Tools.Common.Extensions;

namespace Apps.MauiRunner.ViewModels;

public partial class WordsSpinVM : BaseViewModel
{
    public event ClipboardEvent.Handler OnClipboard = (_, _) => Task.CompletedTask;

    private readonly MauiTextToSpeechService _mauiTextToSpeechService;
    public int MaxIndex { get; set; }

    public GetSpinWords.Query QueryParameters => new ()
    {
        FromIndex = FromIndex,
        ToIndex = ToIndex,
        IsOriginalMode = IsOriginalMode,
        IsTranslationMode = IsTranslationMode,
        SelectedCategory = SelectedCategoryItem.Key,
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
    private WordModel _word = WordModel.Sample;

    [ObservableProperty]
    private bool _isDescriptionVisible = true;

    [ObservableProperty]
    private bool _isTranslationVisible = true;
    
    [ObservableProperty]
    private bool _runAudioOnStart = true;
    
    [ObservableProperty]
    private bool _runAudioOnEnd = false;
    
    [ObservableProperty]
    private bool _hasHiddenInRange = false;

    [ObservableProperty]
    private double _delayInSeconds = 1;

    [ObservableProperty]
    private CategoryItem _selectedCategoryItem = new CategoryItem(null, "Not Selected");
    public ObservableCollection<CategoryItem> CategoryItems { get; init; } = new()
    {
        new CategoryItem(null, "Not Selected"),
        new CategoryItem(Category.Red, "Red"),
        new CategoryItem(Category.Green, "Green"),
        new CategoryItem(Category.Yellow, "Yellow"),
        new CategoryItem(Category.Orange, "Orange"),
        new CategoryItem(Category.Purple, "Purple"),
        new CategoryItem(Category.Pink, "Pink"),
    };
    
    public WordsSpinVM(IMediator mediator, ILogger<WordsSpinVM> logger, MauiTextToSpeechService mauiTextToSpeechService) : base(mediator, logger)
    {
        _mauiTextToSpeechService = mauiTextToSpeechService;
    }

    public async Task SetMaxIndex()
    {
        MaxIndex = await Mediator.Send(new GetWordsSpinMaxIndex.Query()
        {
            Category = SelectedCategoryItem.Key
        }, CancellationToken.None);
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
        PauseCommand.Execute(this);
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
            List<WordModel> words = await GetWordsListAsync(cancellationToken);

            foreach (WordModel word in words.Where(w => !w.Hidden))
            {
                await WaitPause(cancellationToken);
                await NextWord(word, cancellationToken);
            }
        }
        catch (TaskCanceledException ex)
        {
            Logger.LogInformation("StartCommand throw {@Label} with message {@Message}:", ex.GetType().Name, ex.Message);
        }
        finally
        {
            IsPaused = false;
            IsStarted = false;
        }
    }

    [RelayCommand]
    public async Task OpenWord()
    {
        ResetSpin();
        await Shell.Current.GoToWordDetails(Word.Id);
    }
    
    [RelayCommand]
    public async Task SetHidden(CancellationToken cancellationToken)
    {
        await Mediator.Send(new WordHidden.SetHidden
        {
            Id = Word.Id
        }, cancellationToken);
        
        Word = Word with
        {
            Hidden = true
        };
    }
    
    [RelayCommand]
    public async Task SetNotHidden(CancellationToken cancellationToken)
    {
        await Mediator.Send(new WordHidden.SetNotHidden()
        {
            FromIndex = QueryParameters.FromIndex,
            ToIndex = QueryParameters.ToIndex,
            SelectedCategory = QueryParameters.SelectedCategory
        }, cancellationToken);

        HasHiddenInRange = false;
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
        await Clipboard.SetTextAsync(text);
        await OnClipboard.Invoke(this, new ClipboardEvent.Args()
        {
            Text = text,
        });
    }
    
    [RelayCommand]
    public async Task TextSpeech(CancellationToken cancellationToken)
    {
        await _mauiTextToSpeechService.RunVoice(Word.OriginalWord(), cancellationToken);
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

    protected override async void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName == nameof(SelectedCategoryItem))
        {
            await SetMaxIndex();
            ResetIndex();
        }
        
        Console.WriteLine();
    }

    private async Task NextWord(WordModel word, CancellationToken cancellationToken)
    {
        Word = word;

        Task[] beforeDelayTasks =
        [
            Task.Run(async () =>
            {
                var description = await Mediator.Send(new GetWordDetails.QueryWordDescription
                {
                    Id = word.Id
                }, cancellationToken);

                if (description != null && description.Text != Word.Description)
                {
                    Word = Word with
                    {
                        Description = description.Text
                    };
                }
            }, cancellationToken),
            Task.Run(async () =>
            {
                if (RunAudioOnStart)
                {
                    await _mauiTextToSpeechService.RunVoice(Word.OriginalWord(), cancellationToken);
                }
            }, cancellationToken),
        ];
        
        await Task.WhenAll(beforeDelayTasks);
        
        await Task.Delay(TimeSpan.FromSeconds(DelayInSeconds), cancellationToken);
        
        if (RunAudioOnEnd)
        {
            await _mauiTextToSpeechService.RunVoice(Word.TranslationWord(), cancellationToken);
        }
    }

    private async Task WaitPause(CancellationToken cancellationToken)
    {
        while (IsPaused)
        {
            await Task.Delay(100, cancellationToken);
        }
    }
    private async Task<List<WordModel>> GetWordsListAsync(CancellationToken cancellationToken)
    {
        IEnumerable<WordModel> words = await Mediator.Send(QueryParameters, cancellationToken);
        var list = words.OrderRandom().ToList();
        HasHiddenInRange = list.Any(w => w.Hidden);
            
        return list;
    }
}