﻿namespace AdventOfCode2022.day14;

using Common;

public class Day14 : Base2022<int>
{
    private static readonly Point<int> SandDrop = new(500, 0);

    public override async ValueTask<int> ExecutePart1(string fileName)
    {
        return await HandleSandDrop(fileName);
    }

    public override async ValueTask<int> ExecutePart2(string fileName)
    {
        return await HandleSandDrop(fileName, true);
    }

    private static async Task<int> HandleSandDrop(string file, bool stopAtTop = false)
    {
        var (result, maxY) = await ParseFile(file, stopAtTop);
        var set = new HashSet<Point<int>>();
        while (true)
        {
            if (stopAtTop && set.Contains(SandDrop))
            {
                break;
            }

            var dropPoint = FindDropPoint(SandDrop, result, set, maxY);
            if (dropPoint is null)
            {
                break;
            }

            set.Add(dropPoint.Value);
        }

        return set.Count;
    }

    private static Point<int>? FindDropPoint(
        Point<int> start,
        IReadOnlyList<RockFormation> result,
        IReadOnlySet<Point<int>> sand,
        int maxY)
    {
        for (var i = start.Y; i <= maxY; i++)
        {
            var updated = new Point<int>(start.X, i);
            if (!sand.Contains(updated) && !ResultsContain(result, updated))
            {
                continue;
            }

            var left = updated with { X = updated.X - 1 };
            var right = updated with { X = updated.X + 1 };
            if (!sand.Contains(left) && !ResultsContain(result, left))
            {
                return FindDropPoint(left, result, sand, maxY);
            }

            if (!sand.Contains(right) && !ResultsContain(result, right))
            {
                return FindDropPoint(right, result, sand, maxY);
            }

            return new Point<int>(start.X, i - 1);
        }

        return null;
    }

    private static bool ResultsContain(IReadOnlyList<RockFormation> formations, Point<int> point)
    {
        for (var i = 0; i < formations.Count; ++i)
        {
            var rockFormation = formations[i];
            if (rockFormation.IsInFormation(point))
            {
                return true;
            }
        }

        return false;
    }

    private static async ValueTask<(IReadOnlyList<RockFormation>, int)> ParseFile(string file, bool hasFloor = false)
    {
        var formations = new List<RockFormation>();
        var maxY = 0;
        await foreach (var line in File.ReadLinesAsync(file))
        {
            var (rockFormation, y) = ParseLine(line);
            if (y > maxY)
            {
                maxY = y;
            }

            //Console.WriteLine(rockFormation);
            formations.Add(rockFormation);
        }

        if (hasFloor)
        {
            maxY += 2;
            var line = new Line<int>(new Point<int>(int.MinValue, maxY), new Point<int>(int.MaxValue, maxY));
            formations.Add(new RockFormation(new[] { line }));
        }

        return (formations, maxY);
    }

    private static (RockFormation, int) ParseLine(string line)
    {
        var maxY = 0;
        var lines = new List<Line<int>>();
        var points = line.Split(" -> ");
        for (var i = 1; i < points.Length; ++i)
        {
            var p1 = ParsePoint(points[i - 1]);
            var p2 = ParsePoint(points[i]);
            if (p1.Y > maxY)
            {
                maxY = p1.Y;
            }

            if (p2.Y > maxY)
            {
                maxY = p2.Y;
            }

            lines.Add(new Line<int>(p1, p2));
        }

        return (new RockFormation(lines), maxY);
    }

    private static Point<int> ParsePoint(string point)
    {
        var indexes = point.Split(',');
        return new Point<int>(int.Parse(indexes[0]), int.Parse(indexes[1]));
    }
}