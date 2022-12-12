var cycles = new[] { 20, 60, 100, 140, 180, 220 };
var result = await GetSignalStrengths(await ProcessFile("sample.txt"), cycles);
Console.WriteLine($"Sample Found: {result.Sum()}");

result = await GetSignalStrengths(await ProcessFile("measurements.txt"), cycles);
Console.WriteLine($"Sample Found: {result.Sum()}");

async ValueTask<IReadOnlyList<ICommand>> ProcessFile(string fileName)
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

ValueTask<IReadOnlyList<int>> GetSignalStrengths(IEnumerable<ICommand> inputs, params int[] tracking)
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

interface ICommand
{
    int ClockCycles { get; }

    int Execute(int currentValue);
}

record NoOp : ICommand
{
    public int ClockCycles => 1;
    
    public int Execute(int currentValue)
    {
        return currentValue;
    }
}

record Add(int Value) : ICommand
{
    public int ClockCycles => 2;
    
    public int Execute(int currentValue)
    {
        return Value + currentValue;
    }

    public static Add Parse(string[] inputs)
    {
        if (inputs.Length < 2 || inputs[0].ToLower() != "addx")
        {
            throw new InvalidDataException();
        }

        return new Add(int.Parse(inputs[1]));
    }
} 