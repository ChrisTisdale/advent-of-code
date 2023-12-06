namespace AdventOfCode2023.day5;

public readonly record struct SeedRange(long Start, long Stop)
{
    public bool InRange(long value)
    {
        return Start <= value && Stop >= value;
    }
}
