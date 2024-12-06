namespace AdventOfCode2021.day3;

public sealed class Day3 : Base2021AdventOfCodeDay<int>
{
    public override async ValueTask<int> ExecutePart1(Stream fileName, CancellationToken token = default)
    {
        var input = await ParsePart1Inputs(fileName, token);
        return input.Epsilon * input.Gamma;
    }

    public override async ValueTask<int> ExecutePart2(Stream fileName, CancellationToken token = default)
    {
        var input = await ParsePart2Inputs(fileName, token);
        return input.Epsilon * input.Gamma;
    }

    private static async ValueTask<Input> ParsePart1Inputs(Stream fileName, CancellationToken token)
    {
        var allLines = await ReadAllLinesAsync(fileName, token);
        if (allLines.Count == 0)
        {
            return new Input(0, 0);
        }

        var epsilon = 0;
        var gamma = 0;
        var bitLength = allLines[0].Length;
        for (var i = 0; i < bitLength; ++i)
        {
            var zeroCount = allLines.Count(x => x[i] == '0');
            var oneCount = allLines.Count - zeroCount;
            if (zeroCount > oneCount)
            {
                epsilon |= 1 << (bitLength - 1 - i);
            }
            else
            {
                gamma |= 1 << (bitLength - 1 - i);
            }
        }

        return new Input(gamma, epsilon);
    }

    private static async ValueTask<Input> ParsePart2Inputs(Stream fileName, CancellationToken token)
    {
        var allLines = await ReadAllLinesAsync(fileName, token);
        if (allLines.Count == 0)
        {
            return new Input(0, 0);
        }

        var oxygen = 0;
        var c02 = 0;
        var availableOxygen = allLines.ToList();
        var availableC02 = allLines.ToList();
        var bitLength = allLines[0].Length;
        for (var i = 0; i < bitLength; ++i)
        {
            oxygen = GetBit(availableOxygen, i, oxygen, bitLength, false);
            c02 = GetBit(availableC02, i, c02, bitLength, true);
        }

        return new Input(oxygen, c02);
    }

    private static int GetBit(List<string> available, int characterIndex, int value, int bitLength, bool leastAmount)
    {
        switch (available.Count)
        {
            case <= 0:
                return value;
            case 1:
                return (available[0][characterIndex] == '1') ? value | 1 << (bitLength - 1 - characterIndex) : value;
        }

        var zeroCount = available.Count(x => x[characterIndex] == '0');
        var oneCount = available.Count - zeroCount;
        if (leastAmount ? zeroCount <= oneCount : zeroCount > oneCount)
        {
            available.RemoveAll(x => x[characterIndex] == '1');
        }
        else
        {
            value |= 1 << (bitLength - 1 - characterIndex);
            available.RemoveAll(x => x[characterIndex] == '0');
        }

        return value;
    }
}
