namespace AdventOfCode2022.day1;

public class Day1 : Base2022AdventOfCodeDay<decimal>
{
    public override async ValueTask<decimal> ExecutePart1(string fileName)
    {
        var result = await FindBest(fileName, 1);
        return result;
    }

    public override async ValueTask<decimal> ExecutePart2(string fileName)
    {
        var result = await FindBest(fileName, 3);
        return result;
    }

    private static async ValueTask<decimal> FindBest(string filename, int takeCount)
    {
        var cur = 0m;
        var values = new List<decimal>();
        await foreach (var readLine in File.ReadLinesAsync(filename))
        {
            if (string.IsNullOrWhiteSpace(readLine))
            {
                values.Add(cur);
                cur = 0;
            }
            else
            {
                cur += decimal.Parse(readLine);
            }
        }

        if (cur != 0)
        {
            values.Add(cur);
        }

        return values.OrderDescending().Take(takeCount).Sum();
    }
}
