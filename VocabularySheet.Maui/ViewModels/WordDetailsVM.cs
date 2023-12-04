using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using VocabularySheet.Application.Cambridge.Queries;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.Words.Queries;
using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Domain.Pages;
using VocabularySheet.Maui.Common;

namespace VocabularySheet.Maui.ViewModels;

[QueryProperty("Id", "Id")]
public partial class WordDetailsVM : BaseViewModel
{
    [ObservableProperty] long id = 0;
    [ObservableProperty] WordModel word = WordModel.Sample;
    [ObservableProperty] Dictionary<WordLanguage, PublicCambridgeEntry> cambridge = new();
    
    public WordDetailsVM(IMediator mediator, ILogger<LanguageWordVM> logger) : base(mediator, logger)
    {
    }
    
    [RelayCommand]
    public async Task GoBack()
    {
        await Shell.Current.GoToBack();
    }

    public async Task LoadDataAsync()
    {
        Word = await Mediator.Send(new GetSpinWord.Query()
        {
            Id = Id
        }) ?? Word;

        Cambridge = await Mediator.Send(new GetCambridgePage.Query()
        {
            Word = Word
        });
    }
}