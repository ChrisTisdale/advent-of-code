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

namespace AdventOfCode2023.day8;

using Common;

public class Day82023() : BaseAdventOfCodeDay<long>(Parts)
{
    private static readonly IReadOnlyDictionary<Part, string[]> Parts = new Dictionary<Part, string[]>
    {
        { Part.Part1, ["samplePart1.txt", "measurements.txt"] },
        { Part.Part2, ["samplePart2.txt", "measurements.txt"] }
    };

    public override DateOnly Year => new(2023, 12, 8);

    public override async ValueTask<long> ExecutePart1(Stream stream, CancellationToken token = default)
    {
        var tree = await ParseInput(stream, token);
        return CalculateSteps(tree, "AAA", false);
    }

    public override async ValueTask<long> ExecutePart2(Stream stream, CancellationToken token = default)
    {
        var tree = await ParseInput(stream, token);
        var steps = tree.Leaves
            .Where(x => x.Key.EndsWith('A'))
            .Select(x => x.Key)
            .Select(x => CalculateSteps(tree, x, true));
        return steps.Aggregate(1L, MathExtensions.LeastCommonMultiple);
    }

    private static long CalculateSteps(Tree tree, string key, bool isGhost)
    {
        var current = tree.Leaves[key];
        var endFound = false;
        var count = 0;
        while (!endFound)
        {
            foreach (var move in tree.Moves)
            {
                ++count;
                current = move == Move.Left ? tree.Leaves[current.Left] : tree.Leaves[current.Right];
                if (isGhost && !current.Key.EndsWith('Z') || !isGhost && !string.Equals("ZZZ", current.Key))
                {
                    continue;
                }

                endFound = true;
                break;
            }
        }

        return count;
    }

    private static async ValueTask<Tree> ParseInput(Stream stream, CancellationToken token)
    {
        var sr = new StreamReader(stream);
        var line = await sr.ReadLineAsync(token);
        var moves = line!.Select(x => x == 'R' ? Move.Right : Move.Left).ToArray();
        var leaves = new Dictionary<string, Leaf>();
        while (!sr.EndOfStream)
        {
            line = await sr.ReadLineAsync(token);
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var inputs = line.Split(" = ");
            var key = inputs[0].ToUpper();
            var edges = inputs[1].Replace("(", string.Empty).Replace(")", string.Empty).Split(", ");
            leaves.Add(key, new Leaf(key, edges[0].ToUpper(), edges[1].ToUpper()));
        }

        return new Tree(leaves, moves);
    }
}
