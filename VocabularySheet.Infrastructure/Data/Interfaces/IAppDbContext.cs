using Microsoft.EntityFrameworkCore;
using VocabularySheet.Domain;
using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.Infrastructure.Data.Interfaces;

public interface IAppDbContext
{
    DbSet<Word> Words { get; }
    DbSet<ConfigEntry> Configs { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
