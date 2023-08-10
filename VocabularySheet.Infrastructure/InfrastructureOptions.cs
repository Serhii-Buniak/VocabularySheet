namespace VocabularySheet.Infrastructure;

public record InfrastructureOptions
{
    public string DataDirectory { get; set; } = null!;
}