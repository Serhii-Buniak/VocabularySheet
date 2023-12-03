using VocabularySheet.Domain;

namespace VocabularySheet.Application.Commons.Interfaces;

public interface IWordsRepository
{
    Task<Word?> GetById(long id, CancellationToken cancellationToken);
    Task<int?> GetIndexOf(long id, CancellationToken cancellationToken);
    Task<IEnumerable<Word>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Word>> TakeAsync(int take, int skip, CancellationToken cancellationToken);
    Task<IEnumerable<Word>> TakeAsync(int take, int skip, Category category, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<Word> words, CancellationToken cancellationToken);
    Task<int> CountAsync(Category category, CancellationToken cancellationToken);
    Task<int> CountAsync(CancellationToken cancellationToken);
    int Count();
    int Count(Category category);
    Task<int> Clear(CancellationToken cancellationToken);
    Task<int> SaveAsync(CancellationToken cancellationToken);
}