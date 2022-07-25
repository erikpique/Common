namespace Common.Library;

using System;
using System.Linq.Expressions;

public interface IRepositoryProjection<TEntity, TKey>
    where TEntity : notnull, Entity<TKey>
{
    Task<Maybe<TProjection>> ProjectionAsync<TProjection>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TProjection>> projection,
        params string[] includeProperties);

    Task<Maybe<IReadOnlyCollection<TProjection>>> ProjectionsAsync<TProjection>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TProjection>> projection,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        params string[] includeProperties);
}
