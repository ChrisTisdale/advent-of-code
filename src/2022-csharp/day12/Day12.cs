namespace AdventOfCode2022.day12;

using AdventOfCode.day12;
using Common;

public class Day12 : Base2022
{
    public override ValueTask ExecutePart1()
    {
        // TODO add part 1
        return ExecutePart2();
    }

    public override async ValueTask ExecutePart2()
    {
        await HandleFile(GetFileLocation("sample.txt"));
        await HandleFile(GetFileLocation("measurements.txt"));
    }
    
    private static Node<char> GetNode(char value, int row, int col)
    {
        return new Node<char>(
            value switch
            {
                'S' => 'a',
                'E' => 'z',
                _ => value
            },
            value is 'S',
            value is 'E',
            new Point(row, col));
    }
    
    private static async ValueTask<Graph<char>> BuildGraph(string fileName)
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
    
    private static long GetCost(Node<char> item1, Node<char> item2)
    {
        var item1Value = item1.Data - 'a' + 1;
        var item2Value = item2.Data - 'a' + 1;
        return item2Value - item1Value;
    }

    private static ValueTask<int> FindPath(Graph<char> graph, Node<char> start)
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
            priorityQueue.EnqueueRange(
                edges
                    .Where(x => GetCost(next, x) <= 1)
                    .Select(x => (x, 1 + count)));
        }

        return ValueTask.FromResult(0);
    }
    
    private static async ValueTask HandleFile(string file)
    {
        var result = await BuildGraph(file);
        var path = await FindMinPath(result);
        Console.WriteLine($"{file} Answer is: {path}");
    }

    private static async ValueTask<int> FindMinPath(Graph<char> graph)
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
}