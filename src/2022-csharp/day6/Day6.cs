namespace AdventOfCode2022.day6;

public class Day6 : Base2022AdventOfCodeDay<Results>
{
    public override async ValueTask<Results> ExecutePart1(string fileName)
    {
        var result = await FindMarkers(fileName, 4);
        return result;
    }

    public override async ValueTask<Results> ExecutePart2(string fileName)
    {
        var result = await FindMarkers(fileName, 14);
        return result;
    }

    private static async ValueTask<Results> FindMarkers(string fileName, int distinctCount)
    {
        var readLines = (await File.ReadAllLinesAsync(fileName)).Where(x => !string.IsNullOrEmpty(x)).ToArray();
        var results = new int[readLines.Length];
        for (var index = 0; index < readLines.Length; index++)
        {
            var line = readLines[index];
            results[index] = GetDistinctCount(distinctCount, line);
        }

        return new Results(results);
    }

    private static int GetDistinctCount(int distinctCount, string line)
    {
        var count = 0;
        var set = new HashSet<char>();
        var queue = new Queue<char>();
        foreach (var c in line)
        {
            ++count;
            if (set.Contains(c))
            {
                char x;
                do
                {
                    x = queue.Dequeue();
                    set.Remove(x);
                } while (x != c);
            }

            set.Add(c);
            queue.Enqueue(c);
            if (set.Count == distinctCount)
            {
                break;
            }
        }

        return count;
    }

    private static async ValueTask<Results> FindSubsets(string filename, bool anyOverlap)
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

        return new Results(count);
    }

    private static bool AnyOverlap(int[] range1, int[] range2) =>
        range1.Any(range2.Contains);

    private static bool AllOverlap(int[] range1, int[] range2) =>
        range1.All(range2.Contains) || range2.All(range1.Contains);
}
