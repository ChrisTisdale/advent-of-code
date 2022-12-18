namespace Common;

using System.Drawing;
using System.Numerics;

public readonly record struct Line<T> where T : INumber<T>
{
    public Line(Point<T> one, Point<T> two)
    {
        One = one;
        Two = two;
        if (one.Increment != two.Increment)
        {
            throw new InvalidDataException($"The increment for the points must match");
        }
    }

    public Point<T> One { get; init; }

    public Point<T> Two { get; init; }

    public bool IsInLine(Point<T> point)
    {
        if (point.X != One.X && point.Y != One.Y)
        {
            return false;
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

    public IEnumerable<Point<T>> GetPoints()
    {
        if (One.X == Two.X)
        {
            var end = One.Y > Two.Y ? One.Y : Two.Y;
            for (var i = One.Y > Two.Y ? Two.Y : One.Y; i <= end; i += One.Increment)
            {
                var res = One with { Y = i };
                yield return res;
            }
        }
        else if (One.Y == Two.Y)
        {
            var end = One.X > Two.X ? One.X : Two.X;
            for (var i = One.X > Two.X ? Two.X : One.X; i <= end; i += One.Increment)
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

    public void Deconstruct(out Point<T> one, out Point<T> two)
    {
        one = One;
        two = Two;
    }
}
