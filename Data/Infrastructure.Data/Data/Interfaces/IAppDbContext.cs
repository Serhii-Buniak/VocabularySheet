using Domain.Common;
using Domain.WordModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Data.Interfaces;

public interface IAppDbContext
{
    DbSet<Word> Words { get; }
    DbSet<ConfigEntry> Configs { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
