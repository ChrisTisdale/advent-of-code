namespace AdventOfCode2022.day6;

public class Day6 : Base2022AdventOfCodeDay<Results>
{
    public override async ValueTask<Results> ExecutePart1(Stream fileName, CancellationToken token = default) =>
        await FindMarkers(fileName, 4, token);

    public override async ValueTask<Results> ExecutePart2(Stream fileName, CancellationToken token = default) =>
        await FindMarkers(fileName, 14, token);

    private static async ValueTask<Results> FindMarkers(Stream fileName, int distinctCount, CancellationToken token)
    {
        var readLines = await EnumerateLinesAsync(fileName, token).Where(x => !string.IsNullOrEmpty(x)).ToArrayAsync(token);
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
