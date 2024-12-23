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

namespace AdventOfCode2022.day9;

using System.Runtime.CompilerServices;
using Common;

public class Day9 : Base2022AdventOfCodeDay<int>
{
    private static readonly IReadOnlyDictionary<Part, string[]> Files = new Dictionary<Part, string[]>
    {
        { Part.Part1, ["samplePart1.txt", "measurements.txt"] },
        { Part.Part2, ["samplePart2.txt", "measurements.txt"] }
    }.AsReadOnly();

    public Day9()
        : base(Files)
    {
    }

    public override async ValueTask<int> ExecutePart1(Stream fileName, CancellationToken token = default) =>
        await GetUniqueSpaces(fileName, 0, token);

    public override async ValueTask<int> ExecutePart2(Stream fileName, CancellationToken token = default) =>
        await GetUniqueSpaces(fileName, 8, token);

    private static async IAsyncEnumerable<Input> ProcessFile(Stream fileName, [EnumeratorCancellation] CancellationToken token = default)
    {
        using var sr = new StreamReader(fileName);
        while (!sr.EndOfStream)
        {
            var line = await sr.ReadLineAsync(token);
            if (line == null)
            {
                continue;
            }

            var inputs = line.Split(' ');
            yield return new Input(inputs[0][0], int.Parse(inputs[1]));
        }
    }

    private static async ValueTask<int> GetUniqueSpaces(Stream fileName, int middleCount, CancellationToken token)
    {
        var middlePoints = new Point<int>[middleCount + 2];
        var set = new HashSet<Point<int>> { middlePoints.Last() };
        await foreach (var input in ProcessFile(fileName, token))
        {
            for (var i = 0; i < input.Moves; ++i)
            {
                middlePoints[0] = input.Direction switch
                {
                    'U' => middlePoints[0] with { Y = middlePoints[0].Y + 1 },
                    'D' => middlePoints[0] with { Y = middlePoints[0].Y - 1 },
                    'L' => middlePoints[0] with { X = middlePoints[0].X - 1 },
                    'R' => middlePoints[0] with { X = middlePoints[0].X + 1 },
                    _ => throw new ArgumentException(nameof(input.Direction))
                };

                for (var j = 1; j < middlePoints.Length; ++j)
                {
                    middlePoints[j] = GetTailLocation(middlePoints[j - 1], middlePoints[j]);
                }

                set.Add(middlePoints[^1]);
            }
        }

        return set.Count;
    }

    private static Point<int> GetTailLocation(Point<int> head, Point<int> tail)
    {
        var xNeedsUpdate = Math.Abs(tail.X - head.X) > 1;
        var yNeedsUpdate = Math.Abs(tail.Y - head.Y) > 1;
        if (xNeedsUpdate && tail.Y != head.Y || yNeedsUpdate && tail.X != head.X)
        {
            return new Point<int>(tail.X > head.X ? tail.X - 1 : tail.X + 1, tail.Y > head.Y ? tail.Y - 1 : tail.Y + 1);
        }

        if (xNeedsUpdate)
        {
            return tail with { X = tail.X > head.X ? tail.X - 1 : tail.X + 1 };
        }

        if (yNeedsUpdate)
        {
            return tail with { Y = tail.Y > head.Y ? tail.Y - 1 : tail.Y + 1 };
        }

        return tail;
    }
}
