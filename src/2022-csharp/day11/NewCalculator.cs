namespace AdventOfCode2022.day11;

internal record NewCalculator(string Left, string Right, Operator Operator)
{
    public long GetNew(long old)
    {
        var left = GetValue(Left, old);
        var right = GetValue(Right, old);
        checked
        {
            return Operator switch
            {
                Operator.Add => left + right,
                Operator.Subtract => left - right,
                Operator.Multiply => left * right,
                Operator.Divide => left / right,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public long GetNew(long old, long factor)
    {
        return GetNew(old) % factor;
    }

    private static long GetValue(string value, long old)
    {
        return value.ToLower() == "old" ? old : long.Parse(value);
    }
}
