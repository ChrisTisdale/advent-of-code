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

using System.Numerics;
using System.Text;

internal record Graph<TValue, TPoint> where TPoint : INumber<TPoint>
{
    public Graph(
        IReadOnlyCollection<Node<TValue, TPoint>> nodes,
        IReadOnlyDictionary<Node<TValue, TPoint>, IReadOnlyList<Node<TValue, TPoint>>> edges,
        bool anyAStarts)
    {
        Nodes = nodes;
        Edges = edges;

        Start = nodes.First(n => n.Start);
        End = nodes.First(n => n.End);
        PossibleStarts = anyAStarts ? nodes.Where(n => Equals(n.Data, Start.Data)).ToList() : new[] { Start };
    }

    public Node<TValue, TPoint> Start { get; }

    public IReadOnlyList<Node<TValue, TPoint>> PossibleStarts { get; }

    public Node<TValue, TPoint> End { get; }

    public IReadOnlyCollection<Node<TValue, TPoint>> Nodes { get; }

    public IReadOnlyDictionary<Node<TValue, TPoint>, IReadOnlyList<Node<TValue, TPoint>>> Edges { get; }

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

    private static long GetCost(Node<TValue, TPoint> node, Node<TValue, TPoint> node1)
    {
        var item1Value = (node.Data is char data ? data : '\0') - 'a' + 1;
        var item2Value = (node1.Data is char node1Data ? node1Data : '\0') - 'a' + 1;
        return Math.Abs(item1Value - item2Value);
    }
}
