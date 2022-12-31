namespace AdventOfCode2022.day4;

public class Day4 : Base2022AdventOfCodeDay<int>
{
    public override async ValueTask<int> ExecutePart1(string fileName)
    {
        var result = await FindSubsets(fileName, false);
        return result;
    }

    public override async ValueTask<int> ExecutePart2(string fileName)
    {
        var result = await FindSubsets(fileName, true);
        return result;
    }

    private static async ValueTask<int> FindSubsets(string filename, bool anyOverlap)
    {
        var count = 0;
        await foreach (var line in File.ReadLinesAsync(filename))
        {
            var split = line.Split(',', '-').Select(int.Parse).ToArray();
            if (split.Length != 4)
            {
                continue;
            }

            var range1 = Enumerable.Range(split[0], split[1] - split[0] + 1).ToArray();
            var range2 = Enumerable.Range(split[2], split[3] - split[2] + 1).ToArray();
            if (anyOverlap ? AnyOverlap(range1, range2) : AllOverlap(range1, range2))
            {
                count++;
            }
        }

        return count;
    }

    private static bool AnyOverlap(int[] range1, int[] range2) =>
        range1.Any(range2.Contains);

    private static bool AllOverlap(int[] range1, int[] range2) =>
        range1.All(range2.Contains) || range2.All(range1.Contains);
}
