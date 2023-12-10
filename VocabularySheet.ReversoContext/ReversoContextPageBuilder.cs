using VocabularySheet.Common;
using VocabularySheet.Parsing.Common;
using VocabularySheet.ReversoContext.Entities;

namespace VocabularySheet.ReversoContext;

public class ReversoContextPageBuilder
{
    private readonly ReversoContextClient _client;
    private readonly ReversoContextParser _parser;

    public ReversoContextPageBuilder(ReversoContextClient client, ReversoContextParser parser)
    {
        _client = client;
        _parser = parser;
    }

    public async Task<ReversoContextPage?> Build(string word, WordLanguage language,
        WordLanguage translationLanguage, CancellationToken cancellationToken)
    {
        var response = await _client.Page(word, language, translationLanguage, cancellationToken);
        if (response == null)
        {
            return null;
        }
        
        var content = await _parser.Page(response.Html, cancellationToken);
        if (content == null)
        {
            return null;
        }

        return new ReversoContextPage()
        {
            Word = response.Word,
            Language = response.Language,
            TranslationLanguage = translationLanguage,
            Html = response.Html,
            Link = response.Link,
            CreatedAt = DateTime.UtcNow,
            Content = content,
        };
    }
}

public record ReversoContextPage : IParsedPage<ReversoContextContent>
{
    public required string Word { get; init; }
    public required WordLanguage Language { get; init; }
    public required WordLanguage TranslationLanguage { get; init; }
    public required string Html { get; init; }
    public required string Link { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required ReversoContextContent Content { get; init; }
}