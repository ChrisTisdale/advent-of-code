namespace AdventOfCode2022.day10;

public class Day10 : Base2022AdventOfCodeDay<int>
{
    public override async ValueTask<int> ExecutePart1(string fileName) => await ProcessSignals(fileName);

    public override async ValueTask<int> ExecutePart2(string fileName) => await ProcessSignals(fileName);

    private static async ValueTask<int> ProcessSignals(string fileName)
    {
        var cycles = new[] { 20, 60, 100, 140, 180, 220 };
        var result = await GetSignalStrengths(await ProcessFile(fileName), cycles);
        return result.Sum();
    }

    private static async ValueTask<IReadOnlyList<ICommand>> ProcessFile(string fileName)
    {
        var items = new List<ICommand>();
        await foreach (var line in File.ReadLinesAsync(fileName))
        {
            var inputs = line.Split(' ');
            var lower = inputs[0].ToLower();
            switch (lower)
            {
                case "noop":
                    items.Add(new NoOp());
                    break;
                case "addx":
                    items.Add(Add.Parse(inputs));
                    break;
            }
        }

        return items;
    }

    private static ValueTask<IReadOnlyList<int>> GetSignalStrengths(IEnumerable<ICommand> inputs, params int[] tracking)
    {
        var points = new int[tracking.Length];
        var clockCycles = 0;
        var currentValue = 1;
        var currentPos = 0;
        var image = new char[40];
        foreach (var input in inputs)
        {
            for (var i = 0; i < input.ClockCycles; ++i)
            {
                var lookLoc = clockCycles % 40 - currentValue + 1;
                if (lookLoc is >= 0 and < 3)
                {
                    image[currentPos++] = '#';
                }
                else
                {
                    image[currentPos++] = '.';
                }

                if (currentPos >= image.Length)
                {
                    currentPos = 0;
                    Console.WriteLine(new string(image));
                }

                ++clockCycles;
                var indexOf = Array.IndexOf(tracking, clockCycles);
                if (indexOf != -1)
                {
                    points[indexOf] = currentValue * clockCycles;
                }
            }

            currentValue = input.Execute(currentValue);
        }

        return new ValueTask<IReadOnlyList<int>>(points);
    }
}
