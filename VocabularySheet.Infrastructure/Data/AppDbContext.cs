﻿using Microsoft.EntityFrameworkCore;
using VocabularySheet.Domain;
using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Infrastructure.Data.Interfaces;

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
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite();
    }
}
