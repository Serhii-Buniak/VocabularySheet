using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using ML.Predictor;
using Tools.Common.Extensions;
using ArticlePrediction = Application.Common.ArticlePrediction.ArticlePrediction;

namespace Apps.MauiRunner.ViewModels;

public record WordClassificationRecord : IHasPercentage
{
    private const float OtherPercentage = 0.1f;

    public required ArticleType ArticleType { get; init; }
    public required string ArticleFormatted { get; init; }
    public required float Probability { get; init; }
    public required int Percentage { get; set; }
    public string ProbabilityFormatted => $"{Percentage}%";

    public string BackgroundColor => ArticleType.ToAlphaMauiColor().ToArgbHex(true);

    public string BorderColor => ArticleType.ToMauiColor().ToArgbHex(true);

    public string Icon => ArticleType switch
    {
        ArticleType.Economic => "article_business.png",
        ArticleType.Fantasy => "article_entertainment.png",
        ArticleType.Culinary => "article_food.png",
        ArticleType.Digital => "article_graphics.png",
        ArticleType.Historical => "article_historical.png",
        ArticleType.Medical => "article_medical.png",
        ArticleType.Politics => "article_politics.png",
        ArticleType.Sport => "article_sport.png",
        ArticleType.Science => "article_chemical.png",
        ArticleType.Religion => "article_religion.png",
        _ => "article_other.png"
    };

    public static List<WordClassificationRecord> CreateList(ArticleProbabilityResult probability)
    {
        var okProbabilities = probability.OrderedList.Where(r => r.Value >= OtherPercentage).ToList();

        var otherProbabilities = probability.OrderedList.Where(r => r.Value < OtherPercentage).ToList();

        if (otherProbabilities.Count != 0)
        {
            var other = new KeyValuePair<ArticleType, float>(ArticleType.Other, otherProbabilities.Sum(p => p.Value));
            okProbabilities = [.. okProbabilities, other];
        }

        return okProbabilities.Select(Create).ToList().AdjustPercentageTo100();
    }

    public static WordClassificationRecord Create(KeyValuePair<ArticleType, float> record)
    {
        int percent = (int)Math.Round(record.Value * 100);

        return new WordClassificationRecord
        {
            ArticleType = record.Key,
            ArticleFormatted = record.Key.ToString("G"),
            Probability = record.Value,
            Percentage = percent
        };
    }
}

[QueryProperty("WordParam", "WordParam")]
public partial class WordClassificationVm : BaseViewModel
{
    [ObservableProperty] private string _searchWord = string.Empty;
    [ObservableProperty] private List<WordClassificationRecord> _records = [];

    [ObservableProperty] private string _wordParam = string.Empty;

    public WordClassificationVm(IMediator mediator, ILogger<WordClassificationVm> logger) : base(mediator, logger)
    {
    }

    public void TrySet(string word)
    {
        SearchWord = word; ;
    }

    public async Task LoadDataAsync()
    {
        if (!string.IsNullOrWhiteSpace(WordParam))
        {
            SearchWord = WordParam;
        }

        await Classify();
    }

    [RelayCommand]
    public async Task Classify()
    {
        if (string.IsNullOrWhiteSpace(SearchWord))
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(WordParam))
        {
            await Shell.Current.GoToWordClassification();
        }

        ArticleProbabilityResult probabilityResult = await Mediator.Send(new ArticlePrediction.Query()
        {
            Word = SearchWord
        });

        Records = WordClassificationRecord.CreateList(probabilityResult);

        //Records =
        //[
        //    WordClassificationRecord.Create(new KeyValuePair<ArticleType, float>(ArticleType.Other, 100)),
        //    WordClassificationRecord.Create(new KeyValuePair<ArticleType, float>(ArticleType.Sport, 100)),
        //    WordClassificationRecord.Create(new KeyValuePair<ArticleType, float>(ArticleType.Science, 100)),
        //    WordClassificationRecord.Create(new KeyValuePair<ArticleType, float>(ArticleType.Religion, 100)),
        //    WordClassificationRecord.Create(new KeyValuePair<ArticleType, float>(ArticleType.Politics, 100)),
        //    WordClassificationRecord.Create(new KeyValuePair<ArticleType, float>(ArticleType.Medical, 100)),
        //    WordClassificationRecord.Create(new KeyValuePair<ArticleType, float>(ArticleType.Historical, 100)),
        //    WordClassificationRecord.Create(new KeyValuePair<ArticleType, float>(ArticleType.Fantasy, 100)),
        //    WordClassificationRecord.Create(new KeyValuePair<ArticleType, float>(ArticleType.Economic, 100)),
        //    WordClassificationRecord.Create(new KeyValuePair<ArticleType, float>(ArticleType.Digital, 100)),
        //    WordClassificationRecord.Create(new KeyValuePair<ArticleType, float>(ArticleType.Culinary, 100)),
        //];

    }
}