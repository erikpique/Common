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
        CancellationToken cancellationToken = default,
        params string[] includeProperties)
    {
        var query = Context.Set<TEntity>().Where(predicate);

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        if (orderBy is not null)
        {
            return await orderBy(query).ToListAsync(cancellationToken);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Maybe<TEntity>> GetByIdAsync(TKey id, CancellationToken cancellationToken = default) =>
        await Context.Set<TEntity>().FindAsync(new[] { id }, cancellationToken);

    public async Task<Maybe<TProjection>> ProjectionAsync<TProjection>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TProjection>> projection,
        CancellationToken cancellationToken = default,
        params string[] includeProperties)
    {
        var query = Context.Set<TEntity>().AsNoTracking().Where(predicate);

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.Select(projection).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Maybe<IReadOnlyCollection<TProjection>>> ProjectionsAsync<TProjection>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TProjection>> projection,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        CancellationToken cancellationToken = default,
        params string[] includeProperties)
    {
        var query = Context.Set<TEntity>().AsNoTracking().Where(predicate);

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        return await query.Select(projection).ToListAsync(cancellationToken);
    }
}
