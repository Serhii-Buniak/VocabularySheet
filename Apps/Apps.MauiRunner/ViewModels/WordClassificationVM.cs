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

    public string BackgroundColor => ArticleType switch
    {
        ArticleType.Economic => "#33124530",
        ArticleType.Fantasy => "#33c74d10",
        ArticleType.Culinary => "#3372854e",
        ArticleType.Digital => "#3303a874",
        ArticleType.Historical => "#3338261f",
        ArticleType.Medical => "#337d0420",
        ArticleType.Politics => "#33bababa",
        ArticleType.Sport => "#3306706e",
        ArticleType.Science => "#33c7b708",
        ArticleType.Religion => "#33ededed",
        _ => "#33424242"
    };
    
    public string BorderColor => ArticleType switch
    {
        ArticleType.Economic => "#ee124530",
        ArticleType.Fantasy => "#eec74d10",
        ArticleType.Culinary => "#ee72854e",
        ArticleType.Digital => "#ee03a874",
        ArticleType.Historical => "#ee38261f",
        ArticleType.Medical => "#ee7d0420",
        ArticleType.Politics => "#eebababa",
        ArticleType.Sport => "#ee06706e",
        ArticleType.Science => "#eec7b708",
        ArticleType.Religion => "#eeededed",
        _ => "#ee424242"
    };
    
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
            okProbabilities = [..okProbabilities, other];
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
    }
}