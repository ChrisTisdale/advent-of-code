using AdventOfCode2022;
using Common;
using Microsoft.Extensions.DependencyInjection;
using NaturalSort.Extension;

var serviceCollection = new ServiceCollection();
serviceCollection.Scan(
    x => x.FromAssemblies(typeof(I2022Marker).Assembly)
        .AddClasses(z => z.AssignableTo<IAdventOfCodeDay>())
        .AsImplementedInterfaces());

var provider = serviceCollection.BuildServiceProvider();

var runnableDays = provider.GetServices<IAdventOfCodeDay>().GroupBy(x => x.Year).ToDictionary(x => x.Key, x => x.ToList());

var runnableDaysKeys = runnableDays.Keys.Order().ToArray();
while (true)
{
    if (runnableDaysKeys.Length > 1)
    {
        if (!await ProcessYear())
        {
            return;
        }
    }
    else
    {
        if (!await ProcessDay(runnableDaysKeys.First()))
        {
            return;
        }
    }
}

async ValueTask<bool> ProcessYear()
{
    Console.Clear();
    for (var i = 0; i < runnableDaysKeys.Length; i++)
    {
        Console.WriteLine($"Press {i + 1} for {runnableDaysKeys[i]}");
    }

    Console.WriteLine("Press Q to Quit");
    Console.Write("Input: ");
    var read = Console.ReadLine();

    if (int.TryParse(read, out var index) && index <= runnableDaysKeys.Length)
    {
        return await ProcessDay(runnableDaysKeys[index - 1]);
    }

    if (read is "q" or "Q")
    {
        return false;
    }

    return await ProcessYear();
}

async ValueTask<bool> ProcessDay(DateOnly dateOnly)
{
    Console.Clear();
    var adventOfCodeDays =
        runnableDays[dateOnly]
            .OrderBy(x => x.GetType().Name, new NaturalSortComparer(StringComparison.InvariantCulture))
            .ToArray();
    for (var i = 0; i < adventOfCodeDays.Length; i++)
    {
        Console.WriteLine($"Press {i + 1} for {adventOfCodeDays[i].GetType().Name}");
    }

    if (runnableDaysKeys.Length > 1)
    {
        Console.WriteLine("Press Y to return to the year selection");
    }

    Console.WriteLine("Press Q to Quit");
    Console.Write("Input: ");
    var read = Console.ReadLine();
    if (int.TryParse(read, out var index) && index <= adventOfCodeDays.Length)
    {
        return await ProcessPart(adventOfCodeDays[index - 1]);
    }

    if (read is "y" or "Y" && runnableDaysKeys.Length > 1)
    {
        return await ProcessYear();
    }

    if (read is "q" or "Q")
    {
        return false;
    }

    return await ProcessDay(dateOnly);
}

async ValueTask<bool> ProcessPart(IAdventOfCodeDay codeDay)
{
    Console.Clear();
    Console.WriteLine("Press 1 for Part1");
    Console.WriteLine("Press 2 for Part2");
    Console.WriteLine("Press D to return to the day selection");
    if (runnableDaysKeys.Length > 1)
    {
        Console.WriteLine("Press Y to return to the year selection");
    }

    Console.WriteLine("Press Q to Quit");
    Console.Write("Input: ");
    return Console.ReadLine() switch
    {
        "1" => await RunPart1(codeDay),
        "2" => await RunPart2(codeDay),
        "d" or "D" => await ProcessDay(codeDay.Year),
        "y" or "Y" when runnableDaysKeys.Length > 1 => await ProcessYear(),
        "q" or "Q" => false,
        _ => await ProcessPart(codeDay)
    };
}

async ValueTask<bool> RunPart1(IAdventOfCodeDay codeDay)
{
    Console.Clear();
    await codeDay.ExecutePart1();
    var key = GetExecutionKey();
    return key switch
    {
        "r" or "R" => await RunPart1(codeDay),
        "d" or "D" => await ProcessPart(codeDay),
        "y" or "Y" => await ProcessDay(codeDay.Year),
        "q" or "Q" => false,
        _ => true
    };
}

async ValueTask<bool> RunPart2(IAdventOfCodeDay codeDay)
{
    Console.Clear();
    await codeDay.ExecutePart2();
    var key = GetExecutionKey();
    return key switch
    {
        "r" or "R" => await RunPart2(codeDay),
        "d" or "D" => await ProcessPart(codeDay),
        "y" or "Y" => await ProcessDay(codeDay.Year),
        "q" or "Q" => false,
        _ => true
    };
}

string? GetExecutionKey()
{
    Console.WriteLine("Press R to re-run");
    Console.WriteLine("Press D to return to the day");
    if (runnableDaysKeys.Length > 1)
    {
        Console.WriteLine("Press Y to return to the year");
    }

    Console.WriteLine("Press Q to Quit");
    Console.WriteLine("Press any other Key to Return");
    Console.Write("Input: ");
    var s = Console.ReadLine();
    return s;
}
