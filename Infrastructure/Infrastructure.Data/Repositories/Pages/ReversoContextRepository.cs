using Application.Common.Commons.Dtos;
using Application.Common.Commons.Interfaces;
using Domain.Localization;
using Domain.WebSources;
using Infrastructure.Data.Commons;
using Infrastructure.Data.Commons.Lockers;
using Infrastructure.Data.Data;
using Infrastructure.Data.Repositories.Configurations;
using Infrastructure.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebSources.ReversoContext;

namespace Infrastructure.Data.Repositories.Pages;

public class ReversoContextRepository : IReversoContextRepository, IWordDescriptionProvider
{
    private static readonly SemaphoreLocker Locks = new ();
    public WordDescriptionProviderId WordDescriptionId => WordDescriptionProviderId.ReversoContext;
    
    private readonly DbSet<ReversoContextEntry> _reverso;
    private readonly ReversoContextPageBuilder _builder;
    private readonly LocalizationConfigurator _localizationConfigurator;
    private readonly AppDbContext _context;

    public ReversoContextRepository(AppDbContext context, ReversoContextPageBuilder builder, LocalizationConfigurator localizationConfigurator)
    {
        _context = context;
        _reverso = context.ReversoContext;
        _builder = builder;
        _localizationConfigurator = localizationConfigurator;
    }

    public async Task<PublicReversoContextEntry?> Get(string word, WordLanguage language, WordLanguage translateLanguage, CancellationToken cancellationToken)
    {
        using var _ = await Locks.LockKey($"{word}_{language}_{translateLanguage}", cancellationToken);

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
        
        bool isExist = await _reverso.AsNoTracking().OfType<IParsedPageEntry>().IsExist(reversoEntry.Word,
            reversoEntry.Language, reversoEntry.TranslationLanguage, cancellationToken);
        if (!isExist)
        {
            await _reverso.AddAsync(reversoEntry, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        return reversoEntry.CreatePublic();
        
    }

    public async Task<WordDescriptionResult?> GetWordDescription(WordWithLanguage word, CancellationToken cancellationToken)
    {
        var localizationConfig = await _localizationConfigurator.Get(cancellationToken); 
        var translateLanguage = localizationConfig.SetOfLang().FirstOrDefault(l => l != word.Language);
        
        var reversoContext = await Get(word.Word, word.Language, translateLanguage, cancellationToken);

        var subArticles = reversoContext?.Content.Examples.Select(ex => $"- {ex.Origin}").ToList();
        if (subArticles?.Count is 0 or null)
        {
            return null;
        }
        
        return new WordDescriptionResult(WordDescriptionId)
        {
            Text = string.Join("\n", subArticles)
        };
    }
}