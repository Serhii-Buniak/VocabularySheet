using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.CambridgeDictionary;

public class CabridgePageBuilder
{
    private readonly CambridgeClient _client;
    private readonly CambridgeParser _parser;

    public CabridgePageBuilder(CambridgeClient client, CambridgeParser parser)
    {
        _client = client;
        _parser = parser;
    }

    public async Task<CambridgePage?> Build(string word, WordLanguage language, CancellationToken cancellationToken)
    {
        var response = await _client.Page(word, language, WordLanguage.En, cancellationToken);
        if (response == null)
        {
            return null;
        }
        
        var content = await _parser.Page(response.Html, cancellationToken);
        if (content == null)
        {
            return null;
        }

        return new CambridgePage()
        {
            Word = response.Word,
            Language = response.Language,
            TranslationLanguage = response.TranslationLanguage,
            Html = response.Html,
            Link = response.Link,
            CreatedAt = DateTime.UtcNow,
            Content = content,
        };
    }
}