namespace AdventOfCode2022;

using Common;

public abstract class Base2022AdventOfCodeDay<T> : BaseAdventOfCodeDay<T>
{
    protected Base2022AdventOfCodeDay()
    {
    }

    protected Base2022AdventOfCodeDay(IReadOnlyDictionary<Part, string[]> files)
        : base(files)
    {
    }

    public override DateOnly Year => new(2022, 12, 1);
}
