namespace AdventOfCode2022.day16;

using System.Text;

public class Day16 : Base2022<long>
{
    public override async ValueTask<long> ExecutePart1(string fileName)
    {
        var (nodes, start) = await ParseNodes(fileName);
        if (!fileName.Contains("sample"))
        {
            return nodes.Count;
        }

        foreach (var node in nodes)
        {
            Console.WriteLine(node.Value);
        }

        return nodes.Count;
    }

    public override async ValueTask<long> ExecutePart2(string fileName)
    {
        throw new NotImplementedException();
    }

    private static int FindBest(IReadOnlyDictionary<string, Node> nodes, string start)
    {
        var current = nodes[start];
        var flow = 0;
        var startingPoints = nodes.Select(x => (x.Value, FindNodeCost(current, x.Value, new HashSet<string>(), nodes)));
        var queue = new PriorityQueue<Node, int>(startingPoints);
        for (var i = 0; i < 30; ++i)
        {

        }

        return flow;
    }

    private static int FindNodeCost(
        Node start,
        Node destination,
        ISet<string> visited,
        IReadOnlyDictionary<string, Node> nodes)
    {
        visited.Add(start.Valve);
        if (start.Links.Contains(destination.Valve))
        {
            return 1;
        }

        var minCost = int.MaxValue;
        foreach (var link in start.Links)
        {
            var cost = 1 + FindNodeCost(nodes[link], destination, new HashSet<string>(visited), nodes);
            if (cost < minCost)
            {
                minCost = cost;
            }
        }

        return minCost;
    }

    private static async ValueTask<(IReadOnlyDictionary<string, Node>, string)> ParseNodes(string fileName)
    {
        var nodes = new Dictionary<string, Node>();
        string? start = null;
        await foreach (var line in File.ReadLinesAsync(fileName))
        {
            var valve = line.Substring(6, 2);
            start ??= valve;
            var values = line.Split(';', '=');
            var rate = int.Parse(values[1]);
            var associates = values[2]
                .Replace(" ", string.Empty)
                .Split(',')
                .Select(x => x.Substring(x.Length - 2, 2))
                .ToHashSet();
            nodes.Add(valve, new Node(valve, rate, associates));
        }

        return (nodes, start!);
    }
}

public record Node(string Valve, int Rate, IReadOnlySet<string> Links)
{
    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append(nameof(Valve)).Append(" = ").Append(Valve);
        builder.Append(", ").Append(nameof(Rate)).Append(" = ").Append(Rate);
        builder.Append(", ")
            .Append(nameof(Links))
            .Append(" = ")
            .Append(" [ ")
            .Append(string.Join(", ", Links))
            .Append(" ]");
        return true;
    }
}
