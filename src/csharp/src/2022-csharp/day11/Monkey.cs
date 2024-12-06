namespace AdventOfCode2022.day11;

internal class Monkey
{
    public Monkey(int id, Queue<long> items, NewCalculator calculator, Evaluator[] evaluators)
    {
        Id = id;
        Items = items;
        Calculator = calculator;
        Evaluators = evaluators;
    }

    public int Id { get; }

    public Queue<long> Items { get; }

    public NewCalculator Calculator { get; }

    public Evaluator[] Evaluators { get; }

    public override string ToString() =>
        new PrintableMonkey(Id, Items.ToArray(), Calculator, Evaluators).ToString()!;
}
