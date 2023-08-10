using Microsoft.EntityFrameworkCore;
using VocabularySheet.Domain;

namespace VocabularySheet.Infrastructure.Data.Interfaces;

public interface IAppDbContext
{
    DbSet<Word> Words { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
