using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

var regex = new Regex($"({Regex.Escape("+")}|{Regex.Escape("*")}|{Regex.Escape("-")}|{Regex.Escape("/")})");
//await HandleRound(true, 20);
//Console.WriteLine("");
await HandleRound(false, 10000, false);

async ValueTask HandleRound(bool round1, int roundCount, bool print = false)
{
    var result = await ProcessFile("sample.txt");
    var inspected = await EvaluateRounds(result, roundCount, round1, print);
    var highestTwo = inspected.Values.OrderDescending().Take(2).ToArray();
    Console.WriteLine($"Sample-{(round1 ? 1 : 2)} Answer is: {(highestTwo[0] * highestTwo[1])}");

    result = await ProcessFile("measurements.txt");
    inspected = await EvaluateRounds(result, roundCount, round1, print);
    highestTwo = inspected.Values.OrderDescending().Take(2).ToArray();
    Console.WriteLine($"Measurements-{(round1 ? 1 : 2)} Answer is: {(highestTwo[0] * highestTwo[1])}");
}

async ValueTask<IReadOnlyList<Monkey>> ProcessFile(string fileName)
{
    var monkeys = new List<Monkey>();
    var linesAsync = await File.ReadAllLinesAsync(fileName);
    for (var i = 0; i < linesAsync.Length; i += 7)
    {
        var id = int.Parse(linesAsync[i].Split(' ')[1].TrimEnd(':'));
        var ids = linesAsync[i + 1].Split(':')[1].Split(',').Select(x => x.Trim(' ')).Select(long.Parse);
        var calc = ParseCalc(linesAsync[i + 2]);
        var evaluators = GetEvaluators(linesAsync.AsSpan(i + 3, 3));
        monkeys.Add(new Monkey(id, new Queue<long>(ids), calc, evaluators));
    }

    return monkeys;
}

async ValueTask<IReadOnlyDictionary<int, long>> EvaluateRounds(IReadOnlyList<Monkey> monkeys, int roundCount, bool damagesPerRound, bool printRounds = false)
{
    var inspectedCount = monkeys.ToDictionary(m => m.Id, _ => 0L);
    var factor = monkeys.Aggregate(1L, (f, m) => f * m.Evaluators.First().Divisor);
    for (var i = 0; i < roundCount; ++i)
    {
        if (printRounds && i % 1000 == 0)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine($"Start of Round {(i + 1)}:");
        }
        
        await EvaluateRound(monkeys, inspectedCount, damagesPerRound, factor, printRounds  && i % 1000 == 0);
    }

    return inspectedCount;
}

ValueTask EvaluateRound(IReadOnlyList<Monkey> monkeys, IDictionary<int, long> inspectCounter, bool damagesPerRound, long factor, bool printResult = false)
{
    foreach (var monkey in monkeys)
    {
        while (monkey.Items.Any())
        {
            var value = monkey.Items.Dequeue();
            value = monkey.Calculator.GetNew(value, factor);
            if (damagesPerRound)
            {
                value = value / 3;
            }
            
            var evaluator = monkey.Evaluators.First(x => x.IsMeet(value));
            monkeys[evaluator.NextId].Items.Enqueue(value);
            inspectCounter[monkey.Id] += 1L;
        }
    }

    if (!printResult)
    {
        return ValueTask.CompletedTask;
    }
    
    foreach (var monkey in monkeys)
    {
        Console.WriteLine($"Monkey-{monkey.Id} inspected: {inspectCounter[monkey.Id]}");
    }

    return ValueTask.CompletedTask;
}

NewCalculator ParseCalc(string line)
{
    var function = line.Split(':')[1].Replace(" ", string.Empty);
    var operation = function.Split('=')[1];
    var dataPoints = regex.Split(operation).ToArray();
    return new NewCalculator(dataPoints[0], dataPoints[2], GetOperator(dataPoints[1]));
}

Operator GetOperator(string value) =>
    value switch
    {
        "+" => Operator.Add,
        "*" => Operator.Multiply,
        "-" => Operator.Subtract,
        "/" => Operator.Divide,
        _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
    };

Evaluator[] GetEvaluators(ReadOnlySpan<string> lines)
{
    var checker = int.Parse(lines[0].Split(' ').Last());
    return new[]
    {
        GetEvaluator(checker, lines[1]),
        GetEvaluator(checker, lines[2])
    };
}

Evaluator GetEvaluator(int checker, string line)
{
    var truther = line.Replace(":", string.Empty).Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var expected = bool.Parse(truther[1]);
    var id = int.Parse(truther.Last());
    return new Evaluator(expected, checker, id);
}
class Monkey
{
    public int Id { get; }
    
    public Queue<long> Items { get; }
    
    public NewCalculator Calculator { get; }
    
    public Evaluator[] Evaluators { get; }

    public Monkey(int id, Queue<long> items, NewCalculator calculator, Evaluator[] evaluators)
    {
        Id = id;
        Items = items;
        Calculator = calculator;
        Evaluators = evaluators;
    }

    public override string ToString() => new PrintableMonkey(Id, Items.ToArray(), Calculator, Evaluators).ToString();
}

record class PrintableMonkey(int Id, IReadOnlyList<long> Items, NewCalculator Calculator, Evaluator[] Evaluators)
{
    protected virtual bool PrintMembers(StringBuilder builder)
    {
        const string sperator = ", ";
        const string equals = " = ";
        const string arraryStart = "[ ";
        const string arraryEnd = " ]";
        builder.Append(nameof(Id)).Append(equals).Append(Id).Append(sperator);
        builder.Append(nameof(Items)).Append(equals).Append(arraryStart).Append(string.Join(", ", Items))
            .Append(arraryEnd).Append(sperator);
        builder.Append(nameof(Calculator)).Append(equals).Append(Calculator.ToString()).Append(sperator);
        builder.Append(nameof(Evaluators)).Append(equals).Append(arraryStart).Append(string.Join(", ", Evaluators.Select(x => x.ToString())))
            .Append(arraryEnd);
        return true;
    }
}

internal record class Evaluator(bool Expected, long Divisor, int NextId)
{
    public bool IsMeet(long result) => Expected ? result % Divisor == 0 : result % Divisor != 0;
}

enum Operator
{
    Add,
    Subtract,
    Multiply,
    Divide
}

internal record class NewCalculator(string Left, string Right, Operator Operator)
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