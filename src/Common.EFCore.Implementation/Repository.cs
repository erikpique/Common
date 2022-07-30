namespace Common.EFCore.Implementation;

using Common.Library;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public abstract class Repository<TContext, TEntity, TKey> : RepositoryReadOnly<TContext, TEntity, TKey>, IRepository<TEntity, TKey>
    where TEntity : notnull, AggregateRoot<TKey>
    where TContext : notnull, DbContext
{
    protected Repository(TContext context)
        : base(context)
    {
        Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default) => 
        await Context.Set<TEntity>().AddAsync(entity, cancellationToken);

    public async Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) => 
        await Context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);

    public Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Context.Set<TEntity>().Remove(entity);

        return Task.CompletedTask;
    }

    public Task RemoveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        Context.Set<TEntity>().RemoveRange(entities);

        return Task.CompletedTask;
    }
}
