namespace Common.Library;

public interface IRepository<TEntity, TKey> : IRepositoryReadOnly<TEntity, TKey>
    where TEntity : notnull, AggregateRoot<TKey>
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task RemoveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}
