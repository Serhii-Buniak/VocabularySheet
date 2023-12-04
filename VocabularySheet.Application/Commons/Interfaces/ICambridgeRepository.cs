using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Domain.Pages;

namespace VocabularySheet.Application.Commons.Interfaces;

public interface ICambridgeRepository
{
    Task<PublicCambridgeEntry?> Get(string word, WordLanguage language, CancellationToken cancellationToken);
}