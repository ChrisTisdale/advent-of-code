var result = await FindBest("sample.txt");
Console.WriteLine($"Sample Found max: {result}");

result = await FindBest("measurements.txt");
Console.WriteLine($"Result Found max: {result}");

async ValueTask<decimal> FindBest(string filename)
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

    return values.OrderDescending().Take(3).Sum();
}
