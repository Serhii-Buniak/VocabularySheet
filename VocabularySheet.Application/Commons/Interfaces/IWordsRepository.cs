using VocabularySheet.Domain;

namespace VocabularySheet.Application.Commons.Interfaces;

public interface IWordsRepository
{
    Task<IEnumerable<Word>> GetAllAsync(CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<Word> words, CancellationToken cancellationToken);
    Task<int> Clear(CancellationToken cancellationToken);
    Task<int> SaveAsync(CancellationToken cancellationToken);
}