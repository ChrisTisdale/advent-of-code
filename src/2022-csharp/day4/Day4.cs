namespace AdventOfCode2022.day4;

public class Day4 : Base2022<int>
{
    public override ValueTask<int> ExecutePart1(string fileName)
    {
        return ExecutePart2(fileName);
    }

    public override async ValueTask<int> ExecutePart2(string fileName)
    {
        var result = await FindSubsets(fileName);
        return result;
    }

    private static async ValueTask<int> FindSubsets(string filename)
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
            if (range1.Any(x => range2.Contains(x)))
            {
                count++;
            }
        }

        return count;
    }
}
