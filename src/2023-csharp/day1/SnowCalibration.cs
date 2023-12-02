namespace AdventOfCode2023.day1;

public readonly record struct SnowCalibration(int? Start, int End)
{
    public bool HasData => Start.HasValue;

    public int Total => Start.HasValue ? Start.Value * 10 + End : throw new InvalidOperationException();
}
