using VocabularySheet.Common;
using VocabularySheet.Domain.Pages;

namespace VocabularySheet.Application.Commons.Interfaces;

public interface IReversoContextRepository
{
    Task<PublicReversoContextEntry?> Get(string word, WordLanguage language, WordLanguage translateLanguage,
        CancellationToken cancellationToken);
}