namespace VocabularySheet.CambridgeDictionary.Entities;

public record CambridgePage
{
    public required string Title { get; init; }
    public required List<CambridgeWordBlock> Blocks { get; init; }
    public CambridgeRef? Ref { get; init; }
}

public interface IHasHtmlContent
{
    string HtmlContent { get; }
}

public record CambridgePageWithHtml : CambridgePage, IHasHtmlContent
{
    public required string HtmlContent { get; init; }

    public CambridgePage WithOutHtml()
    {
        return this with
        {
            HtmlContent = ""
        };
    }
}
