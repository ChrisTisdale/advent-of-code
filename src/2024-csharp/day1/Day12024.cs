namespace AdventOfCode2024.day1;

public class Day1 : Base2024AdventOfCodeDay<long>
{
    public override async ValueTask<long> ExecutePart1(Stream stream, CancellationToken token = default)
    {
        var (left, right) = await GetLists(stream, token);

        left.Sort();
        right.Sort();
        long sum = 0;
        for (var i = 0; i < left.Count; ++i)
        {
            sum += Math.Abs(right[i] - left[i]);
        }

        return sum;
    }

    public override async ValueTask<long> ExecutePart2(Stream stream, CancellationToken token = default)
    {
        var (left, right) = await GetLists(stream, token);
        long sum = 0;
        for (var i = 0; i < left.Count; ++i)
        {
            var leftValue = left[i];
            var count = 0;
            for (var j = 0; j < right.Count; ++j)
            {
                if (leftValue == right[j])
                {
                    ++count;
                }
            }

            sum += count * leftValue;
        }

        return sum;
    }

    private async ValueTask<(List<long> left, List<long> right)> GetLists(Stream stream, CancellationToken token)
    {
        List<long> left = [];
        List<long> right = [];
        await foreach (var line in EnumerateLinesAsync(stream, token))
        {
            var split = line.IndexOf(' ');
            if (split == -1)
            {
                continue;
            }

            var leftString = line.AsSpan(0, split);
            var rightString = line.AsSpan(split+1);
            var a = long.Parse(leftString);
            var b = long.Parse(rightString);
            left.Add(a);
            right.Add(b);
        }

        return (left, right);
    }
}
