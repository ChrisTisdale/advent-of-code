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

namespace AdventOfCode2023.day5;

using Common;

public class Day52023 : BaseAdventOfCodeDay<long>
{
    private static readonly IReadOnlyDictionary<string, MappingType> MappingLookup = new Dictionary<string, MappingType>
    {
        { "humidity-to-location map:", MappingType.HumidityToLocation },
        { "temperature-to-humidity map:", MappingType.TempToHumidity },
        { "light-to-temperature map:", MappingType.LightToTemp },
        { "water-to-light map:", MappingType.WaterToLight },
        { "fertilizer-to-water map:", MappingType.FertilizerToWater },
        { "soil-to-fertilizer map:", MappingType.SoilToFertilizer },
        { "seed-to-soil map:", MappingType.SeedToSoil }
    };

    public override DateOnly Year => new(2023, 12, 5);

    public override async ValueTask<long> ExecutePart1(Stream stream, CancellationToken token = default)
    {
        var almanac = await ParseInput(stream, token);
        return almanac.Seeds.Min(seed => FindLocation(almanac, seed));
    }

    public override async ValueTask<long> ExecutePart2(Stream stream, CancellationToken token = default)
    {
        var almanac = await ParseInput(stream, token);
        var ranges = new List<SeedRange>();
        for (var s = 0; s < almanac.Seeds.Count - 1; s += 2)
        {
            ranges.Add(new SeedRange(almanac.Seeds[s], almanac.Seeds[s] + almanac.Seeds[s + 1] - 1));
        }

        for (var i = 0L; i < long.MaxValue; ++i)
        {
            var seed = FindSeed(almanac, i);
            if (ranges.Any(x => x.InRange(seed)))
            {
                return i;
            }
        }

        return long.MaxValue;
    }

    private static long GetMapped(MappingType mappingType, Almanac almanac, long value)
    {
        var mapping = almanac.Mappings[mappingType].FirstOrDefault(x => InMapping(x, value));
        return mapping is null ? value : mapping.Destination + (value - mapping.Source);
    }

    private static long GetMappedReversed(MappingType mappingType, Almanac almanac, long value)
    {
        var mapping = almanac.Mappings[mappingType].FirstOrDefault(x => InMappingReversed(x, value));
        return mapping is null ? value : mapping.Source + (value - mapping.Destination);
    }

    private static long FindLocation(Almanac almanac, long seed)
    {
        var soil = GetMapped(MappingType.SeedToSoil, almanac, seed);
        var fertilizer = GetMapped(MappingType.SoilToFertilizer, almanac, soil);
        var water = GetMapped(MappingType.FertilizerToWater, almanac, fertilizer);
        var light = GetMapped(MappingType.WaterToLight, almanac, water);
        var temp = GetMapped(MappingType.LightToTemp, almanac, light);
        var hum = GetMapped(MappingType.TempToHumidity, almanac, temp);
        var loc = GetMapped(MappingType.HumidityToLocation, almanac, hum);
        return loc;
    }

    private static long FindSeed(Almanac almanac, long loc)
    {
        var hum = GetMappedReversed(MappingType.HumidityToLocation, almanac, loc);
        var temp = GetMappedReversed(MappingType.TempToHumidity, almanac, hum);
        var light = GetMappedReversed(MappingType.LightToTemp, almanac, temp);
        var water = GetMappedReversed(MappingType.WaterToLight, almanac, light);
        var fertilizer = GetMappedReversed(MappingType.FertilizerToWater, almanac, water);
        var soil = GetMappedReversed(MappingType.SoilToFertilizer, almanac, fertilizer);
        return GetMappedReversed(MappingType.SeedToSoil, almanac, soil);
    }

    private static async ValueTask<Almanac> ParseInput(Stream stream, CancellationToken token)
    {
        using var sr = new StreamReader(stream);
        var line = await sr.ReadLineAsync(token);
        if (string.IsNullOrEmpty(line))
        {
            throw new InvalidOperationException();
        }

        var seeds = line.Split(": ")[1]
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray();

        var lookup = new Dictionary<MappingType, List<Mapping>>();
        var currentList = new List<Mapping>();
        var currentMapping = default(MappingType);
        while ((line = await sr.ReadLineAsync(token)) is not null)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            if (MappingLookup.TryGetValue(line, out var mapping))
            {
                UpdateInput(ref currentMapping, ref currentList, lookup, mapping);
            }
            else
            {
                var maps = line.Split(' ', StringSplitOptions.TrimEntries).Select(long.Parse).ToArray();
                currentList.Add(new Mapping(maps[0], maps[1], maps[2]));
            }
        }

        UpdateInput(ref currentMapping, ref currentList, lookup, MappingType.HumidityToLocation);
        return new Almanac(seeds, lookup);
    }

    private static void UpdateInput(
        ref MappingType currentMapping,
        ref List<Mapping> currentList,
        IDictionary<MappingType, List<Mapping>> lookup,
        MappingType updatedType)
    {
        if (lookup.TryGetValue(currentMapping, out var value))
        {
            value.AddRange(currentList);
            lookup[currentMapping] = value.OrderBy(x => x.Source).ToList();
        }
        else
        {
            lookup.Add(currentMapping, currentList.OrderBy(x => x.Source).ToList());
        }

        currentList = [];
        currentMapping = updatedType;
    }

    private static bool InMapping(Mapping mapping, long value) => mapping.Source <= value && mapping.Source + mapping.Range > value;

    private static bool InMappingReversed(Mapping mapping, long value) =>
        mapping.Destination <= value && mapping.Destination + mapping.Range > value;
}
