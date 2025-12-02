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

namespace AdventOfCode2023.day2;

using Common;

public class Day22023 : BaseAdventOfCodeDay<long>
{
    private static readonly IReadOnlyDictionary<Cube, int> CubSizes = new Dictionary<Cube, int>
    {
        { Cube.Red, 12 },
        { Cube.Green, 13 },
        { Cube.Blue, 14 }
    };

    public override DateOnly Year => new(2023, 12, 2);

    public override async ValueTask<long> ExecutePart1(Stream stream, CancellationToken token = default)
    {
        var games = await ParseFile(stream, token);
        return games.Where(g => g.Rounds.All(r => r.Sizes.All(s => CubSizes[s.Key] >= s.Value)))
            .Sum(x => x.Id);
    }

    public override async ValueTask<long> ExecutePart2(Stream stream, CancellationToken token = default)
    {
        var games = await ParseFile(stream, token);
        var count = 0L;
        foreach (var game in games)
        {
            var red = 0;
            var blue = 0;
            var green = 0;
            foreach (var round in game.Rounds)
            {
                if (round.Sizes.TryGetValue(Cube.Red, out var r))
                {
                    red = Math.Max(red, r);
                }

                if (round.Sizes.TryGetValue(Cube.Blue, out var b))
                {
                    blue = Math.Max(blue, b);
                }

                if (round.Sizes.TryGetValue(Cube.Green, out var g))
                {
                    green = Math.Max(green, g);
                }
            }

            count += red * blue * green;
        }

        return count;
    }

    private static async ValueTask<List<Game>> ParseFile(Stream stream, CancellationToken token)
    {
        using var sr = new StreamReader(stream);
        var games = new List<Game>();
        while (await sr.ReadLineAsync(token) is { } line)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var gameData = line.Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var rounds = new List<Round>();
            foreach (var d in gameData[1].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            {
                var counts = new Dictionary<Cube, int>();
                var marbles = d.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (var marble in marbles)
                {
                    var values = marble.Split(' ');
                    var cube = Enum.Parse<Cube>(values[1], true);
                    var count = int.Parse(values[0]);
                    counts.Add(cube, count);
                }

                rounds.Add(new Round(counts));
            }

            var gameInfo = gameData[0].Split(' ');
            var gameId = int.Parse(gameInfo[1]);
            games.Add(new Game(gameId, rounds));
        }

        return games;
    }
}
