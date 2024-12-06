namespace AdventOfCode2023.day5;

public record Almanac(IReadOnlyList<long> Seeds, IReadOnlyDictionary<MappingType, List<Mapping>> Mappings);
