namespace Common.Library;

public interface IRepository<TEntity, TKey> : IRepositoryReadOnly<TEntity, TKey>
    where TEntity : notnull, AggregateRoot<TKey>
{
    Task AddAsync(TEntity entity);

    Task AddAsync(IEnumerable<TEntity> entities);

    Task RemoveAsync(TEntity entity);

    Task RemoveAsync(IEnumerable<TEntity> entities);
}
