namespace VocabularySheet.CambridgeDictionary.Entities;

public record CambridgeArticle
{
    public required CambridgeArticleHeader Header { get; init; }
    public List<CambridgeSubArticle> SubArticles { get; init; } = new();
}

public record CambridgeArticleHeader
{
    public string? Title { get; init; }
    public string? BlueTitle { get; init; }
    public string? Category { get; init; }
    public string? Meaning { get; init; }
    public bool IsBlue() => BlueTitle != null;

}