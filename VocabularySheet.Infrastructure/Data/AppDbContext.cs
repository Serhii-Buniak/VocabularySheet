using Microsoft.EntityFrameworkCore;
using VocabularySheet.Domain;
using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Infrastructure.Commons;
using VocabularySheet.Infrastructure.Data.Interfaces;
using VocabularySheet.Infrastructure.Repositories.Pages;

namespace VocabularySheet.Infrastructure.Data;

/// <summary>
///     dotnet ef migrations add Initial --startup-project ./VocabularySheet.Infrastructure --project ./VocabularySheet.Infrastructure
/// </summary>
public sealed class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Word> Words => Set<Word>();
    public DbSet<ConfigEntry> Configs => Set<ConfigEntry>();
    public DbSet<CambridgeEntry> Cambridge => Set<CambridgeEntry>();
    public DbSet<ReversoContextEntry> ReversoContext => Set<ReversoContextEntry>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CambridgeEntry>().HasKeyPageEntry();
        modelBuilder.Entity<ReversoContextEntry>().HasKeyPageEntry();
        
        modelBuilder.Entity<Word>()
            .HasIndex(w => w.Original)
            .IsUnique();

        modelBuilder.Entity<Word>()
            .HasIndex(w => w.Translation);

        modelBuilder.Entity<Word>()
            .HasIndex(w => w.Category);
    }
}
