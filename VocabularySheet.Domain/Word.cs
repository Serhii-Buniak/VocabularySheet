namespace VocabularySheet.Domain;

public record Word : IEntity
{
    public long Id { get; set; }

    public required string Original { get; set; }
    
    public required string Translation { get; set; }
    
    public string? Description { get; set; }
}
