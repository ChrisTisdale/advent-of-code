// Copyright (c) Christopher Tisdale 2024.
//
// Licensed under BSD-3-Clause.
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      https://spdx.org/licenses/BSD-3-Clause.html
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace AdventOfCode2022.day12;

using Common;

public class Day12 : Base2022AdventOfCodeDay<int>
{
    public override async ValueTask<int> ExecutePart1(Stream fileName, CancellationToken token = default) =>
        await HandleFile(fileName, false, token);

    public override async ValueTask<int> ExecutePart2(Stream fileName, CancellationToken token = default) =>
        await HandleFile(fileName, true, token);

    private static Node<char, int> GetNode(char value, int row, int col)
    {
        return new Node<char, int>(
            value switch
            {
                'S' => 'a',
                'E' => 'z',
                _ => value
            },
            value is 'S',
            value is 'E',
            new Point<int>(row, col));
    }

    private static async ValueTask<Graph<char, int>> BuildGraph(Stream fileName, bool anyA, CancellationToken token)
    {
        var nodes = new List<Node<char, int>>();
        var edges = new Dictionary<Node<char, int>, IReadOnlyList<Node<char, int>>>();
        var lines = await ReadAllLinesAsync(fileName, token);
        for (var row = 0; row < lines.Count; ++row)
        {
            for (var col = 0; col < lines[row].Length; ++col)
            {
                var value = lines[row][col];
                var node = GetNode(value, row, col);
                nodes.Add(node);

                var localEdges = new List<Node<char, int>>();
                if (row is not 0)
                {
                    localEdges.Add(GetNode(lines[row - 1][col], row - 1, col));
                }

                if (row < lines.Count - 1)
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

        return new Graph<char, int>(nodes, edges, anyA);
    }

    private static long GetCost(Node<char, int> item1, Node<char, int> item2)
    {
        var item1Value = item1.Data - 'a' + 1;
        var item2Value = item2.Data - 'a' + 1;
        return item2Value - item1Value;
    }

    private static ValueTask<int> FindPath(Graph<char, int> graph, Node<char, int> start)
    {
        var edges = graph.Edges[start];
        var priorityQueue =
            new PriorityQueue<Node<char, int>, int>(edges.Where(x => GetCost(start, x) <= 1).Select(x => (x, 1)));

        var visited = new Dictionary<Node<char, int>, int> { { start, 0 } };
        while (priorityQueue.TryDequeue(out var next, out var count))
        {
            if (next == graph.End)
            {
                return ValueTask.FromResult(count);
            }

            if (!visited.TryAdd(next, count))
            {
                continue;
            }

            edges = graph.Edges[next];
            priorityQueue.EnqueueRange(
                edges
                    .Where(x => GetCost(next, x) <= 1)
                    .Select(x => (x, 1 + count)));
        }

        return ValueTask.FromResult(0);
    }

    private static async Task<int> HandleFile(Stream file, bool anyA, CancellationToken token)
    {
        var result = await BuildGraph(file, anyA, token);
        var path = await FindMinPath(result);
        return path;
    }

    private static async ValueTask<int> FindMinPath(Graph<char, int> graph)
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
