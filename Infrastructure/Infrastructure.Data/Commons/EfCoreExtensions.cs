using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Commons;

public static class EfCoreExtensions
{
    public static async Task<TEntity?> FirstOrDefaultId<TEntity, TId>(this IQueryable<TEntity> entities, TId id, CancellationToken cancellationToken)
        where TEntity : class, IEntity<TId>
        where TId : notnull
    {
        return await entities.FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
    }
    
    public static IQueryable<TId> SelectId<TEntity, TId>(this IQueryable<TEntity> entities)
        where TEntity : class, IEntity<TId>
        where TId : notnull
    {
        return entities.Select(e => e.Id);
    }
}