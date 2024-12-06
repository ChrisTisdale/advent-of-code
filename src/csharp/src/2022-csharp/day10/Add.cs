namespace AdventOfCode2022.day10;

internal record Add(int Value) : ICommand
{
    public int ClockCycles => 2;

    public int Execute(int currentValue) => Value + currentValue;

    public static Add Parse(string[] inputs)
    {
        if (inputs.Length < 2 || inputs[0].ToLower() != "addx")
        {
            throw new InvalidDataException();
        }

        return new Add(int.Parse(inputs[1]));
    }
}
