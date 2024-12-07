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

namespace AdventOfCode2022.day3;

using System.Text;

public class Day3 : Base2022AdventOfCodeDay<decimal>
{
    public override async ValueTask<decimal> ExecutePart1(Stream fileName, CancellationToken token = default) =>
        await FindScorePart1(fileName, token);

    public override async ValueTask<decimal> ExecutePart2(Stream fileName, CancellationToken token = default) =>
        await FindScorePart2(fileName, token);

    private static async ValueTask<decimal> FindScorePart1(Stream filename, CancellationToken token)
    {
        var score = 0m;
        await foreach (var line in EnumerateLinesAsync(filename, token))
        {
            var sack = Encoding.ASCII.GetBytes(line);
            var half = sack.Length / 2;
            var first = sack[..half];
            var second = sack[half..];
            score += FindCommon(first, second).Select(GetPriority).Sum();
        }

        return score;
    }

    private static async ValueTask<decimal> FindScorePart2(Stream filename, CancellationToken token)
    {
        var score = 0m;
        var readLines = await ReadAllLinesAsync(filename, token);
        for (var i = 0; i <= readLines.Count - 3; i += 3)
        {
            var first = Encoding.ASCII.GetBytes(readLines[i]);
            var second = Encoding.ASCII.GetBytes(readLines[i + 1]);
            var third = Encoding.ASCII.GetBytes(readLines[i + 2]);
            var common = FindCommon(first, second);
            score += FindCommon(common, third).Select(GetPriority).Sum();
        }

        return score;
    }

    private static IEnumerable<byte> FindCommon(IEnumerable<byte> first, IEnumerable<byte> second)
    {
        var enumerable = second as byte[] ?? second.ToArray();
        foreach (var value in first.Distinct())
        {
            if (!enumerable.Contains(value))
            {
                continue;
            }

            yield return value;
        }
    }

    private static int GetPriority(byte c) => c > 90 ? c - 96 : c - 38;
}
