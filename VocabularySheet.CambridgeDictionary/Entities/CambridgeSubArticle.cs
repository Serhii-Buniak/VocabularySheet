namespace VocabularySheet.CambridgeDictionary.Entities;

public record CambridgeSubArticle
{
    public required CambridgeSubArticleHeader Header { get; init; }
    public List<string> Examples { get; init; } = new();
    public string? ImageUrl { get; init; }
    
    public string? FullImageLink()
    {
        if (ImageUrl == null)
        {
            return null;
        }
        
        return CambridgeClient.Base + ImageUrl;
    }
}

public record CambridgeSubArticleHeader
{
    public string? Title { get; set; }
    public string? Level { get; set; }
    public string? Translation { get; set; }
    public string? BlueTitle { get; init; }
    public bool IsBlue() => BlueTitle != null;
}