namespace Common;

public interface IAdventOfCodeDay
{
    DateOnly Year { get; }

    ValueTask ExecutePart1();

    ValueTask ExecutePart2();
}
