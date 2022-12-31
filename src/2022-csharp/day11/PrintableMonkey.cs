namespace AdventOfCode2022.day11;

using System.Text;

internal record PrintableMonkey(int Id, IReadOnlyList<long> Items, NewCalculator Calculator, Evaluator[] Evaluators)
{
    protected virtual bool PrintMembers(StringBuilder builder)
    {
        const string separator = ", ";
        const string equals = " = ";
        const string arrayStart = "[ ";
        const string arrayEnd = " ]";
        builder.Append(nameof(Id)).Append(equals).Append(Id).Append(separator);
        builder.Append(nameof(Items))
            .Append(equals)
            .Append(arrayStart)
            .Append(string.Join(separator, Items))
            .Append(arrayEnd)
            .Append(separator);

        builder.Append(nameof(Calculator)).Append(equals).Append(Calculator).Append(separator);
        builder.Append(nameof(Evaluators))
            .Append(equals)
            .Append(arrayStart)
            .Append(string.Join(", ", Evaluators.Select(x => x.ToString())))
            .Append(arrayEnd);

        return true;
    }
}
