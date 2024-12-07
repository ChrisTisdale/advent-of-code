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

namespace AdventOfCode2022.day14;

using Common;

public class Day14 : Base2022AdventOfCodeDay<int>
{
    private static readonly Point<int> SandDrop = new(500, 0);

    public override async ValueTask<int> ExecutePart1(Stream fileName, CancellationToken token = default) =>
        await HandleSandDrop(fileName, token: token);

    public override async ValueTask<int> ExecutePart2(Stream fileName, CancellationToken token = default) =>
        await HandleSandDrop(fileName, true, token);

    private static async Task<int> HandleSandDrop(Stream file, bool stopAtTop = false, CancellationToken token = default)
    {
        var (result, maxY) = await ParseFile(file, stopAtTop, token);
        var set = new HashSet<Point<int>>();
        while (true)
        {
            if (stopAtTop && set.Contains(SandDrop))
            {
                break;
            }

            var dropPoint = FindDropPoint(SandDrop, result, set, maxY);
            if (dropPoint is null)
            {
                break;
            }

            set.Add(dropPoint.Value);
        }

        return set.Count;
    }

    private static Point<int>? FindDropPoint(
        Point<int> start,
        IReadOnlyList<RockFormation> result,
        IReadOnlySet<Point<int>> sand,
        int maxY)
    {
        for (var i = start.Y; i <= maxY; i++)
        {
            var updated = new Point<int>(start.X, i);
            if (!sand.Contains(updated) && !ResultsContain(result, updated))
            {
                continue;
            }

            var left = updated with { X = updated.X - 1 };
            var right = updated with { X = updated.X + 1 };
            if (!sand.Contains(left) && !ResultsContain(result, left))
            {
                return FindDropPoint(left, result, sand, maxY);
            }

            if (!sand.Contains(right) && !ResultsContain(result, right))
            {
                return FindDropPoint(right, result, sand, maxY);
            }

            return new Point<int>(start.X, i - 1);
        }

        return null;
    }

    private static bool ResultsContain(IReadOnlyList<RockFormation> formations, Point<int> point)
    {
        for (var i = 0; i < formations.Count; ++i)
        {
            var rockFormation = formations[i];
            if (rockFormation.IsInFormation(point))
            {
                return true;
            }
        }

        return false;
    }

    private static async ValueTask<(IReadOnlyList<RockFormation>, int)> ParseFile(
        Stream file,
        bool hasFloor = false,
        CancellationToken token = default)
    {
        var formations = new List<RockFormation>();
        var maxY = 0;
        using var sr = new StreamReader(file);
        while (!sr.EndOfStream)
        {
            var line = await sr.ReadLineAsync(token);
            if (line == null)
            {
                continue;
            }

            var (rockFormation, y) = ParseLine(line);
            if (y > maxY)
            {
                maxY = y;
            }

            formations.Add(rockFormation);
        }

        if (hasFloor)
        {
            maxY += 2;
            var line = new Line<int>(new Point<int>(int.MinValue, maxY), new Point<int>(int.MaxValue, maxY));
            formations.Add(new RockFormation([line]));
        }

        return (formations, maxY);
    }

    private static (RockFormation, int) ParseLine(string line)
    {
        var maxY = 0;
        var lines = new List<Line<int>>();
        var points = line.Split(" -> ");
        for (var i = 1; i < points.Length; ++i)
        {
            var p1 = ParsePoint(points[i - 1]);
            var p2 = ParsePoint(points[i]);
            if (p1.Y > maxY)
            {
                maxY = p1.Y;
            }

            if (p2.Y > maxY)
            {
                maxY = p2.Y;
            }

            lines.Add(new Line<int>(p1, p2));
        }

        return (new RockFormation(lines), maxY);
    }

    private static Point<int> ParsePoint(string point)
    {
        var indexes = point.Split(',');
        return new Point<int>(int.Parse(indexes[0]), int.Parse(indexes[1]));
    }
}
