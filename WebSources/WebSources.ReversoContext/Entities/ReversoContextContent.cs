namespace WebSources.ReversoContext.Entities;

public record ReversoContextContent
{
    public string? Title { get; init; }
    public required List<ReversoContextCetegoryGroup> CategoryGroups { get; init; }
    public required List<ReversoContextExample> Examples { get; init; }
}

public record ReversoContextCetegoryGroup
{
    public string? Title { get; init; }
    public required List<string> Translations { get; init; }

    internal static List<ReversoContextCetegoryGroup> CreateList(List<ReversoContextCetegory> categories, List<ReversoContextTranslation> translations)
    {
        return translations
            .GroupBy(x => x.Type)
            .Select(group => new ReversoContextCetegoryGroup
            {
                Title = categories.FirstOrDefault(c => c.Type == group.Key)?.Name,
                Translations = group.Select(t => t.Name).ToList()
            }).OrderBy(cg => cg.Title == null).ToList();
    }
}

public record ReversoContextExample
{
    public required string Origin { get; init; }
    public required string Translation { get; init; }
}

internal record ReversoContextCetegory
{
    public required string Name { get; init; }
    public string? Type { get; init; }
}

internal record ReversoContextTranslation
{
    public required string Name { get; init; }
    public string? Type { get; init; }
}