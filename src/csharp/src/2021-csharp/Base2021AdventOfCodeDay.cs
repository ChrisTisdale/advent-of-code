namespace AdventOfCode2021;

using Common;

public abstract class Base2021AdventOfCodeDay<T> : BaseAdventOfCodeDay<T>
{
    public override DateOnly Year => new(2021, 12, 1);
}
