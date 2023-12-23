namespace AdventOfCode2022.day6;

public class Day6 : Base2022AdventOfCodeDay<Results>
{
    public override async ValueTask<Results> ExecutePart1(Stream fileName)
    {
        var result = await FindMarkers(fileName, 4);
        return result;
    }

    public override async ValueTask<Results> ExecutePart2(Stream fileName)
    {
        var result = await FindMarkers(fileName, 14);
        return result;
    }

    private static async ValueTask<Results> FindMarkers(Stream fileName, int distinctCount)
    {
        var readLines = await EnumerateLinesAsync(fileName).Where(x => !string.IsNullOrEmpty(x)).ToArrayAsync();
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
}
