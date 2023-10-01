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
        return GetWordQuery().Count();
    }   
    
    public int Count(Category category)
    {
        return GetWordByCategoryQuery(category).Count();
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await GetWordQuery().CountAsync(cancellationToken);
    }   
    
    public async Task<int> CountAsync(Category category, CancellationToken cancellationToken)
    {
        return await GetWordByCategoryQuery(category).CountAsync(cancellationToken);
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
        return await GetWordQuery()
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Word>> TakeAsync(int take, int skip, Category category, CancellationToken cancellationToken)
    {
        return await GetWordByCategoryQuery(category)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
    }   
    
    public IQueryable<Word> GetWordQuery()
    {
        return _context.Words.AsNoTracking();
    }   
    
    public IQueryable<Word> GetWordByCategoryQuery(Category category)
    {
        return _context.Words.Where(w => w.Category == category).AsNoTracking();
    }
}
