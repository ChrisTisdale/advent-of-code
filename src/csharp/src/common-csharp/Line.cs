﻿// Copyright (c) Christopher Tisdale 2024.
//
// Licensed under BSD-3-Clause.
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      https://spdx.org/licenses/BSD-3-Clause.html
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
        else if (T.Abs(One.Y - Two.Y) == T.Abs(One.X - Two.X) &&
                 T.Abs(One.X - Two.X) % Increment == T.Zero)
        {
            var incrementX = One.X < Two.X;
            var incrementY = One.Y < Two.Y;
            for (var i = T.Zero; i <= T.Abs(One.X - Two.X); i += Increment)
            {
                yield return new Point<T>(
                    incrementX ? One.X + i : One.X - i,
                    incrementY ? One.Y + i : One.Y - i);
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

        return GetPoints().Contains(point);
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
