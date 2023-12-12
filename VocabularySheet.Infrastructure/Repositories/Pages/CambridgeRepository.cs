using Microsoft.EntityFrameworkCore;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.CambridgeDictionary;
using VocabularySheet.Common;
using VocabularySheet.Domain.Pages;
using VocabularySheet.Infrastructure.Commons;
using VocabularySheet.Infrastructure.Data;

namespace VocabularySheet.Infrastructure.Repositories.Pages;

public class CambridgeRepository : ICambridgeRepository
{
    private readonly DbSet<CambridgeEntry> _cambridge;
    private readonly CabridgePageBuilder _builder;
    private readonly AppDbContext _context;

    public CambridgeRepository(AppDbContext context, CabridgePageBuilder builder)
    {
        _context = context;
        _cambridge = context.Cambridge;
        _builder = builder;
    }

    public async Task<PublicCambridgeEntry?> Get(string word, WordLanguage language, CancellationToken cancellationToken)
    {
        var entry = await _cambridge.AsNoTracking().OfType<IParsedPageEntry>().FirstOrDefaultKey(word, language, WordLanguage.En, cancellationToken);
        if (entry != null)
        {
            return CambridgeEntry.CreatePublic(entry);
        }
        
        var cambridgePage =  await _builder.Build(word, language, cancellationToken);
        if (cambridgePage == null)
        {
            return null;
        }
        
        var cambridgeEntry = CambridgeEntry.Create(cambridgePage);
        await _cambridge.AddAsync(cambridgeEntry, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return cambridgeEntry.CreatePublic();
        
    }
}