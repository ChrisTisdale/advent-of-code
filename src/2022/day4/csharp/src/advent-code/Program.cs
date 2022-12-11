using System.Text;

var result = await FindSubsets("sample.txt");
Console.WriteLine($"Sample Found subsets: {result}");

result = await FindSubsets("measurements.txt");
Console.WriteLine($"Measure Found subset: {result}");

async ValueTask<int> FindSubsets(string filename)
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

