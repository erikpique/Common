﻿namespace Common.EFCore.Implementation;

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

    public async Task AddAsync(TEntity entity) => await Context.Set<TEntity>().AddAsync(entity);

    public async Task AddAsync(IEnumerable<TEntity> entities) => await Context.Set<TEntity>().AddRangeAsync(entities);

    public Task RemoveAsync(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);

        return Task.CompletedTask;
    }

    public Task RemoveAsync(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().RemoveRange(entities);

        return Task.CompletedTask;
    }
}