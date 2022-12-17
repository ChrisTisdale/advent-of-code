namespace AdventOfCode2022;

using Common;

public abstract class Base2022 : IAdventOfCodeDay
{
    public DateOnly Year => new(2022, 12, 1);

    public abstract ValueTask ExecutePart1();
    public abstract ValueTask ExecutePart2();

    protected string GetFileLocation(string file)
    {
        var ns = GetType().Namespace ?? throw new InvalidOperationException();
        return Path.Combine(ns.Split('.').Last(), file);
    }
}
