using VocabularySheet.Domain;

namespace VocabularySheet.Infrastructure.Repositories.Interfaces;

public interface IGoogleSheetWordsRepository
{
    Task<IEnumerable<Word>?> GetAllAsync(CancellationToken cancellationToken);
}