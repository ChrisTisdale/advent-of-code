namespace AdventOfCode2022.day5;

public class Day5 : Base2022AdventOfCodeDay<string>
{
    public override async ValueTask<string> ExecutePart1(Stream fileName)
    {
        var result = await GetStacks(fileName, false);
        return string.Join("", result.Select(x => x.Pop()));
    }

    public override async ValueTask<string> ExecutePart2(Stream fileName)
    {
        var result = await GetStacks(fileName, true);
        return string.Join("", result.Select(x => x.Pop()));
    }

    private static async ValueTask<IEnumerable<Stack<char>>> GetStacks(Stream fileName, bool moveMultiple)
    {
        var readLines = (await ReadFile(fileName)).ToArray();
        var index = Array.FindIndex(readLines, string.IsNullOrEmpty);
        var lines = readLines[index - 1].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(int.Parse).ToList();
        var count = lines.Max();
        var sources = Enumerable.Range(0, count).Select(_ => new Stack<char>()).ToArray();
        ReadFile(index, sources, readLines);
        ProcessMoves(moveMultiple, index, readLines, sources);
        return sources;
    }

    private static void ReadFile(int index, Stack<char>[] sources, string[] readLines)
    {
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
    }

    private static void ProcessMoves(bool moveMultiple, int index, string[] readLines, Stack<char>[] sources)
    {
        for (var i = index + 1; i < readLines.Length; ++i)
        {
            var ints = readLines[i].Split(' ').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToArray();
            var move = ints[0];
            var from = ints[1];
            var to = ints[2];
            if (moveMultiple)
            {
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
            else
            {
                for (var j = move - 1; j >= 0; --j)
                {
                    sources[to - 1].Push(sources[from - 1].Pop());
                }
            }
        }
    }
}
