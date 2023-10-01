namespace VocabularySheet.Domain;

public record Word : IEntity
{
    public long Id { get; set; }

    public required string Original { get; set; }
    
    public required string Translation { get; set; }
    
    public string? Description { get; set; }

    public Category? Category { get; set; }
}

public enum Category
{
    Unknown = 0,
    Red = 1,
    Green = 2,
    Yellow = 3,
    Orange = 4,
    Purple = 5,
    Pink = 6,
}