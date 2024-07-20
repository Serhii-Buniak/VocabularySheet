using Application.Common.Commons.Dtos;
using Application.Common.Commons.Interfaces;
using Domain.Localization;
using Domain.WebSources;
using Infrastructure.Data.Commons;
using Infrastructure.Data.Commons.Lockers;
using Infrastructure.Data.Data;
using Infrastructure.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Tools.Common.Extensions;
using WebSources.CambridgeDictionary;

namespace Infrastructure.Data.Repositories.Pages;

public class CambridgeRepository : ICambridgeRepository, ITextToSpeechProvider, IWordDescriptionProvider
{
    private static readonly SemaphoreLocker Locks = new ();

    public TextToSpeechProviderId TextToSpeechId => TextToSpeechProviderId.Cambridge;
    public WordDescriptionProviderId WordDescriptionId => WordDescriptionProviderId.Cambridge;

    
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
        var entry = await _cambridge.AsNoTracking()
            .OfType<IParsedPageEntry>()
            .FirstOrDefaultKey(word, language, WordLanguage.En, cancellationToken);
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
        using var _ = await Locks.LockKey(cambridgeEntry.CacheKey(), cancellationToken);
        
        bool isExist = await _cambridge.AsNoTracking()
            .OfType<IParsedPageEntry>()
            .IsExist(cambridgeEntry.Word, cambridgeEntry.Language, cambridgeEntry.TranslationLanguage, cancellationToken);
        if (!isExist)
        {
            await _cambridge.AddAsync(cambridgeEntry, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return cambridgeEntry.CreatePublic();
    }

    public async Task<TextToSpeechResult?> GetTextToSpeech(WordWithLanguage word, CancellationToken cancellationToken)
    {
        var cambridgeEntry = await Get(word.Word, word.Language, cancellationToken);

        var cambridgeAudio = cambridgeEntry?.Content.Blocks.SelectMany(b => b.Audios).SelectMany(a => a.Links).Random();
        if (cambridgeAudio == null)
        {
            return null;
        }
        
        return new TextToSpeechResult(TextToSpeechId)
        {
            Url = cambridgeAudio.FullLink()
        };
    }

    public async Task<WordDescriptionResult?> GetWordDescription(WordWithLanguage word, CancellationToken cancellationToken)
    {
        if (word.Language != WordLanguage.En)
        {
            return null;
        }

        var cambridgeEntry = await Get(word.Word, word.Language, cancellationToken);

        var subArticles = cambridgeEntry?.Content.Blocks
            .SelectMany(b => b.Articles)
            .SelectMany(a => a.SubArticles)
            .ToList();
        if (subArticles?.Count is 0 or null)
        {
            return null;
        }

        var descriptionBlocks = subArticles.Select(a =>
        {
            var examples = a.Examples.Select(ex => $"- {ex}");
            return $"""
                    {a.Header.Title}
                    {string.Join("\n", examples)}
                    """;
        });
        
        return new WordDescriptionResult(WordDescriptionId)
        {
            Text = string.Join("\n\n", descriptionBlocks).Replace("\n\n\n", "\n\n")
        };
    }
}