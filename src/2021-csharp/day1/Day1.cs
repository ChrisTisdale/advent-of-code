namespace AdventOfCode2021.day1;

public class Day1 : Base2021AdventOfCodeDay<int>
{
    public override async ValueTask<int> ExecutePart1(Stream fileName, CancellationToken token = default)
    {
        var result = await FindIncreasingCount(fileName, 1, token);
        return result;
    }

    public override async ValueTask<int> ExecutePart2(Stream fileName, CancellationToken token = default)
    {
        var result = await FindIncreasingCount(fileName, 3, token);
        return result;
    }

    private static async ValueTask<int> FindIncreasingCount(Stream stream, int windowSize, CancellationToken token)
    {
        var lines = await EnumerateLinesAsync(stream, token).Select(int.Parse).ToArrayAsync(token);
        var count = 0;
        for (var i = 0; i < lines.Length - windowSize; ++i)
        {
            var startWindow = lines[i..(i + windowSize)];
            var endWindow = lines[(i + 1)..(i + windowSize + 1)];
            if (startWindow.Sum() < endWindow.Sum())
            {
                ++count;
            }
        }

        return count;
    }
}
