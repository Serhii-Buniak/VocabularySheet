using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Maui.Common;
using VocabularySheet.Maui.Views;

namespace VocabularySheet.Maui.ViewModels;

public partial class WordsListVM : BaseViewModel
{
    private readonly WordsSpinVM _wordsSpinVm;
    private readonly WordDetailsVM _wordDetails;

    [ObservableProperty] List<WordSpinDto> words = new List<WordSpinDto>();
    
    public WordsListVM(IMediator mediator, ILogger<LanguageWordVM> logger, WordsSpinVM wordsSpinVm, WordDetailsVM wordDetails) : base(mediator, logger)
    {
        _wordsSpinVm = wordsSpinVm;
        _wordDetails = wordDetails;
    }
    
    [RelayCommand]
    public async Task OpenWord(long id)
    {
        _wordDetails.SetWord(Words.FirstOrDefault(w => w.Id == id) ?? WordSpinDto.Sample);
        await Shell.Current.GoToWordDetails();
    }
    
    public async Task LoadDataAsync(CancellationToken cancellationToken)
    {
        var allWords = await Mediator.Send(_wordsSpinVm.QueryParameters, cancellationToken);
        
        Words = allWords.ToList();
    }
}
