using Microsoft.EntityFrameworkCore;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Domain;
using VocabularySheet.Infrastructure.Commons;
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

    public async Task<Word?> GetById(long id, CancellationToken cancellationToken)
    {
       return await _context.Words.FirstOrDefaultId(id, cancellationToken);
    }
    
    public async Task<Word?> GetByName(string word, CancellationToken cancellationToken)
    {
        var strictOrigial = await _context.Words
            .FirstOrDefaultAsync(w => w.Original == word, cancellationToken);
        if (strictOrigial != null)
        {
            return strictOrigial;
        }
        
        var strictTranslate = await _context.Words
            .FirstOrDefaultAsync(w => w.Translation == word, cancellationToken);
        if (strictTranslate != null)
        {
            return strictTranslate;
        }

        var original = await _context.Words.OrderBy(w => w.Original.Length)
            .FirstOrDefaultAsync(w => w.Original.Contains(word), cancellationToken);
        if (original != null)
        {
            return original;
        }
        
        var translate = await _context.Words.OrderBy(w => w.Translation.Length)
            .FirstOrDefaultAsync(w => w.Translation.Contains(word), cancellationToken);
        
        return translate;
    }

    public async Task<int?> GetIndexOf(long id, CancellationToken cancellationToken)
    {
        var words = await _context.Words
            .OrderBy(w => w.Id)
            .OfType<IEntity<long>>()
            .ToListAsync(cancellationToken);

        var word = words.Select((w, i) => new
        {
            w.Id,
            Index = i,
        }).FirstOrDefault(w => w.Id == id);

        return word?.Index + 1; 
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
