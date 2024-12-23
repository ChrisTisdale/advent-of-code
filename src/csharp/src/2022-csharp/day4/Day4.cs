﻿// Copyright (c) Christopher Tisdale 2024.
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

namespace AdventOfCode2022.day4;

public class Day4 : Base2022AdventOfCodeDay<int>
{
    public override async ValueTask<int> ExecutePart1(Stream fileName, CancellationToken token = default) =>
        await FindSubsets(fileName, false, token);

    public override async ValueTask<int> ExecutePart2(Stream fileName, CancellationToken token = default) =>
        await FindSubsets(fileName, true, token);

    private static async ValueTask<int> FindSubsets(Stream filename, bool anyOverlap, CancellationToken token)
    {
        var count = 0;
        using var sr = new StreamReader(filename);
        while (!sr.EndOfStream)
        {
            var line = await sr.ReadLineAsync(token);
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var split = line.Split(',', '-').Select(int.Parse).ToArray();
            if (split.Length != 4)
            {
                continue;
            }

            var range1 = Enumerable.Range(split[0], split[1] - split[0] + 1).ToArray();
            var range2 = Enumerable.Range(split[2], split[3] - split[2] + 1).ToArray();
            if (anyOverlap ? AnyOverlap(range1, range2) : AllOverlap(range1, range2))
            {
                count++;
            }
        }

        return count;
    }

    private static bool AnyOverlap(int[] range1, int[] range2) =>
        range1.Any(range2.Contains);

    private static bool AllOverlap(int[] range1, int[] range2) =>
        range1.All(range2.Contains) || range2.All(range1.Contains);
}
