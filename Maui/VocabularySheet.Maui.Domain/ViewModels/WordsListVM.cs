using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.Words.Queries;
using VocabularySheet.Maui.Domain.Common;

namespace VocabularySheet.Maui.Domain.ViewModels;

public partial class WordsListVM : BaseViewModel
{
    private readonly WordsSpinVM _wordsSpinVm;
    
    [ObservableProperty] List<WordModel> _words = new List<WordModel>();
    [ObservableProperty] GetSpinWords.Query? _savedQuery = null;
    
    public WordsListVM(IMediator mediator, ILogger<WordsListVM> logger, WordsSpinVM wordsSpinVm) : base(mediator, logger)
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
        if (_wordsSpinVm.QueryParameters.IsValid() && SavedQuery != _wordsSpinVm.QueryParameters)
        {
            SavedQuery = _wordsSpinVm.QueryParameters;
            IEnumerable<WordModel> allWords = await Mediator.Send(_wordsSpinVm.QueryParameters, cancellationToken);
            Words = allWords.ToList();
        }
    }
}
