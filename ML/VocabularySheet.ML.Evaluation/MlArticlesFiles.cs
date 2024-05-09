namespace VocabularySheet.ML.Evaluation;

internal record MlArticlesFiles
{
    public required string[] Business { get; init; }
    public required string[] Entertainment { get; init; }
    public required string[] Food { get; init; }
    public required string[] Graphics { get; init; }
    public required string[] Historical { get; init; }
    public required string[] Medical { get; init; }
    public required string[] Politics { get; init; }
    public required string[] Space { get; init; }
    public required string[] Sport { get; init; }
    public required string[] Technologie { get; init; }
}