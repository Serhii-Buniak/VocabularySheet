using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Windows.Input;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.Words.Queries;

namespace VocabularySheet.Maui.ViewModels;

public partial class WordsSpinVM : BaseViewModel
{
    private static readonly Random rng = new();

    [ObservableProperty]
    private GetSpinWords.Query queryParameters = new()
    {
        FromIndex = 1,
        ToIndex = 1,
        IsOriginalMode = true,
        IsTranslationMode = false,
    };



    [ObservableProperty]
    private bool isStarted = false;

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

    public WordsSpinVM(IMediator mediator, ILogger<WordsSpinVM> logger) : base(mediator, logger)
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
            QueryParameters.ToIndex = max;
        }

    }

    [RelayCommand(IncludeCancelCommand = true, AllowConcurrentExecutions = true, CanExecute = nameof(IsStarted))]
    public async Task Start(CancellationToken cancellationToken)
    {
        IsStarted = true;
        try
        {
            IEnumerable<WordSpinDto> words = await GetWordsListAsync(cancellationToken);


            foreach (WordSpinDto word in words)
            {
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

    //[RelayCommand]
    //private void Pause()
    //{
    //    IsPaused = true;
    //}

    //[RelayCommand]
    //private void Resume()
    //{
    //    IsPaused = false;
    //}

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
