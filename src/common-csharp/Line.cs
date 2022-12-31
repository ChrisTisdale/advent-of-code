namespace Common;

using System.Collections;
using System.Numerics;

public readonly record struct Line<T> : IEnumerable<Point<T>> where T : INumber<T>
{
    public Line(Point<T> one, Point<T> two, T increment)
    {
        One = one;
        Two = two;
        Increment = increment;
        CheckForValidLine();
    }

    public Line(Point<T> one, Point<T> two) : this(one, two, T.One)
    {
    }

    public Point<T> One { get; init; }

    public Point<T> Two { get; init; }

    public T Increment { get; init; }

    private T XDistance => T.Abs(One.X - Two.X);

    private T YDistance => T.Abs(One.Y - Two.Y);

    public IEnumerator<Point<T>> GetEnumerator()
    {
        if (One.X == Two.X)
        {
            var end = One.Y > Two.Y ? One.Y : Two.Y;
            for (var i = One.Y > Two.Y ? Two.Y : One.Y; i <= end; i += Increment)
            {
                var res = One with { Y = i };
                yield return res;
            }
        }
        else if (One.Y == Two.Y)
        {
            var end = One.X > Two.X ? One.X : Two.X;
            for (var i = One.X > Two.X ? Two.X : One.X; i <= end; i += Increment)
            {
                var res = One with { X = i };
                yield return res;
            }
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public T ManhattanDistance() => Point<T>.ManhattanDistance(One, Two);

    public bool IsInLine(Point<T> point)
    {
        if (point == One || point == Two)
        {
            return true;
        }

        if (One.X == Two.X)
        {
            return point.X == One.X &&
                   (One.Y > Two.Y
                       ? point.Y >= Two.Y && point.Y <= One.Y
                       : point.Y >= One.Y && point.Y <= Two.Y);
        }

        if (One.Y == Two.Y)
        {
            return point.Y == One.Y &&
                   (One.X > Two.X
                       ? point.X >= Two.X && point.X <= One.X
                       : point.X >= One.X && point.X <= Two.X);
        }

        throw new NotImplementedException();
    }

    public IEnumerable<Point<T>> GetPoints() => this;

    public void Deconstruct(out Point<T> one, out Point<T> two)
    {
        one = One;
        two = Two;
    }

    public void Deconstruct(out Point<T> one, out Point<T> two, out T increment)
    {
        one = One;
        two = Two;
        increment = Increment;
    }

    private void CheckForValidLine()
    {
        var tType = typeof(T);
        if (tType == typeof(decimal) || tType == typeof(float))
        {
            return;
        }

        var xDiff = XDistance;
        var yDiff = YDistance;
        if (xDiff % Increment != T.Zero || yDiff % Increment != T.Zero)
        {
            throw new InvalidDataException("The line can't make valid points");
        }
    }
}
