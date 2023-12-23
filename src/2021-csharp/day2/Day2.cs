namespace AdventOfCode2021.day2;

public class Day2 : Base2021AdventOfCodeDay<int>
{
    public override async ValueTask<int> ExecutePart1(Stream fileName)
    {
        var operations = await ReadDiveOperations(fileName);
        var result = await ProcessPart1(operations);
        return result;
    }

    public override async ValueTask<int> ExecutePart2(Stream fileName)
    {
        var operations = await ReadDiveOperations(fileName);
        var result = await ProcessPart2(operations);
        return result;
    }

    private static async ValueTask<IReadOnlyCollection<Dive>> ReadDiveOperations(Stream stream) =>
        await EnumerateLinesAsync(stream)
            .Select(x => x.Split(' '))
            .Select(x => new Dive(ToDirection(x[0]), int.Parse(x[1])))
            .ToArrayAsync()
            .ConfigureAwait(false);

    private static ValueTask<int> ProcessPart1(IEnumerable<Dive> operations)
    {
        var position = 0;
        var depth = 0;
        foreach (var d in operations)
        {
            switch (d.Direction)
            {
                case Direction.Forward:
                    position += d.Units;
                    break;
                case Direction.Up:
                    depth -= d.Units;
                    break;
                case Direction.Down:
                    depth += d.Units;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return new ValueTask<int>(position * depth);
    }

    private static ValueTask<int> ProcessPart2(IEnumerable<Dive> operations)
    {
        var position = 0;
        var aim = 0;
        var depth = 0;
        foreach (var d in operations)
        {
            switch (d.Direction)
            {
                case Direction.Forward:
                    position += d.Units;
                    depth += aim * d.Units;
                    break;
                case Direction.Up:
                    aim -= d.Units;
                    break;
                case Direction.Down:
                    aim += d.Units;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(operations), d.Direction, null);
            }
        }

        return new ValueTask<int>(position * depth);
    }

    private static Direction ToDirection(string dir) =>
        dir.ToLower() switch
        {
            "forward" => Direction.Forward,
            "up" => Direction.Up,
            _ => Direction.Down
        };
}
