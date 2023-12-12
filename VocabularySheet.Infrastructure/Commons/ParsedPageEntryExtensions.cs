using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VocabularySheet.Common;
using VocabularySheet.Infrastructure.Repositories.Pages;

namespace VocabularySheet.Infrastructure.Commons;

public static class ParsedPageEntryExtensions
{
    public static KeyBuilder HasKeyPageEntry<TPage>(this EntityTypeBuilder<TPage> typeBuilder)
        where TPage : ParsedPageEntry
    {
        return typeBuilder.HasKey(entry => new { entry.Word, entry.Language, entry.TranslationLanguage });
    }

    public static async Task<T?> FirstOrDefaultKey<T>(this IQueryable<T> queryable, string word, WordLanguage language, 
        WordLanguage translationLanguage, CancellationToken cancellationToken) where T : IParsedPageEntry
    {
        return await queryable.FirstOrDefaultAsync(x => x.Word == word && x.Language == language && x.TranslationLanguage == translationLanguage, cancellationToken);
    }
}