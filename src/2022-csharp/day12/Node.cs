namespace AdventOfCode2022.day12;

using System.Numerics;
using Common;

internal record Node<TValue, TPoint>(TValue Data, bool Start, bool End, Point<TPoint> Point)
    where TPoint : INumber<TPoint>;
