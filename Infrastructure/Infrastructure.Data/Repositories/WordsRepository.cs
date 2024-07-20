using Application.Common.Commons.Dtos;
using Application.Common.Commons.Interfaces;
using Domain.Common;
using Domain.WordModels;
using Infrastructure.Data.Commons;
using Infrastructure.Data.Data.Interfaces;
using Infrastructure.Data.Repositories.Configurations;
using Infrastructure.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class WordsRepository : IWordsRepository, IWordDescriptionProvider
{
    public WordDescriptionProviderId WordDescriptionId => WordDescriptionProviderId.GoogleSheet;
    
    private readonly IAppDbContext _context;
    private readonly LocalizationConfigurator _localizationConfigurator;

    public WordsRepository(IAppDbContext context, LocalizationConfigurator localizationConfigurator)
    {
        _context = context;
        _localizationConfigurator = localizationConfigurator;
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
    
    public async Task<Word?> GetByIdRealOnly(long id, CancellationToken cancellationToken)
    {
       return await _context.Words.AsNoTracking().FirstOrDefaultId(id, cancellationToken);
    }

    public async Task SetHidden(long id, CancellationToken cancellationToken)
    {
        var word = await GetById(id, cancellationToken);
        if (word == null)
        {
            return;
        }
        
        word.HiddenTo = Word.CreateHiddenTo();
        await SaveAsync(cancellationToken);
    }

    public async Task SetNotHidden(int take, int skip, CancellationToken cancellationToken)
    {
        await _context.Words
            .Skip(skip)
            .Take(take)
            .ExecuteUpdateAsync(p => p.SetProperty(pr => pr.HiddenTo, Word.CreateNoHiddenTo()), cancellationToken);
        
    }
    
    public async Task SetNotHidden(int take, int skip, Category category, CancellationToken cancellationToken)
    {
        await _context.Words
            .Where(w => w.Category == category)
            .Skip(skip)
            .Take(take)
            .ExecuteUpdateAsync(p => p.SetProperty(pr => pr.HiddenTo, Word.CreateNoHiddenTo()), cancellationToken);
    }

    public async Task<Word?> GetByName(string word, CancellationToken cancellationToken)
    {
        var strictOrigial = await _context.Words
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.Original == word, cancellationToken);
        if (strictOrigial != null)
        {
            return strictOrigial;
        }
        
        var strictTranslate = await GetWordQuery()
            .FirstOrDefaultAsync(w => w.Translation == word, cancellationToken);
        if (strictTranslate != null)
        {
            return strictTranslate;
        }

        var original = await _context.Words.OrderBy(w => w.Original.Length)
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.Original.Contains(word), cancellationToken);
        if (original != null)
        {
            return original;
        }
        
        var translate = await _context.Words.OrderBy(w => w.Translation.Length)
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.Translation.Contains(word), cancellationToken);
        
        return translate;
    }

    public async Task<int?> GetIndexOf(long id, CancellationToken cancellationToken)
    {
        var words = await GetWordQuery()
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
        return await GetWordQuery().ToListAsync(cancellationToken);
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

    public async Task<WordDescriptionResult?> GetWordDescription(WordWithLanguage word, CancellationToken cancellationToken)
    {
        var localizationConfig = await _localizationConfigurator.Get(cancellationToken);

        string? description;
        if (localizationConfig.OriginLang == word.Language)
        {
            description = GetWordQuery().FirstOrDefault(r => r.Original == word.Word)?.Description;
        }
        else if (localizationConfig.TranslateLang == word.Language)
        {
            description = GetWordQuery().FirstOrDefault(r => r.Translation == word.Word)?.Description;
        }
        else
        {
            description = GetWordQuery().FirstOrDefault(r => r.Original == word.Word || r.Translation == word.Word)?.Description;
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return null;
        }
        
        return new WordDescriptionResult(WordDescriptionId)
        {
            Text = description
        };
    }
}
