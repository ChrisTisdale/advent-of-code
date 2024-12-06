namespace Common;

public interface IAdventOfCodeDay
{
    DateOnly Year { get; }

    ValueTask ExecutePart1(CancellationToken token = default);

    ValueTask ExecutePart2(CancellationToken token = default);
}
