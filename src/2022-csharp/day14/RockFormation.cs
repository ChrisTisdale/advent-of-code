namespace AdventOfCode2022.day14;

using System.Text;
using Common;

internal record RockFormation(IReadOnlyList<Line<int>> Lines)
{
    public bool IsInFormation(Point<int> point)
    {
        for (var i = 0; i < Lines.Count; ++i)
        {
            var line = Lines[i];
            if (line.IsInLine(point))
            {
                return true;
            }
        }

        return false;
    }

    public IEnumerable<Point<int>> GetPoints() => Lines.SelectMany(x => x.GetPoints());

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append(nameof(Lines)).Append(" = ").Append(" [ ").Append(string.Join(", ", Lines)).Append(" ] ");
        return true;
    }
}
