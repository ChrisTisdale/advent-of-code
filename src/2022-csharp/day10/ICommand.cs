namespace AdventOfCode2022.day10;

internal interface ICommand
{
    int ClockCycles { get; }

    int Execute(int currentValue);
}
