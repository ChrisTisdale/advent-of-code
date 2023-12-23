namespace AdventOfCode2022.day3;

using System.Text;

public class Day3 : Base2022AdventOfCodeDay<decimal>
{
    public override async ValueTask<decimal> ExecutePart1(Stream fileName)
    {
        var result = await FindScorePart1(fileName);
        return result;
    }

    public override async ValueTask<decimal> ExecutePart2(Stream fileName)
    {
        var result = await FindScorePart2(fileName);
        return result;
    }

    private static async ValueTask<decimal> FindScorePart1(Stream filename)
    {
        var score = 0m;
        await foreach (var line in EnumerateLinesAsync(filename))
        {
            var sack = Encoding.ASCII.GetBytes(line);
            var half = sack.Length / 2;
            var first = sack[..half];
            var second = sack[half..];
            score += FindCommon(first, second).Select(GetPriority).Sum();
        }

        return score;
    }

    private static async ValueTask<decimal> FindScorePart2(Stream filename)
    {
        var score = 0m;
        var readLines = await ReadAllLinesAsync(filename);
        for (var i = 0; i <= readLines.Count - 3; i += 3)
        {
            var first = Encoding.ASCII.GetBytes(readLines[i]);
            var second = Encoding.ASCII.GetBytes(readLines[i + 1]);
            var third = Encoding.ASCII.GetBytes(readLines[i + 2]);
            var common = FindCommon(first, second);
            score += FindCommon(common, third).Select(GetPriority).Sum();
        }

        return score;
    }

    private static IEnumerable<byte> FindCommon(IEnumerable<byte> first, IEnumerable<byte> second)
    {
        var enumerable = second as byte[] ?? second.ToArray();
        foreach (var value in first.Distinct())
        {
            if (!enumerable.Contains(value))
            {
                continue;
            }

            yield return value;
        }
    }

    private static int GetPriority(byte c) => c > 90 ? c - 96 : c - 38;
}
