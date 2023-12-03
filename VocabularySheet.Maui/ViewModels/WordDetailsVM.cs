using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.Words.Queries;
using VocabularySheet.Maui.Common;

namespace VocabularySheet.Maui.ViewModels;

[QueryProperty("Id", "Id")]
public partial class WordDetailsVM : BaseViewModel
{
    [ObservableProperty] long id = 0;
    [ObservableProperty] WordSpinDto word = WordSpinDto.Sample;
    
    public WordDetailsVM(IMediator mediator, ILogger<LanguageWordVM> logger) : base(mediator, logger)
    {
    }

    public async Task SetWord()
    {
        Word = await Mediator.Send(new GetSpinWord.Query()
        {
            Id = Id
        }) ?? Word;
    }
    
    [RelayCommand]
    public async Task GoBack()
    {
        await Shell.Current.GoToBack();
    }
}