namespace AdventOfCode2022.day5;

public class Day5 : Base2022
{
    public override ValueTask ExecutePart1()
    {
        return ExecutePart2();
    }

    public override async ValueTask ExecutePart2()
    {
        var result = (await GetStacks(GetFileLocation("sample.txt"))).ToArray();
        for (var i = 0; i < result.Length; ++i)
        {
            Console.WriteLine($"Sample Found subsets: {i}-{string.Join(',', result[i])}");
        }

        Console.WriteLine($"$Sample Answer: {string.Join("", result.Select(x => x.Pop()))}");

        result = (await GetStacks(GetFileLocation("measurements.txt"))).ToArray();
        for (var i = 0; i < result.Length; ++i)
        {
            Console.WriteLine($"Measure Found subsets: {i}-{string.Join(',', result[i])}");
        }

        Console.WriteLine($"Measure Answer: {string.Join("", result.Select(x => x.Pop()))}");
    }

    private static ValueTask<IEnumerable<Stack<char>>> GetStacks(string fileName)
    {
        var readLines = File.ReadLines(fileName).ToArray();
        var index = Array.FindIndex(readLines, string.IsNullOrEmpty);
        var lines = readLines[index - 1].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(int.Parse).ToList();
        var count = lines.Max();
        var sources = Enumerable.Range(0, count).Select(_ => new Stack<char>()).ToArray();
        for (var i = index - 2; i >= 0; --i)
        {
            for (var j = 0; j < sources.Length; ++j)
            {
                var charLoc = readLines[index - 1].IndexOf((j + 1).ToString(), StringComparison.Ordinal);
                var character = readLines[i][charLoc];
                if (character == ' ')
                {
                    continue;
                }
            
                sources[j].Push(character);
            }
        }

        for (var i = index + 1; i < readLines.Length; ++i)
        {
            var ints = readLines[i].Split(' ').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToArray();
            var move = ints[0];
            var from = ints[1];
            var to = ints[2];
            var data = new char[move];
            for (var j = move - 1; j >= 0; --j)
            {
                data[j] = sources[from - 1].Pop();
            }
        
            foreach (var c in data)
            {
                sources[to - 1].Push(c);
            }
        }

        return new ValueTask<IEnumerable<Stack<char>>>(sources);
    }
}
