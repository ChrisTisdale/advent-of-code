namespace AdventOfCode2022.day4;

public class Day4 : Base2022
{
    public override ValueTask ExecutePart1()
    {
        return ExecutePart2();
    }

    public override async ValueTask ExecutePart2()
    {
        var result = await FindSubsets(GetFileLocation("sample.txt"));
        Console.WriteLine($"Sample Found subsets: {result}");

        result = await FindSubsets(GetFileLocation("measurements.txt"));
        Console.WriteLine($"Measure Found subset: {result}");
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
