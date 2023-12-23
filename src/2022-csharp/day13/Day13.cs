namespace AdventOfCode2022.day13;

using System.Text.RegularExpressions;

public class Day13 : Base2022AdventOfCodeDay<int>
{
    private static readonly Regex Regex = new($"({Regex.Escape(",")}|{Regex.Escape("[")}|{Regex.Escape("]")})");

    public override async ValueTask<int> ExecutePart1(Stream fileName) => await HandleFilePart1(fileName);

    public override async ValueTask<int> ExecutePart2(Stream fileName) => await HandleFile(fileName);

    private static async Task<int> HandleFile(Stream file)
    {
        var result = await GetPackets(file);
        var starter = new ListPacket(new[] { new ListPacket(new[] { new ValuePacket(2) }) });
        var ender = new ListPacket(new[] { new ListPacket(new[] { new ValuePacket(6) }) });
        var sortedList =
            new SortedSet<IPacket>(result.SelectMany(x => x).Append(starter).Append(ender), new PacketComparer());
        var count = 0;
        var startIndex = 0;
        var endIndex = 0;
        foreach (var p in sortedList)
        {
            ++count;
            if (Equals(p, starter))
            {
                startIndex = count;
            }

            if (Equals(p, ender))
            {
                endIndex = count;
            }
        }

        return startIndex * endIndex;
    }

    private static async Task<int> HandleFilePart1(Stream file)
    {
        var result = await GetPackets(file);
        var count = 0;
        var comparer = new PacketComparer();
        for (var i = 0; i < result.Count; ++i)
        {
            var checker = result[i];
            if (comparer.Compare(checker.Left, checker.Right) < 0)
            {
                count += i + 1;
            }
        }

        return count;
    }

    private static async ValueTask<IReadOnlyList<PacketChecker>> GetPackets(Stream file)
    {
        var lines = await ReadAllLinesAsync(file);
        var comparisons = new List<PacketChecker>();
        for (var i = 0; i < lines.Count; i += 3)
        {
            var left = ParseLine(lines[i]);
            var right = ParseLine(lines[i + 1]);
            var packetChecker = new PacketChecker(left, right);
            comparisons.Add(packetChecker);
        }

        return comparisons;
    }

    private static ListPacket ParseLine(string line)
    {
        var values = Regex.Split(line).Where(x => !string.IsNullOrEmpty(x)).ToArray();
        var count = 1; // skip first is must always be [
        return ParseValues(values, ref count);
    }

    private static ListPacket ParseValues(IReadOnlyList<string> values, ref int i)
    {
        var data = new List<IPacket>();
        for (; i < values.Count; ++i)
        {
            var value = values[i];
            switch (value)
            {
                case "[":
                    ++i;
                    data.Add(ParseValues(values, ref i));
                    break;
                case "]":
                    ++i;
                    return new ListPacket(data);
                case ",":
                    break;
                default:
                    data.Add(new ValuePacket(int.Parse(value)));
                    break;
            }
        }

        return new ListPacket(data);
    }
}
