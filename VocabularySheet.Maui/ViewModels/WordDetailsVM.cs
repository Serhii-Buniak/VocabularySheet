using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.LanguageWords;
using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Maui.Views;

namespace VocabularySheet.Maui.ViewModels;

public partial class WordDetailsVM : BaseViewModel
{
    [ObservableProperty] private WordSpinDto word = WordSpinDto.Sample;
    
    public WordDetailsVM(IMediator mediator, ILogger<LanguageWordVM> logger) : base(mediator, logger)
    {
    }

    public void SetWord(WordSpinDto wordsSpin)
    {
        Word = wordsSpin;
    }
}
