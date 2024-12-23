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

namespace AdventOfCode2023.day6;

using Common;

public class Day62023 : BaseAdventOfCodeDay<long>
{
    public override DateOnly Year => new(2023, 12, 6);

    public override async ValueTask<long> ExecutePart1(Stream stream, CancellationToken token = default)
    {
        var races = await GetRaces(stream, token);
        return races.Aggregate(1L, (current, race) => current * GetPossibleWins(race));
    }

    public override async ValueTask<long> ExecutePart2(Stream stream, CancellationToken token = default)
    {
        var race = await GetRace(stream, token);
        return GetPossibleWins(race);
    }

    private static long GetPossibleWins(Race race)
    {
        if (race.Distance == 0)
        {
            return (long)race.RaceTime.TotalMilliseconds - 2L;
        }

        var count = 0;
        for (var i = 1L; i < (long)race.RaceTime.TotalMilliseconds; ++i)
        {
            var traveledDistance = ((long)race.RaceTime.TotalMilliseconds - i) * i;
            if (race.Distance < traveledDistance)
            {
                ++count;
            }
            else if (count > 0)
            {
                break;
            }
        }

        return count;
    }

    private static async ValueTask<IReadOnlyList<Race>> GetRaces(Stream stream, CancellationToken token)
    {
        using var sr = new StreamReader(stream);
        var times = await sr.ReadLineAsync(token).ConfigureAwait(false);
        var distances = await sr.ReadLineAsync(token).ConfigureAwait(false);
        var raceTimes = times!.Split(':')[1]
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .Select(x => TimeSpan.FromMilliseconds(x))
            .ToArray();
        var allDistances = distances!.Split(':')[1]
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray();

        return raceTimes.Select((t, i) => new Race(t, allDistances[i])).ToList();
    }

    private static async ValueTask<Race> GetRace(Stream stream, CancellationToken token)
    {
        using var sr = new StreamReader(stream);
        var times = await sr.ReadLineAsync(token).ConfigureAwait(false);
        var distances = await sr.ReadLineAsync(token).ConfigureAwait(false);
        var time = times!.Split(':')[1]
            .Replace(" ", string.Empty);
        var distance = distances!.Split(':')[1]
            .Replace(" ", string.Empty);
        return new Race(TimeSpan.FromMilliseconds(long.Parse(time)), long.Parse(distance));
    }
}
