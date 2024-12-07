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

namespace AdventOfCode2022.day5;

public class Day5 : Base2022AdventOfCodeDay<string>
{
    public override async ValueTask<string> ExecutePart1(Stream fileName, CancellationToken token = default)
    {
        var result = await GetStacks(fileName, false, token);
        return string.Join("", result.Select(x => x.Pop()));
    }

    public override async ValueTask<string> ExecutePart2(Stream fileName, CancellationToken token = default)
    {
        var result = await GetStacks(fileName, true, token);
        return string.Join("", result.Select(x => x.Pop()));
    }

    private static async ValueTask<IEnumerable<Stack<char>>> GetStacks(Stream fileName, bool moveMultiple, CancellationToken token)
    {
        var readLines = (await ReadAllLinesAsync(fileName, token)).ToArray();
        var index = Array.FindIndex(readLines, string.IsNullOrEmpty);
        var lines = readLines[index - 1].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(int.Parse).ToList();
        var count = lines.Max();
        var sources = Enumerable.Range(0, count).Select(_ => new Stack<char>()).ToArray();
        ReadFile(index, sources, readLines);
        ProcessMoves(moveMultiple, index, readLines, sources);
        return sources;
    }

    private static void ReadFile(int index, IReadOnlyList<Stack<char>> sources, IReadOnlyList<string> readLines)
    {
        for (var i = index - 2; i >= 0; --i)
        {
            for (var j = 0; j < sources.Count; ++j)
            {
                var charLoc = readLines[index - 1].IndexOf((j + 1).ToString(), StringComparison.Ordinal);
                var character = readLines[i][charLoc];
                if (character == ' ')
                {
                    continue;
                }

                sources[j].Push(character);
            }
        }
    }

    private static void ProcessMoves(bool moveMultiple, int index, IReadOnlyList<string> readLines, IReadOnlyList<Stack<char>> sources)
    {
        for (var i = index + 1; i < readLines.Count; ++i)
        {
            var ints = readLines[i].Split(' ').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToArray();
            var move = ints[0];
            var from = ints[1];
            var to = ints[2];
            if (moveMultiple)
            {
                var data = new char[move];
                for (var j = move - 1; j >= 0; --j)
                {
                    data[j] = sources[from - 1].Pop();
                }

                foreach (var c in data)
                {
                    sources[to - 1].Push(c);
                }
            }
            else
            {
                for (var j = move - 1; j >= 0; --j)
                {
                    sources[to - 1].Push(sources[from - 1].Pop());
                }
            }
        }
    }
}
