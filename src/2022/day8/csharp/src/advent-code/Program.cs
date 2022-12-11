var result = await BuildGraph("sample.txt");
var visibleTrees = await CalculateVisibleTrees(result);
var bestScore = await CalculateBetScenicScore(result);
Console.WriteLine($"Sample Answer: {visibleTrees}, Best Score: {bestScore}");

result = await BuildGraph("measurements.txt");
visibleTrees = await CalculateVisibleTrees(result);
bestScore = await CalculateBetScenicScore(result);
Console.WriteLine($"Measure Answer: {visibleTrees}, Best Score: {bestScore}");

async ValueTask<Graph> BuildGraph(string fileName)
{
    var lines = await File.ReadAllLinesAsync(fileName);
    var nodes = new List<TreeNode>();
    var edgeNodes = new Dictionary<TreeNode, IReadOnlyDictionary<Direction, EdgeNode>>();
    for (var row = 0; row < lines.Length; ++row)
    {
        var rowItem = lines[row];
        for (var col = 0; col < rowItem.Length; ++col)
        {
            var value = int.Parse(rowItem.AsSpan(col, 1));
            var treeNode = new TreeNode(value, col, row);
            nodes.Add(treeNode);
            edgeNodes.Add(treeNode, await GetEdgeNodes(lines, row, col, in treeNode));
        }
    }

    return new Graph(nodes, edgeNodes);
}

ValueTask<IReadOnlyDictionary<Direction, EdgeNode>> GetEdgeNodes(
    IReadOnlyList<string> lines,
    int row,
    int col,
    in TreeNode cur)
{
    var edges = new Dictionary<Direction, EdgeNode>();
    if (row + 1 < lines.Count)
    {
        var value = int.Parse(lines[row + 1].AsSpan(col, 1));
        edges.Add(Direction.Bottom, new EdgeNode(cur, new TreeNode(value, col, row + 1), Direction.Bottom));
    }

    if (row > 0)
    {
        var value = int.Parse(lines[row - 1].AsSpan(col, 1));
        edges.Add(Direction.Top, new EdgeNode(cur, new TreeNode(value, col, row - 1), Direction.Top));
    }

    if (col + 1 < lines[row].Length)
    {
        var value = int.Parse(lines[row].AsSpan(col + 1, 1));
        edges.Add(Direction.Right, new EdgeNode(cur, new TreeNode(value, col + 1, row), Direction.Right));
    }

    if (col > 0)
    {
        var value = int.Parse(lines[row].AsSpan(col - 1, 1));
        edges.Add(Direction.Left, new EdgeNode(cur, new TreeNode(value, col - 1, row), Direction.Left));
    }

    return new ValueTask<IReadOnlyDictionary<Direction, EdgeNode>>(edges);
}

async ValueTask<int> CalculateVisibleTrees(Graph graph)
{
    var count = 0;
    foreach (var node in graph.Nodes)
    {
        if (await IsNodeVisible(node, graph))
        {
            count++;
        }
    }

    return count;
}

async ValueTask<int> CalculateBetScenicScore(Graph graph)
{
    var maxValue = 0;
    foreach (var node in graph.Nodes)
    {
        maxValue = Math.Max(maxValue, await GetScenicScore(node, graph));
    }

    return maxValue;
}

async ValueTask<bool> IsNodeVisible(TreeNode node, Graph graph)
{
    var edges = graph.EdgeNodes[node];
    if (edges.Count != 4)
    {
        return true;
    }
    
    foreach (var (_, edge) in edges)
    {
        var isVisible = await IsVisibleForDirection(graph, node, node.Height, edge);
        if (isVisible)
        {
            return true;
        }
    }

    return false;
}

async ValueTask<int> GetScenicScore(TreeNode node, Graph graph)
{
    var edges = graph.EdgeNodes[node];
    var score = 1;
    
    foreach (var dir in Enum.GetValues<Direction>())
    {
        if (!edges.TryGetValue(dir, out var edge))
        {
            return 0;
        }

        score *= await GetScenicScoreForDirection(graph, node, node.Height, edge);
    }

    return score;
}

async ValueTask<bool> IsVisibleForDirection(Graph graph, TreeNode node, int height, EdgeNode edge)
{
    var edgeNodes = graph.EdgeNodes[node];
    if (!edgeNodes.ContainsKey(edge.Direction))
    {
        return true;
    }

    var direction = edgeNodes[edge.Direction];
    if (direction.End.Height >= height)
    {
        return false;
    }

    return await IsVisibleForDirection(graph, direction.End, height, direction);
}

async ValueTask<int> GetScenicScoreForDirection(Graph graph, TreeNode node, int height, EdgeNode edge)
{
    var edgeNodes = graph.EdgeNodes[node];
    if (!edgeNodes.ContainsKey(edge.Direction))
    {
        return 0;
    }

    var direction = edgeNodes[edge.Direction];
    if (direction.End.Height >= height)
    {
        return 1;
    }

    return 1 + await GetScenicScoreForDirection(graph, direction.End, height, direction);
}

public class Graph
{
    public IReadOnlyList<TreeNode> Nodes { get; }
    
    public IReadOnlyDictionary<TreeNode, IReadOnlyDictionary<Direction, EdgeNode>> EdgeNodes { get; }

    public Graph(
        IReadOnlyList<TreeNode> nodes,
        IReadOnlyDictionary<TreeNode, IReadOnlyDictionary<Direction, EdgeNode>> edgeNodes)
    {
        Nodes = nodes;
        EdgeNodes = edgeNodes;
    }
}

public record struct EdgeNode(TreeNode Start, TreeNode End, Direction Direction);

public enum Direction
{
    Top,
    Bottom,
    Left,
    Right
}

public record struct TreeNode(int Height, int ColumPosition, int RowPosition);