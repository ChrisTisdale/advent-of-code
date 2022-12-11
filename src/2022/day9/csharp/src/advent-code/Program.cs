var result = await GetUniqueSpaces(ProcessFile("sample.txt"));
Console.WriteLine($"Sample Found: {result}");

result = await GetUniqueSpaces(ProcessFile("measurements.txt"));
Console.WriteLine($"Measurements Found: {result}");

async IAsyncEnumerable<Input> ProcessFile(string fileName)
{
    await foreach (var line in File.ReadLinesAsync(fileName))
    {
        var inputs = line.Split(' ');
        yield return new Input(inputs[0][0], int.Parse(inputs[1]));
    }
}

async ValueTask<int> GetUniqueSpaces(IAsyncEnumerable<Input> inputs)
{
    var head = new Point(0, 0);
    var middlePoints = new Point[8];
    var tail = new Point(0, 0);
    var set = new HashSet<Point> { tail };
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

Point GetTailLocation(Point head, Point tail)
{
    var xNeedsUpdate = Math.Abs(tail.X - head.X) > 1;
    var yNeedsUpdate = Math.Abs(tail.Y - head.Y) > 1;
    if ((xNeedsUpdate && tail.Y != head.Y) || (yNeedsUpdate && tail.X != head.X))
    {
        return new Point(tail.X > head.X ? tail.X - 1 : tail.X + 1, tail.Y > head.Y ? tail.Y - 1 : tail.Y + 1);
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

record struct Input(char Direction, int Moves);

record struct Point(int X, int Y);