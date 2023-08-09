using Microsoft.EntityFrameworkCore;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Domain;

namespace VocabularySheet.Infrastructure.Data;

/// <summary>
///     dotnet ef migrations add Initial --startup-project ./VocabularySheet.Infrastructure --project ./VocabularySheet.Infrastructure
/// </summary>
public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Word> Words => Set<Word>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite();
    }
}
