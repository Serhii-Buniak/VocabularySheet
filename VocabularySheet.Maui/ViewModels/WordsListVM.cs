using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Maui.Common;

namespace VocabularySheet.Maui.ViewModels;

public partial class WordsListVM : BaseViewModel
{
    private readonly WordsSpinVM _wordsSpinVm;

    [ObservableProperty] List<WordSpinDto> words = new List<WordSpinDto>();
    
    public WordsListVM(IMediator mediator, ILogger<LanguageWordVM> logger, WordsSpinVM wordsSpinVm) : base(mediator, logger)
    {
        _wordsSpinVm = wordsSpinVm;
    }
    
    [RelayCommand]
    public async Task OpenWord(long id)
    {
        await Shell.Current.GoToWordDetails(id);
    }
    
    public async Task LoadDataAsync(CancellationToken cancellationToken)
    {
        if (_wordsSpinVm.QueryParameters.IsValid())
        {
            var allWords = await Mediator.Send(_wordsSpinVm.QueryParameters, cancellationToken);
            Words = allWords.ToList();
        }
    }
}
