namespace Common.Library;

public interface IAggregateRoot<TDomainEvent>
    where TDomainEvent : notnull
{
    IReadOnlyList<TDomainEvent> DomainEvents { get; }

    void AddDomainEvent(TDomainEvent eventItem);

    void ClearEvents();
}
