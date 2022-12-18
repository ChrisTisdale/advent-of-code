namespace AdventOfCode2022.day7;

public class Day7 : Base2022<string>
{
    public override ValueTask<string> ExecutePart1(string fileName)
    {
        // TODO handle part 1
        return ExecutePart2(fileName);
    }

    public override async ValueTask<string> ExecutePart2(string fileName)
    {
        var result = (await GetStacks(fileName)).ToArray();
        /*for (var i = 0; i < result.Length; ++i)
        {
            Console.WriteLine($"{fileName} Found subsets: {i}-{string.Join(',', result[i])}");
        }*/

        return string.Join("", result.Select(x => x.Pop()));
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
