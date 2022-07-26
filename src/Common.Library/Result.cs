namespace Common.Library;

using System;

public sealed class Result<T> : Either<T, Exception>
{
    public Result(T left) : base(left)
    {
    }

    public Result(Exception right) : base(right)
    {
    }

    public bool Ok => IsLeft;

    public static implicit operator Result<T>(T value) => new(value);

    public static implicit operator Result<T>(Exception value) => new(value);
}
