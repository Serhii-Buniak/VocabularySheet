namespace VocabularySheet.Domain;

public class Word : BaseEntity
{
    public override long Id { get; set; }

    public string Original { get; set; } = null!;
    
    public string Translation { get; set; } = null!;
    
    public string? Description { get; set; }
}
