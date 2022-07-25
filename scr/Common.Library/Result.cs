namespace Common.Library;

using System;

public class Result<T> : Either<T, Exception>
{
    public Result(T left) : base(left)
    {
    }

    public Result(Exception right) : base(right)
    {
    }

    public static implicit operator Result<T>(T value) => new(value);

    public static implicit operator Result<T>(Exception value) => new(value);
}
