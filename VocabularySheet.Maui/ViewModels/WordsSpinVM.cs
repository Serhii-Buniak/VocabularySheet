using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.Words.Queries;

namespace VocabularySheet.Maui.ViewModels;

public partial class WordsSpinVM : BaseViewModel
{
    private static readonly Random rng = new();
    private CancellationTokenSource iterationTokenSource = new();

    [ObservableProperty]
    private GetSpinWords.Query queryParameters = new()
    {
        FromIndex = 1,
        ToIndex = 1,
        IsOriginalMode = true,
        IsTranslationMode = false,
    };

    public bool StartStopButtonEnable => IsNotStarted && (QueryParameters.IsOriginalMode || QueryParameters.IsTranslationMode);


    [ObservableProperty, NotifyPropertyChangedFor(nameof(IsNotStarted), nameof(StartStopButtonEnable))]
    private bool isStarted;
    public bool IsNotStarted => !IsStarted;

    [ObservableProperty, NotifyPropertyChangedFor(nameof(IsNotPaused))]
    private bool isPaused;
    public bool IsNotPaused => !IsPaused;

    [ObservableProperty]
    private WordSpinDto word = WordSpinDto.Sample;

    [ObservableProperty]
    private bool isDescriptionShowed = true;

    [ObservableProperty]
    private bool isTranslationEnable = true;

    [ObservableProperty]
    private double delayInSeconds = 1;

    public WordsSpinVM(IMediator mediator) : base(mediator)
    {

    }

    public async Task ResetIndex()
    {
        int max = await Mediator.Send(new GetWordsSpinMaxIndex.Query());

        if (max == 0)
        {
            QueryParameters.FromIndex = 0;
            QueryParameters.ToIndex = 0;
        }
        else
        {
            QueryParameters.ToIndex = 1;
        }

    }

    [RelayCommand]
    private async Task Start()
    {
        IsStarted = true;
        iterationTokenSource = new CancellationTokenSource();

        try
        {
            IsStarted = true;

            IEnumerable<WordSpinDto> words = await GetWordsListAsync();

            foreach (WordSpinDto word in words)
            {
                if (IsNotStarted)
                {
                    break;
                }

                await WaitPause();
                await NextWord(word);
            }
        }
        catch (AggregateException)
        {
        }
        finally
        {
            IsPaused = false;
            IsStarted = false;
        }
    }

    [RelayCommand]
    private void Stop()
    {
        iterationTokenSource.Cancel();
    }

    [RelayCommand]
    private void Pause()
    {
        IsPaused = true;
    }

    [RelayCommand]
    private void Resume()
    {
        IsPaused = false;
    }

    private async Task NextWord(WordSpinDto word)
    {
        Word = word;
        await Task.Delay(TimeSpan.FromSeconds(DelayInSeconds), iterationTokenSource.Token);
    }

    private async Task WaitPause()
    {
        while (IsPaused)
        {
            await Task.Delay(100, iterationTokenSource.Token);
        }
    }
    private async Task<IEnumerable<WordSpinDto>> GetWordsListAsync()
    {
        var words = await Mediator.Send(QueryParameters);
        return words.OrderBy(a => rng.Next()).ToList();
    }
}
