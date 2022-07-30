namespace Common.Library;

using System;
using System.Linq.Expressions;

public interface IRepositoryReadOnly<TEntity, TKey>
    where TEntity : notnull, Entity<TKey>
{
    Task<Maybe<TEntity>> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

    Task<Maybe<IEnumerable<TEntity>>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        CancellationToken cancellationToken = default,
        params string[] includeProperties);
}
