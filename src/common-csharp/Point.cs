namespace Common;

using System.Numerics;

public readonly record struct Point<T>(T X, T Y) where T : INumber<T>
{
    public void Deconstruct(out T x, out T y)
    {
        x = X;
        y = Y;
    }

    public T ManhattanDistance(in Point<T> other) => T.Abs(X - other.X) + T.Abs(Y - other.Y);

    public static T ManhattanDistance(in Point<T> first, in Point<T> second) => first.ManhattanDistance(in second);
}
