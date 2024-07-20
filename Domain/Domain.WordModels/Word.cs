using Domain.Common;
using ML.Predictor;

namespace Domain.WordModels;

public interface IWord
{
    long Id { get; }
    string Original { get; }
    string Translation { get; }
    string? Description { get; }
    ArticleType ArticleType { get; }
    bool Hidden { get; }
}

public record Word : ILongEntity, IWord
{
    public static DateTime CreateHiddenTo() => DateTime.UtcNow + TimeSpan.FromHours(50);
    public static DateTime CreateNoHiddenTo() => DateTime.MinValue;
    
    public long Id { get; init; }

    public required string Original { get; init; }
    
    public required string Translation { get; init; }
    
    public string? Description { get; init; }

    public Category? Category { get; init; }
    
    public ArticleType ArticleType { get; set; } = ArticleType.Other;
    public int RowNumber { get; init; } = 0;
    public DateTime HiddenTo { get; set; }
    
    public bool Hidden => DateTime.UtcNow < HiddenTo;
}
