using Common;
using Microsoft.Extensions.DependencyInjection;
using NaturalSort.Extension;
using Startup.Properties;

var serviceCollection = new ServiceCollection();
serviceCollection.Scan(
    x => x.FromApplicationDependencies()
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
    while (true)
    {
        Console.Clear();
        for (var i = 0; i < runnableDaysKeys.Length; i++)
        {
            Console.WriteLine(Resources.YearSelectionFmt, i + 1, runnableDaysKeys[i]);
        }

        Console.WriteLine(Resources.Quit);
        Console.Write(Resources.Input);
        var read = Console.ReadLine();

        if (int.TryParse(read, out var index) && index <= runnableDaysKeys.Length)
        {
            return await ProcessDay(runnableDaysKeys[index - 1]);
        }

        if (read is "q" or "Q")
        {
            return false;
        }
    }
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
        Console.WriteLine(Resources.YearSelectionFmt, i + 1, adventOfCodeDays[i].GetType().Name);
    }

    if (runnableDaysKeys.Length > 1)
    {
        Console.WriteLine(Resources.ReturnToYearSelection);
    }

    Console.WriteLine(Resources.Quit);
    Console.Write(Resources.Input);
    var read = Console.ReadLine();
    if (int.TryParse(read, out var index) && index <= adventOfCodeDays.Length)
    {
        return await ProcessPart(adventOfCodeDays[index - 1]);
    }

    return read switch
    {
        "y" or "Y" when runnableDaysKeys.Length > 1 => await ProcessYear(),
        "q" or "Q" => false,
        _ => await ProcessDay(dateOnly)
    };
}

async ValueTask<bool> ProcessPart(IAdventOfCodeDay codeDay)
{
    Console.Clear();
    Console.WriteLine(Resources.ExecutePart1);
    Console.WriteLine(Resources.ExecutePart2);
    Console.WriteLine(Resources.ReturnToDaySelection);
    if (runnableDaysKeys.Length > 1)
    {
        Console.WriteLine(Resources.ReturnToYearSelection);
    }

    Console.WriteLine(Resources.Quit);
    Console.Write(Resources.Input);
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
    return await Test(codeDay, runnableDaysKeys, RunPart1);
}

async Task<bool> Test(
    IAdventOfCodeDay adventOfCodeDay,
    IReadOnlyCollection<DateOnly> dates,
    Func<IAdventOfCodeDay, ValueTask<bool>> action,
    bool clear = false)
{
    if (clear)
    {
        Console.Clear();
    }

    var key = GetExecutionKey();
    return key switch
    {
        "r" or "R" => await action(adventOfCodeDay),
        "d" or "D" => await ProcessDay(adventOfCodeDay.Year),
        "y" or "Y" when dates.Count > 1 => await ProcessYear(),
        "q" or "Q" => false,
        "p" or "P" => await ProcessPart(adventOfCodeDay),
        _ => await Test(adventOfCodeDay, dates, action, true)
    };
}

async ValueTask<bool> RunPart2(IAdventOfCodeDay codeDay)
{
    Console.Clear();
    await codeDay.ExecutePart2();
    return await Test(codeDay, runnableDaysKeys, RunPart2);
}

string? GetExecutionKey()
{
    Console.WriteLine(Resources.ReRun);
    Console.WriteLine(Resources.ReturnToPartSelection);
    Console.WriteLine(Resources.ReturnToDaySelection);
    if (runnableDaysKeys.Length > 1)
    {
        Console.WriteLine(Resources.ReturnToYearSelection);
    }

    Console.WriteLine(Resources.Quit);
    Console.Write(Resources.Input);
    var s = Console.ReadLine();
    return s;
}
