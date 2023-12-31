﻿using Microsoft.EntityFrameworkCore;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Common;
using VocabularySheet.Domain.Pages;
using VocabularySheet.Infrastructure.Commons;
using VocabularySheet.Infrastructure.Data;
using VocabularySheet.ReversoContext;

namespace VocabularySheet.Infrastructure.Repositories.Pages;

public class ReversoContextRepository : IReversoContextRepository
{
    private readonly DbSet<ReversoContextEntry> _reverso;
    private readonly ReversoContextPageBuilder _builder;
    private readonly AppDbContext _context;

    public ReversoContextRepository(AppDbContext context, ReversoContextPageBuilder builder)
    {
        _context = context;
        _reverso = context.ReversoContext;
        _builder = builder;
    }

    public async Task<PublicReversoContextEntry?> Get(string word, WordLanguage language, WordLanguage translateLanguage, CancellationToken cancellationToken)
    {
        var entry = await _reverso.AsNoTracking().OfType<IParsedPageEntry>()
            .FirstOrDefaultKey(word, language, translateLanguage, cancellationToken);
        if (entry != null)
        {
            return ReversoContextEntry.CreatePublic(entry);
        }

        var reversoPage = await _builder.Build(word, language, translateLanguage, cancellationToken);
        if (reversoPage == null)
        {
            return null;
        }
        
        var reversoEntry = ReversoContextEntry.Create(reversoPage);
        await _reverso.AddAsync(reversoEntry, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return reversoEntry.CreatePublic();
        
    }
}