namespace AdventOfCode2021.day6;

public class Day6 : Base2021AdventOfCodeDay<long>
{
    public override async ValueTask<long> ExecutePart1(Stream stream)
    {
        var fish = await ReadFish(stream);
        return DetermineFishSize(fish, 80);
    }

    public override async ValueTask<long> ExecutePart2(Stream stream)
    {
        var fish = await ReadFish(stream);
        return DetermineFishSize(fish, 256);
    }

    private static long DetermineFishSize(IReadOnlyCollection<long> fish, int length)
    {
        long count = fish.Count;
        var set = fish.GroupBy(x => x).ToDictionary(x => x.Key, x => (long)x.Count());
        for (var i = 0; i < length; ++i)
        {
            var valuePairs = set.ToArray();
            set.Clear();
            var addCount = GetNewFish(valuePairs, set);
            if (addCount <= 0)
            {
                continue;
            }

            count += addCount;
            set.Add(8, addCount);
        }

        return count;
    }

    private static long GetNewFish(IEnumerable<KeyValuePair<long, long>> valuePairs, IDictionary<long, long> set)
    {
        var addCount = 0L;
        foreach (var j in valuePairs)
        {
            var update = j.Key - 1;
            if (j.Key == 0)
            {
                addCount += j.Value;
                update = 6;
            }

            if (set.ContainsKey(update))
            {
                set[update] += j.Value;
            }
            else
            {
                set.Add(update, j.Value);
            }
        }

        return addCount;
    }

    private static async ValueTask<IReadOnlyList<long>> ReadFish(Stream stream)
    {
        var input = await ReadFile(stream);
        return input.Count < 1 ? new List<long>() : input[0].Split(',').Select(long.Parse).ToList();
    }
}
