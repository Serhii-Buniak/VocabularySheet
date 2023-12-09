namespace VocabularySheet.CambridgeDictionary.Entities;

public interface ICambridgeWordBlock
{
    string? Title { get; init; }
    string? Category { get; init; }
    List<string> Irregulars { get; init; }
    List<CambridgeAudio> Audios { get; init; }
}

public record CambridgeWordBlock : ICambridgeWordBlock
{
    public string? Title { get; init; }
    public string? Category { get; init; }
    public List<string> Irregulars { get; init; } = new List<string>();
    public List<CambridgeAudio> Audios { get; init; } = new();
    public List<CambridgeArticle> Articles { get; init; } = new();
}

public record CambridgeWordSubBlock : ICambridgeWordBlock
{
    public string? Title { get; init; }
    public string? Category { get; init; }
    public string? Other { get; init; }
    public List<string> Irregulars { get; init; } = new();
    public List<CambridgeAudio> Audios { get; init; } = new();
    public List<CambridgeSubArticle> SubArticles { get; init; } = new();
}
