namespace AdventOfCode2022.day9;

using Common;

public class Day9 : Base2022<int>
{
    public override ValueTask<int> ExecutePart1(string fileName)
    {
        // TODO process part 1
        return ExecutePart2(fileName);
    }

    public override async ValueTask<int> ExecutePart2(string fileName)
    {
        var result = await GetUniqueSpaces(ProcessFile(fileName));
        return result;
    }

    private static async IAsyncEnumerable<Input> ProcessFile(string fileName)
    {
        await foreach (var line in File.ReadLinesAsync(fileName))
        {
            var inputs = line.Split(' ');
            yield return new Input(inputs[0][0], int.Parse(inputs[1]));
        }
    }

    private static async ValueTask<int> GetUniqueSpaces(IAsyncEnumerable<Input> inputs)
    {
        var head = new Point<int>(0, 0);
        var middlePoints = new Point<int>[8];
        var tail = new Point<int>(0, 0);
        var set = new HashSet<Point<int>> { tail };
        await foreach (var input in inputs)
        {
            for (var i = 0; i < input.Moves; ++i)
            {
                head = input.Direction switch
                {
                    'U' => head with { Y = head.Y + 1 },
                    'D' => head with { Y = head.Y - 1 },
                    'L' => head with { X = head.X - 1 },
                    'R' => head with { X = head.X + 1 },
                    _ => throw new ArgumentException()
                };

                for (var j = 0; j < middlePoints.Length; ++j)
                {
                    middlePoints[j] = GetTailLocation(j == 0 ? head : middlePoints[j - 1], middlePoints[j]);
                }

                tail = GetTailLocation(middlePoints[^1], tail);
                set.Add(tail);
            }
        }

        return set.Count;
    }

    private static Point<int> GetTailLocation(Point<int> head, Point<int> tail)
    {
        var xNeedsUpdate = Math.Abs(tail.X - head.X) > 1;
        var yNeedsUpdate = Math.Abs(tail.Y - head.Y) > 1;
        if ((xNeedsUpdate && tail.Y != head.Y) || (yNeedsUpdate && tail.X != head.X))
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
