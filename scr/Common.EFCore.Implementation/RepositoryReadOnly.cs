namespace Common.EFCore.Implementation;

using Common.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

public abstract class RepositoryReadOnly<TContext, TEntity, TKey> : IRepositoryReadOnly<TEntity, TKey>, IRepositoryProjection<TEntity, TKey>
    where TEntity : notnull, Entity<TKey>
    where TContext : notnull, DbContext
{
    protected readonly TContext Context;

    protected RepositoryReadOnly(TContext context)
    {
        Context = context;
        Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task<Maybe<IEnumerable<TEntity>>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        params string[] includeProperties)
    {
        var query = Context.Set<TEntity>().Where(predicate);

        foreach (var includeProperty in includeProperties)
            query = query.Include(includeProperty);

        if (orderBy is not null)
            return await orderBy(query).ToListAsync();

        var data = await query.ToListAsync();

        return new Maybe<IEnumerable<TEntity>>(data);
    }

    public async Task<Maybe<TEntity>> GetByIdAsync(TKey id)
    {
        var data = await Context.Set<TEntity>().FindAsync(id);

        return new Maybe<TEntity>(data);
    }

    public async Task<Maybe<TProjection>> ProjectionAsync<TProjection>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TProjection>> projection,
        params string[] includeProperties)
    {
        var query = Context.Set<TEntity>().AsNoTracking().Where(predicate);

        foreach (var includeProperty in includeProperties)
            query = query.Include(includeProperty);

        var data = await query.Select(projection).FirstOrDefaultAsync();

        return new Maybe<TProjection>(data);
    }

    public async Task<Maybe<IReadOnlyCollection<TProjection>>> ProjectionsAsync<TProjection>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TProjection>> projection,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        params string[] includeProperties)
    {
        var query = Context.Set<TEntity>().AsNoTracking().Where(predicate);

        foreach (var includeProperty in includeProperties)
            query = query.Include(includeProperty);

        if (orderBy is not null)
            query = orderBy(query);

        var data = await query.Select(projection).ToListAsync();

        return new Maybe<IReadOnlyCollection<TProjection>>(data);
    }
}
