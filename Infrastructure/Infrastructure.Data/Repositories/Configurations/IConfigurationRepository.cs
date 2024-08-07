﻿using Application.Common.Commons.Interfaces;
using Domain.Common;
using Infrastructure.Data.Commons;
using Infrastructure.Data.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories.Configurations;

public abstract class BaseConfigurator<T> : IConfigurator<T> where T : BaseConfigurationEntity<T>, new()
{
    private readonly IAppDbContext _context;
    public abstract ConfigType Type  { get; }
    private DbSet<ConfigEntry> Configs { get; }
    
    protected BaseConfigurator(IAppDbContext context)
    {
        _context = context;
        Configs = context.Configs;
    }

    public async Task<T> Get(CancellationToken cancellationToken)
    {
        var configEntity = await Ensure(cancellationToken);
        return BaseConfigurationEntity<T>.Create(configEntity.JsonData) ?? new T();
    }
    
    public async Task<T> Set(T configuration, CancellationToken cancellationToken)
    {
        ConfigEntry entry = await Ensure(cancellationToken);
        entry.JsonData = configuration.ToJsonString();
        Configs.Update(entry);
        await _context.SaveChangesAsync(cancellationToken);

        return configuration;
    }
    
    public async Task<T> Set(Func<T, T> func, CancellationToken cancellationToken)
    {
        var configuration = await Get(cancellationToken);
        configuration = func.Invoke(configuration);
        return await Set(configuration, cancellationToken);
    }
    
    private async Task<ConfigEntry> Ensure(CancellationToken cancellationToken)
    {
        ConfigEntry? configEntity = await Configs.FirstOrDefaultId(Type, cancellationToken);

        if (configEntity == null)
        {
            var entry = await Configs.AddAsync(new ConfigEntry()
            {
                Id = Type,
                JsonData = new T().ToJsonString()
            }, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            configEntity = entry.Entity;
        }
        
        return configEntity;
    }
}