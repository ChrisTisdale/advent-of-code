namespace AdventOfCode2022.day14;

using Common;

public class Day14 : Base2022
{
    private static readonly Point<int> SandDrop = new(500, 0);
    
    public override async ValueTask ExecutePart1()
    {
        await HandleSandDrop(GetFileLocation("sample.txt"));
        await HandleSandDrop(GetFileLocation("measurements.txt"));
    }

    public override async ValueTask ExecutePart2()
    {
        await HandleSandDrop(GetFileLocation("sample.txt"), true);
        await HandleSandDrop(GetFileLocation("measurements.txt"), true);
    }

    private static async ValueTask HandleSandDrop(string file, bool stopAtTop = false)
    {
        var (result, maxY) = await ParseFile(file, stopAtTop);
        var set = new HashSet<Point<int>>();
        Point<int>? top = null;
        while (true)
        {
            if (stopAtTop && top == SandDrop)
            {
                break;
            }
            
            if (!top.HasValue)
            {
                top = FindDropPoint(SandDrop, result, set, maxY);
                if (top is null)
                {
                    break;
                }

                set.Add(top.Value);
                //Console.WriteLine($"Item-{set.Count}: {top.Value}");
            }
            else
            {
                var left = top.Value with { X = top.Value.X - 1 };
                var right = top.Value with { X = top.Value.X + 1 };
                if (!set.Contains(left) && !ResultsContain(result, left))
                {
                    var findDropPoint = FindDropPoint(left, result, set, maxY);
                    if (findDropPoint is null)
                    {
                        break;
                    }
                    
                    set.Add(findDropPoint.Value);
                    //Console.WriteLine($"Item-{set.Count}: {findDropPoint.Value}");
                }
                else if (!set.Contains(right) && !ResultsContain(result, right))
                {
                    var findDropPoint = FindDropPoint(right, result, set, maxY);
                    if (findDropPoint is null)
                    {
                        break;
                    }
                    
                    set.Add(findDropPoint.Value);
                    //Console.WriteLine($"Item-{set.Count}: {findDropPoint.Value}");
                }
                else
                {
                    top = top.Value with { Y = top.Value.Y - 1 };
                    set.Add(top.Value);
                    //Console.WriteLine($"Item-{set.Count}: {top.Value}");
                }
            }
        }
        
        Console.WriteLine($"{file} has {set.Count} dropped");
    }

    private static Point<int>? FindDropPoint(
        Point<int> start,
        IReadOnlyList<RockFormation> result,
        HashSet<Point<int>> sand,
        int maxY)
    {
        for (var i = start.Y; i <= maxY; i++)
        {
            var updated = new Point<int>(start.X, i);
            if (sand.Contains(updated) || ResultsContain(result, updated))
            {
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