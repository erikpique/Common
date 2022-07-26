namespace Common.Library;

public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<IDomainEvent>
{
    private readonly IList<IDomainEvent> _domainEvents = new List<IDomainEvent>();

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.ToList();

    public void AddDomainEvent(IDomainEvent eventItem) => _domainEvents.Add(eventItem);

    public void ClearEvents() => _domainEvents.Clear();
}
