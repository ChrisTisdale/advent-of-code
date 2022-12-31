namespace AdventOfCode2022.day3;

using System.Text;

public class Day3 : Base2022AdventOfCodeDay<decimal>
{
    public override async ValueTask<decimal> ExecutePart1(string fileName)
    {
        var result = await FindScorePart1(fileName);
        return result;
    }

    public override async ValueTask<decimal> ExecutePart2(string fileName)
    {
        var result = await FindScorePart2(fileName);
        return result;
    }

    private static ValueTask<decimal> FindScorePart1(string filename)
    {
        var score = 0m;
        var readLines = File.ReadLines(filename).ToArray();
        for (var i = 0; i < readLines.Length; ++i)
        {
            var sack = Encoding.ASCII.GetBytes(readLines[i]);
            var half = sack.Length / 2;
            var first = sack[..half];
            var second = sack[half..];
            score += FindCommon(first, second).Select(GetPriority).Sum();
        }

        return new ValueTask<decimal>(score);
    }

    private static ValueTask<decimal> FindScorePart2(string filename)
    {
        var score = 0m;
        var readLines = File.ReadLines(filename).ToArray();
        var i = 0;
        for (; i <= readLines.Length - 3; i += 3)
        {
            var first = Encoding.ASCII.GetBytes(readLines[i]);
            var second = Encoding.ASCII.GetBytes(readLines[i + 1]);
            var third = Encoding.ASCII.GetBytes(readLines[i + 2]);
            var common = FindCommon(first, second);
            score += FindCommon(common, third).Select(GetPriority).Sum();
        }

        return new ValueTask<decimal>(score);
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
