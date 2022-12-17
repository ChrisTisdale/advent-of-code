namespace AdventOfCode2022.day12;

using System.Text;
using AdventOfCode.day12;

internal record Graph<T>
{
    public Graph(IReadOnlyCollection<Node<T>> nodes, IReadOnlyDictionary<Node<T>, IReadOnlyList<Node<T>>> edges)
    {
        Nodes = nodes;
        Edges = edges;

        Start = nodes.First(n => n.Start);
        End = nodes.First(n => n.End);
        PossibleStarts = nodes.Where(n => Equals(n.Data, Start.Data)).ToList();
    }

    public Node<T> Start { get; }

    public IReadOnlyList<Node<T>> PossibleStarts { get; }

    public Node<T> End { get; }

    public IReadOnlyCollection<Node<T>> Nodes { get; }

    public IReadOnlyDictionary<Node<T>, IReadOnlyList<Node<T>>> Edges { get; }

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append(Environment.NewLine);
        builder.AppendLine(
            string.Join(
                Environment.NewLine,
                Nodes.Select(
                    x =>
                        $"{x}{Environment.NewLine}{string.Join(Environment.NewLine, Edges[x].Select(z => $"    {z}: Cost={GetCost(x, z)}"))}")));

        return true;
    }

    private static long GetCost(Node<T> node, Node<T> node1)
    {
        var item1Value = (node.Data is char data ? data : '\0') - 'a' + 1;
        var item2Value = (node1.Data is char node1Data ? node1Data : '\0') - 'a' + 1;
        return Math.Abs(item1Value - item2Value);
    }
}
