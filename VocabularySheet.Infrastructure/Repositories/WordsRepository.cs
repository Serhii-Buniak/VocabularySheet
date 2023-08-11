using Microsoft.EntityFrameworkCore;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Domain;
using VocabularySheet.Infrastructure.Data.Interfaces;

namespace VocabularySheet.Infrastructure.Repositories;

public class WordsRepository : IWordsRepository
{
    private readonly IAppDbContext _context;

    public WordsRepository(IAppDbContext context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(IEnumerable<Word> words, CancellationToken cancellationToken)
    {
        await _context.Words.AddRangeAsync(words, cancellationToken);
    }

    public async Task<int> Clear(CancellationToken cancellationToken)
    {
        return await _context.Words.ExecuteDeleteAsync(cancellationToken);
    }

    public int Count()
    {
        return _context.Words.Count();
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await _context.Words.CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<Word>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Words.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Word>> TakeAsync(int take, int skip, CancellationToken cancellationToken)
    {
        return await _context.Words.Skip(skip).Take(take).AsNoTracking().ToListAsync(cancellationToken);
    }
}
