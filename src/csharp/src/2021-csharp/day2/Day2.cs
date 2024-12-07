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

namespace AdventOfCode2021.day2;

public class Day2 : Base2021AdventOfCodeDay<int>
{
    public override async ValueTask<int> ExecutePart1(Stream fileName, CancellationToken token = default)
    {
        var operations = await ReadDiveOperations(fileName, token);
        var result = await ProcessPart1(operations);
        return result;
    }

    public override async ValueTask<int> ExecutePart2(Stream fileName, CancellationToken token = default)
    {
        var operations = await ReadDiveOperations(fileName, token);
        var result = await ProcessPart2(operations);
        return result;
    }

    private static async ValueTask<IReadOnlyCollection<Dive>> ReadDiveOperations(Stream stream, CancellationToken token) =>
        await EnumerateLinesAsync(stream, token)
            .Select(x => x.Split(' '))
            .Select(x => new Dive(ToDirection(x[0]), int.Parse(x[1])))
            .ToArrayAsync(token)
            .ConfigureAwait(false);

    private static ValueTask<int> ProcessPart1(IEnumerable<Dive> operations)
    {
        var position = 0;
        var depth = 0;
        foreach (var d in operations)
        {
            switch (d.Direction)
            {
                case Direction.Forward:
                    position += d.Units;
                    break;
                case Direction.Up:
                    depth -= d.Units;
                    break;
                case Direction.Down:
                    depth += d.Units;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return new ValueTask<int>(position * depth);
    }

    private static ValueTask<int> ProcessPart2(IEnumerable<Dive> operations)
    {
        var position = 0;
        var aim = 0;
        var depth = 0;
        foreach (var d in operations)
        {
            switch (d.Direction)
            {
                case Direction.Forward:
                    position += d.Units;
                    depth += aim * d.Units;
                    break;
                case Direction.Up:
                    aim -= d.Units;
                    break;
                case Direction.Down:
                    aim += d.Units;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(operations), d.Direction, null);
            }
        }

        return new ValueTask<int>(position * depth);
    }

    private static Direction ToDirection(string dir) =>
        dir.ToLower() switch
        {
            "forward" => Direction.Forward,
            "up" => Direction.Up,
            _ => Direction.Down
        };
}
