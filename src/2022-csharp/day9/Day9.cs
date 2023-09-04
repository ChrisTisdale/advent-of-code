namespace AdventOfCode2022.day9;

using Common;

public class Day9 : Base2022AdventOfCodeDay<int>
{
    private static readonly IReadOnlyDictionary<Part, string[]> Files = new Dictionary<Part, string[]>
    {
        { Part.Part1, new[] { "samplePart1.txt", "measurements.txt" } },
        { Part.Part2, new[] { "samplePart2.txt", "measurements.txt" } }
    }.AsReadOnly();

    public Day9()
        : base(Files)
    {
    }

    public override async ValueTask<int> ExecutePart1(Stream fileName)
    {
        var result = await GetUniqueSpaces(ProcessFile(fileName), 0);
        return result;
    }

    public override async ValueTask<int> ExecutePart2(Stream fileName)
    {
        var result = await GetUniqueSpaces(ProcessFile(fileName), 8);
        return result;
    }

    private static async IAsyncEnumerable<Input> ProcessFile(Stream fileName)
    {
        using var sr = new StreamReader(fileName);
        while (!sr.EndOfStream)
        {
            var line = await sr.ReadLineAsync();
            if (line == null)
            {
                continue;
            }

            var inputs = line.Split(' ');
            yield return new Input(inputs[0][0], int.Parse(inputs[1]));
        }
    }

    private static async ValueTask<int> GetUniqueSpaces(IAsyncEnumerable<Input> inputs, int middleCount)
    {
        var middlePoints = new Point<int>[middleCount + 2];
        var set = new HashSet<Point<int>> { middlePoints.Last() };
        await foreach (var input in inputs)
        {
            for (var i = 0; i < input.Moves; ++i)
            {
                middlePoints[0] = input.Direction switch
                {
                    'U' => middlePoints[0] with { Y = middlePoints[0].Y + 1 },
                    'D' => middlePoints[0] with { Y = middlePoints[0].Y - 1 },
                    'L' => middlePoints[0] with { X = middlePoints[0].X - 1 },
                    'R' => middlePoints[0] with { X = middlePoints[0].X + 1 },
                    _ => throw new ArgumentException(nameof(input.Direction))
                };

                for (var j = 1; j < middlePoints.Length; ++j)
                {
                    middlePoints[j] = GetTailLocation(middlePoints[j - 1], middlePoints[j]);
                }

                set.Add(middlePoints[^1]);
            }
        }

        return set.Count;
    }

    private static Point<int> GetTailLocation(Point<int> head, Point<int> tail)
    {
        var xNeedsUpdate = Math.Abs(tail.X - head.X) > 1;
        var yNeedsUpdate = Math.Abs(tail.Y - head.Y) > 1;
        if (xNeedsUpdate && tail.Y != head.Y || yNeedsUpdate && tail.X != head.X)
        {
            return new Point<int>(tail.X > head.X ? tail.X - 1 : tail.X + 1, tail.Y > head.Y ? tail.Y - 1 : tail.Y + 1);
        }

        if (xNeedsUpdate)
        {
            return tail with { X = tail.X > head.X ? tail.X - 1 : tail.X + 1 };
        }

        if (yNeedsUpdate)
        {
            return tail with { Y = tail.Y > head.Y ? tail.Y - 1 : tail.Y + 1 };
        }

        return tail;
    }
}
