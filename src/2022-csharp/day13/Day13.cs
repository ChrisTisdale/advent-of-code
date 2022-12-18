namespace AdventOfCode2022.day13;

using System.Text.RegularExpressions;
using Common;

public class Day13 : Base2022<int>
{
    private static readonly Regex Regex = new Regex($"({Regex.Escape(",")}|{Regex.Escape("[")}|{Regex.Escape("]")})");

    public override ValueTask<int> ExecutePart1(string fileName)
    {
        // TODO add part 1
        return ExecutePart2(fileName);
    }

    public override async ValueTask<int> ExecutePart2(string fileName)
    {
        return await HandleFile(fileName);
    }

    private async Task<int> HandleFile(string file)
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

    private async ValueTask<IReadOnlyList<PacketChecker>> GetPackets(string file)
    {
        var lines = await File.ReadAllLinesAsync(file);
        var comparisions = new List<PacketChecker>();
        for (var i = 0; i < lines.Length; i += 3)
        {
            var left = ParseLine(lines[i]);
            var right = ParseLine(lines[i + 1]);
            var packetChecker = new PacketChecker(left, right);
            //Console.WriteLine(packetChecker);
            comparisions.Add(packetChecker);
        }

        return comparisions;
    }

    private ListPacket ParseLine(string line)
    {
        var values = Regex.Split(line).Where(x => !string.IsNullOrEmpty(x)).ToArray();
        var count = 1; // skip first is must always be [
        return ParseValues(values, ref count);
    }

    ListPacket ParseValues(IReadOnlyList<string> values, ref int i)
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

class PacketComparer : IComparer<IPacket>
{
    public int Compare(IPacket? x, IPacket? y)
    {
        return InCorrectOrder(x ?? throw new ArgumentNullException(nameof(x)),
            y ?? throw new ArgumentNullException(nameof(y)));
    }

    private static int InCorrectOrder(IPacket left, IPacket right)
    {
        switch (left)
        {
            case ValuePacket valLeft when right is ValuePacket valRight:
                return valLeft.Value.CompareTo(valRight.Value);
            case ListPacket listLeft when right is ListPacket listRight:
            {
                for (var i = 0; i < Math.Min(listLeft.Values.Count, listRight.Values.Count); ++i)
                {
                    var leftValue = listLeft.Values[i];
                    var rightValue = listRight.Values[i];
                    var res = InCorrectOrder(leftValue, rightValue);
                    if (res != 0)
                    {
                        return res;
                    }
                }

                return listLeft.Values.Count.CompareTo(listRight.Values.Count);
            }
            default:
                return left is ValuePacket
                    ? InCorrectOrder(new ListPacket(new[] { left }), right)
                    : InCorrectOrder(left, new ListPacket(new[] { right }));
        }
    }
}
