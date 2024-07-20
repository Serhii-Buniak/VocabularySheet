using System.Windows.Input;
using Application.Common.Commons.Dtos;
using Application.Common.LanguageWords;
using CommunityToolkit.Mvvm.Input;
using Domain.Localization;
using MediatR;
using Microsoft.Extensions.Logging;
using WebSources.Common;
using WebSources.Linker;

namespace Apps.MauiRunner.ViewModels;

public record LinkBoxModel
{
    public required string BorderColor { get; init; }
    public required string BackgroundColor { get; init; }
    public required string Image { get; init; }
    public required ICommand Command { get; init; }
}

public partial class LinkBoxVm(IMediator mediator, ILogger logger) : BaseViewModel(mediator, logger)
{
    private LocalizationConfig Localization  { get; set; } = new()
    {
        OriginLang = WordModel.Sample.OrignalLanguage,
        TranslateLang = WordModel.Sample.TranslationLanguage,
    };
    
    private string Word { get; set; } = WordModel.Sample.Original;

    public async Task SetWord(string word, CancellationToken cancellationToken)
    {
        Localization = await Mediator.Send(new GetLanguageWord.Query(), cancellationToken);
        Word = word;
    }

    public List<LinkBoxModel> BoxModels =>
    [
        new LinkBoxModel
        {
            BorderColor = "#ee596af0",
            BackgroundColor = "#33596af0",
            Image = "google_traslator_logo.png",
            Command = OpenGoogleTranslateCommand
        },
        new LinkBoxModel
        {
            BorderColor = "#ee8ed4f5",
            BackgroundColor = "#598ed4f5",
            Image = "deepl_logo.png",
            Command = OpenDeepLTranslateCommand
        },
        new LinkBoxModel
        {
            BorderColor = "#ee88e3a5",
            BackgroundColor = "#3388e3a5",
            Image = "oxford_dictionary_icon.png",
            Command = OpenOxfordDictionaryTranslateCommand
        },
        new LinkBoxModel
        {
            BorderColor = "#ee526efa",
            BackgroundColor = "#33526efa",
            Image = "urban_dictionary_icon.png",
            Command = OpenUrbanDictionaryTranslateCommand
        },
        new LinkBoxModel
        {
            BorderColor = "#eef452fa",
            BackgroundColor = "#33f452fa",
            Image = "machine_learning.png",
            Command = OpenMachineLearningTranslateCommand
        },
    ];
    
    [RelayCommand]
    public async Task OpenGoogleTranslate()
    {
        ExternalSourceLink link = GoogleTranslatorLinker.Link(Word, Localization.OriginLang, Localization.TranslateLang);
        await Launcher.OpenAsync(link.Link);
    }
    
    [RelayCommand]
    public async Task OpenDeepLTranslate()
    {
        ExternalSourceLink link = DeepLTranslatorLinker.Link(Word, Localization.OriginLang, Localization.TranslateLang);
        await Launcher.OpenAsync(link.Link);
    }
    
    [RelayCommand]
    public async Task OpenOxfordDictionaryTranslate()
    {
        ExternalSourceLink link = OxfordDictionaryTranslatorLinker.Link(Word, Localization.OriginLang, Localization.TranslateLang);
        await Launcher.OpenAsync(link.Link);
    }
    
    [RelayCommand]
    public async Task OpenUrbanDictionaryTranslate()
    {
        ExternalSourceLink link = UrbanDictionaryTranslatorLinker.Link(Word, Localization.OriginLang, Localization.TranslateLang);
        await Launcher.OpenAsync(link.Link);
    }
    
    [RelayCommand]
    public async Task OpenMachineLearningTranslate()
    {
        await Shell.Current.GoToWordClassification(Word);
    }
}