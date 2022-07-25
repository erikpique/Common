namespace Common.Library;

using System;

public abstract class Either<TLeft, TRight>
{
    private readonly bool _isLeft;
    private readonly TLeft _left;
    private readonly TRight _right;

    protected Either(TLeft left)
    {
        _left = left;
        _isLeft = true;
    }

    protected Either(TRight right)
    {
        _right = right;
        _isLeft = false;
    }

    public T Match<T>(Func<TLeft, T> left, Func<TRight, T> right) =>
        _isLeft ? left(_left) : right(_right);
}
