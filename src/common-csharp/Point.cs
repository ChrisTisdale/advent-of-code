namespace Common;

using System.Numerics;

public readonly record struct Point<T>(T X, T Y, T Increment) where T : INumber<T>
{
    public Point(T x, T y) : 
        this(x, y, T.One)
    {
    }

    public void Deconstruct(out T x, out T y)
    {
        x = X;
        y = Y;
    }
    
    public void Deconstruct(out T x, out T y, out T increment)
    {
        x = X;
        y = Y;
        increment = Increment;
    }
}