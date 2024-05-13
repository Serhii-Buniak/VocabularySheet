using Domain.Common;

namespace Domain.WordModels;

public interface IWord
{
    long Id { get; }
    string Original { get; }
    string Translation { get; }
    string? Description { get; }
}

public record Word : ILongEntity, IWord
{
    public long Id { get; set; }

    public required string Original { get; set; }
    
    public required string Translation { get; set; }
    
    public string? Description { get; set; }

    public Category? Category { get; set; }
}