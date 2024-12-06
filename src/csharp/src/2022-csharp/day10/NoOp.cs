namespace AdventOfCode2022.day10;

internal record NoOp : ICommand
{
    public int ClockCycles => 1;

    public int Execute(int currentValue) => currentValue;
}
