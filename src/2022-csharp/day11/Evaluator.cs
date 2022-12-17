namespace AdventOfCode2022.day11;

internal record Evaluator(bool Expected, long Divisor, int NextId)
{
    public bool IsMeet(long result) => Expected ? result % Divisor == 0 : result % Divisor != 0;
}
