namespace AdventOfCode2023.day6;

using Common;

public class Day62023 : BaseAdventOfCodeDay<long>
{
    public override DateOnly Year => new(2023, 12, 6);

    public override async ValueTask<long> ExecutePart1(Stream stream)
    {
        var races = await GetRaces(stream);
        return races.Aggregate(1L, (current, race) => current * GetPossibleWins(race));
    }

    public override async ValueTask<long> ExecutePart2(Stream stream)
    {
        var race = await GetRace(stream);
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

    private static async ValueTask<IReadOnlyList<Race>> GetRaces(Stream stream)
    {
        using var sr = new StreamReader(stream);
        var times = await sr.ReadLineAsync().ConfigureAwait(false);
        var distances = await sr.ReadLineAsync().ConfigureAwait(false);
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

    private static async ValueTask<Race> GetRace(Stream stream)
    {
        using var sr = new StreamReader(stream);
        var times = await sr.ReadLineAsync().ConfigureAwait(false);
        var distances = await sr.ReadLineAsync().ConfigureAwait(false);
        var time = times!.Split(':')[1]
            .Replace(" ", string.Empty);
        var distance = distances!.Split(':')[1]
            .Replace(" ", string.Empty);
        return new Race(TimeSpan.FromMilliseconds(long.Parse(time)), long.Parse(distance));
    }
}
