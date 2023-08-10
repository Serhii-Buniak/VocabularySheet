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

    public async Task<IEnumerable<Word>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Words.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
