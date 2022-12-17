namespace AdventOfCode2022.day11;

using System.Text;

internal record PrintableMonkey(int Id, IReadOnlyList<long> Items, NewCalculator Calculator, Evaluator[] Evaluators)
{
    protected virtual bool PrintMembers(StringBuilder builder)
    {
        const string sperator = ", ";
        const string equals = " = ";
        const string arraryStart = "[ ";
        const string arraryEnd = " ]";
        builder.Append(nameof(Id)).Append(equals).Append(Id).Append(sperator);
        builder.Append(nameof(Items))
            .Append(equals)
            .Append(arraryStart)
            .Append(string.Join(", ", Items))
            .Append(arraryEnd)
            .Append(sperator);

        builder.Append(nameof(Calculator)).Append(equals).Append(Calculator.ToString()).Append(sperator);
        builder.Append(nameof(Evaluators))
            .Append(equals)
            .Append(arraryStart)
            .Append(string.Join(", ", Evaluators.Select(x => x.ToString())))
            .Append(arraryEnd);

        return true;
    }
}
