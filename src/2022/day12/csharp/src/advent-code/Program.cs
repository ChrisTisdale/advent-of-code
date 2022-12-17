using System.Text;

await HandleFile("sample.txt");
await HandleFile("measurements.txt");

async ValueTask HandleFile(string file)
{
    var result = await BuildGraph(file);
    var path = await FindMinPath(result);
    Console.WriteLine($"{file} Answer is: {path}");    
}

Node<char> GetNode(char value, int row, int col)
{
    return new Node<char>(value switch
    {
        'S' => 'a',
        'E' => 'z',
        _ => value
    }, value is 'S', value is 'E', new Point(row, col));
}

async ValueTask<Graph<char>> BuildGraph(string fileName)
{
    var nodes = new List<Node<char>>();
    var edges = new Dictionary<Node<char>, IReadOnlyList<Node<char>>>();
    var lines = await File.ReadAllLinesAsync(fileName);
    for (var row = 0; row < lines.Length; ++row)
    {
        for (var col = 0; col < lines[row].Length; ++col)
        {
            var value = lines[row][col];
            var node = GetNode(value, row, col);
            nodes.Add(node);

            var localEdges = new List<Node<char>>();
            if (row is not 0)
            {
                localEdges.Add(GetNode(lines[row - 1][col], row - 1, col));
            }

            if (row < lines.Length - 1)
            {
                localEdges.Add(GetNode(lines[row + 1][col], row + 1, col));
            }

            if (col is not 0)
            {
                localEdges.Add(GetNode(lines[row][col - 1], row, col - 1));
            }

            if (col < lines[row].Length - 1)
            {
                localEdges.Add(GetNode(lines[row][col + 1], row, col + 1));
            }

            edges[node] = localEdges;
        }
    }

    return new Graph<char>(nodes, edges);
}

long GetCost(Node<char> item1, Node<char> item2)
{
    var item1Value = item1.Data - 'a' + 1;
    var item2Value = item2.Data - 'a' + 1;
    return item2Value - item1Value;
}

async ValueTask<int> FindMinPath(Graph<char> graph)
{
    var minValue = int.MaxValue;
    foreach (var start in graph.PossibleStarts)
    {
        var res = await FindPath(graph, start);
        if (res is 0 || res >= minValue)
        {
            continue;
        }

        minValue = res;
    }

    return minValue;
}

ValueTask<int> FindPath(Graph<char> graph, Node<char> start)
{
    var edges = graph.Edges[start];
    var priorityQueue =
        new PriorityQueue<Node<char>, int>(edges.Where(x => GetCost(start, x) <= 1).Select(x => (x, 1)));
    var visited = new Dictionary<Node<char>, int> { { start, 0 } };
    while (priorityQueue.TryDequeue(out var next, out var count))
    {
        //Console.WriteLine($"Looking at node: {next}.  With cost: {data.Count}");
        if (next == graph.End)
        {
            return ValueTask.FromResult(count);
        }

        if (visited.ContainsKey(next))
        {
            continue;
        }
        
        visited.Add(next, count);
        edges = graph.Edges[next];
        priorityQueue.EnqueueRange(edges
            .Where(x => GetCost(next, x) <= 1).Select(x => (x, 1 + count)));
    }
    
    return ValueTask.FromResult(0);
}

// ReSharper disable once NotAccessedPositionalProperty.Global
record Node<T>(T Data, bool Start, bool End, Point Point);

// ReSharper disable twice NotAccessedPositionalProperty.Global
record struct Point(int Row, int Col);

record Graph<T>
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
        builder.AppendLine(string.Join(Environment.NewLine, Nodes.Select(x => $"{x}{Environment.NewLine}{string.Join(Environment.NewLine, Edges[x].Select(z => $"    {z}: Cost={GetCost(x, z)}"))}")));
        return true;
    }

    private long GetCost(Node<T> node, Node<T> node1)
    {
        var item1Value = (node.Data is char data ? data : '\0') - 'a' + 1;
        var item2Value = (node1.Data is char node1Data ? node1Data : '\0') - 'a' + 1;
        return Math.Abs(item1Value - item2Value);
    }
}

class Testing : IComparer<List<Node<char>>>
{
    public int Compare(List<Node<char>>? x, List<Node<char>>? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;
        return x.Count.CompareTo(y.Count);
    }
}