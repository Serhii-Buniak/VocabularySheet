using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using VocabularySheet.Common.Extensions;
using VocabularySheet.ML.Client;
using ArticlePrediction = VocabularySheet.Application.ArticlePrediction.ArticlePrediction;

namespace VocabularySheet.Maui.Domain.ViewModels;

public record WordClassificationRecord : IHasPercentage
{
    private const float OtherPercentage = 0.05f;
        
    public required ArticleType ArticleType { get; init; }
    public required string ArticleFormatted { get; init; }
    public required float Probability { get; init; }
    public required int Percentage { get; set; }
    public string ProbabilityFormatted => $"{Percentage}%";

    public string BackgroundColor => ArticleType switch
    {
        ArticleType.Business => "#33124530",
        ArticleType.Entertainment => "#33c74d10",
        ArticleType.Food => "#3372854e",
        ArticleType.Graphics => "#3303a874",
        ArticleType.Historical => "#3338261f",
        ArticleType.Medical => "#337d0420",
        ArticleType.Politics => "#33bababa",
        ArticleType.Space => "#333b0670",
        ArticleType.Sport => "#3306706e",
        ArticleType.Technologie => "#33c7b708",
        _ => "#33424242"
    };
    
    public string BorderColor => ArticleType switch
    {
        ArticleType.Business => "#ee124530",
        ArticleType.Entertainment => "#eec74d10",
        ArticleType.Food => "#ee72854e",
        ArticleType.Graphics => "#ee03a874",
        ArticleType.Historical => "#ee38261f",
        ArticleType.Medical => "#ee7d0420",
        ArticleType.Politics => "#eebababa",
        ArticleType.Space => "#ee3b0670",
        ArticleType.Sport => "#ee06706e",
        ArticleType.Technologie => "#eec7b708",
        _ => "#ee424242"
    };
    
    public string Icon => ArticleType switch
    {
        ArticleType.Business => "article_business.png",
        ArticleType.Entertainment => "article_entertainment.png",
        ArticleType.Food => "article_food.png",
        ArticleType.Graphics => "article_graphics.png",
        ArticleType.Historical => "article_historical.png",
        ArticleType.Medical => "article_medical.png",
        ArticleType.Politics => "article_politics.png",
        ArticleType.Space => "article_space.png",
        ArticleType.Sport => "article_sport.png",
        ArticleType.Technologie => "article_technologie.png",
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


public partial class WordClassificationVm : BaseViewModel
{
    [ObservableProperty] private string _searchWord = string.Empty;
    [ObservableProperty] private List<WordClassificationRecord> _records = [];
    
    public WordClassificationVm(IMediator mediator, ILogger<WordClassificationVm> logger) : base(mediator, logger)
    {
    }
    
    [RelayCommand]
    public async Task Classify()
    {
        if (string.IsNullOrWhiteSpace(SearchWord))
        {
            return;
        }

        ArticleProbabilityResult probabilityResult = await Mediator.Send(new ArticlePrediction.Query
        {
            Word = SearchWord
        });

        Records = WordClassificationRecord.CreateList(probabilityResult);
    }
}