namespace Common.Library;

using System;

public abstract class Either<TLeft, TRight>
{
    private readonly TLeft _left;
    private readonly TRight _right;

    protected readonly bool IsLeft;

    protected Either(TLeft left)
    {
        _left = left;
        IsLeft = true;
    }

    protected Either(TRight right)
    {
        _right = right;
        IsLeft = false;
    }

    public T Match<T>(Func<TLeft, T> left, Func<TRight, T> right) =>
        IsLeft ? left(_left) : right(_right);
}
