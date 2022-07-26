namespace Common.Library;

public sealed class Maybe<T> : IMaybe<T>
{
    public static readonly Maybe<T> Nothing = new();

    public bool HasValue { get; }

    public T Value { get; }

    private Maybe() { }

    public Maybe(T value)
    {
        HasValue = !Equals(value, null);
        Value = value;
    }

    public static implicit operator Maybe<T>(T value) => value is not null ? new(value) : Nothing;
}
