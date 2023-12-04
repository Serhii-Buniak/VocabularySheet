namespace VocabularySheet.CambridgeDictionary.Entities;

public record CambridgeRef
{
    public required string Title { get; set; }
    public required string Url { get; set; }
    
    public string FullLink()
    {
        return CambridgeClient.Base + Url;
    }
}