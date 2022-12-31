namespace AdventOfCode2021;

using Common;

public abstract class Base2021AdventOfCodeDay<T> : BaseAdventOfCodeDay<T>
{
    protected Base2021AdventOfCodeDay()
    {
    }

    protected Base2021AdventOfCodeDay(IReadOnlyDictionary<Part, string[]> files)
        : base(files)
    {
    }

    public override DateOnly Year => new(2021, 12, 1);
}
