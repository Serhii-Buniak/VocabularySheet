using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.LanguageWords;
using VocabularySheet.Common;

namespace VocabularySheet.Maui.ViewModels;

public partial class LanguageWordVM : BaseViewModel
{
    [ObservableProperty]
    private WordLanguageItem origin = new WordLanguageItem(WordLanguage.En, "English");
    
    [ObservableProperty]
    private WordLanguageItem translation = new WordLanguageItem(WordLanguage.En, "English");
    
    public ObservableCollection<WordLanguageItem> LanguageItems { get; init; } = new()
    {
        new WordLanguageItem(WordLanguage.En, "English"),
        new WordLanguageItem(WordLanguage.Ua, "Ukranian"),
        new WordLanguageItem(WordLanguage.Ru, "404?"),
    };
    
    public LanguageWordVM(IMediator mediator, ILogger<LanguageWordVM> logger) : base(mediator, logger)
    {
    }
    
    [RelayCommand]
    private async Task Save()
    {
        await Mediator.Send(new SetLanguageWord.Command()
        {
            WordLang = Origin.Key,
            TranslateLang = Translation.Key,
        });
    }
    
    public async Task LoadDataAsync()
    {
        var configuration = await Mediator.Send(new GetLanguageWord.Query());
        Origin = LanguageItems.First(l => l.Key == configuration.OriginLang);
        Translation = LanguageItems.First(l => l.Key == configuration.TranslateLang);
    }
}
