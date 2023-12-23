namespace AdventOfCode2021.day5;

using Common;

public sealed class Day5 : Base2021AdventOfCodeDay<int>
{
    public override async ValueTask<int> ExecutePart1(Stream fileName, CancellationToken token = default)
    {
        var lines = await ParseLines(fileName, token);
        return CountDuplicates(lines, x => x.One.Y == x.Two.Y || x.One.X == x.Two.X);
    }

    public override async ValueTask<int> ExecutePart2(Stream fileName, CancellationToken token = default)
    {
        var lines = await ParseLines(fileName, token);
        return CountDuplicates(
            lines,
            x => x.One.Y == x.Two.Y || x.One.X == x.Two.X || Math.Abs(x.One.X - x.Two.X) == Math.Abs(x.Two.Y - x.One.Y));
    }

    private static int CountDuplicates(IEnumerable<Line<int>> lines, Func<Line<int>, bool> filter)
    {
        var dic = new Dictionary<Point<int>, int>();
        foreach (var point in lines.Where(filter).SelectMany(x => x.GetPoints()))
        {
            if (dic.TryGetValue(point, out var value))
            {
                dic[point] = value + 1;
            }
            else
            {
                dic.Add(point, 1);
            }
        }

        return dic.Values.Count(x => x > 1);
    }

    private static async ValueTask<IReadOnlyList<Line<int>>> ParseLines(Stream file, CancellationToken token) =>
        await EnumerateLinesAsync(file, token)
            .Select(x => x.Split(" -> "))
            .Select(points => new Line<int>(ParsePoint(points[0]), ParsePoint(points[1])))
            .ToListAsync(token)
            .ConfigureAwait(false);

    private static Point<int> ParsePoint(string input)
    {
        var values = input.Split(',');
        return new Point<int>(int.Parse(values[0]), int.Parse(values[1]));
    }
}
