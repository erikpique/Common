namespace Common.Library
{
    public interface IMaybe<out T>
    {
        bool HasValue { get; }

        T Value { get; }
    }
}
