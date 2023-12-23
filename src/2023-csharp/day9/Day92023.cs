namespace AdventOfCode2023.day9;

using Common;

public class Day92023 : BaseAdventOfCodeDay<long>
{
    public override DateOnly Year => new(2023, 12, 9);

    public override async ValueTask<long> ExecutePart1(Stream stream) =>
        await ReadLinesAsync(stream).Select(x => x.PredictNext()).Select(next => next.Values[^1]).SumAsync();

    public override async ValueTask<long> ExecutePart2(Stream stream) =>
        await ReadLinesAsync(stream).Select(x => x.PredictPrevious()).Select(next => next.Values[0]).SumAsync();

    private static IAsyncEnumerable<Readings> ReadLinesAsync(Stream stream) =>
        EnumerateLinesAsync(stream)
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray())
            .Select(values => new Readings(values));
}
