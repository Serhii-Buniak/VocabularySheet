namespace ML.Trainer;

internal record MlArticlesFiles
{
    public required string[] Sport { get; init; }
    public required string[] Science { get; init; }
    public required string[] Religion { get; init; }
    public required string[] Politics { get; init; }
    public required string[] Medical { get; init; }
    public required string[] Historical { get; init; }
    public required string[] Fantasy { get; init; }
    public required string[] Economic { get; init; }
    public required string[] Digital { get; init; }
    public required string[] Culinary { get; init; }
}