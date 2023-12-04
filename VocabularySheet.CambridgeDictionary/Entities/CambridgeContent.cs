namespace VocabularySheet.CambridgeDictionary.Entities;

public record CambridgeContent
{
    public required string Title { get; init; }
    public required List<CambridgeWordBlock> Blocks { get; init; }
    public CambridgeRef? Ref { get; init; }
}