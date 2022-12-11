﻿using System.Text;

var result = await FindScore("sample.txt");
Console.WriteLine($"Sample Found scores: {result}");

result = await FindScore("measurements.txt");
Console.WriteLine($"Measure Found scores: {result}");

ValueTask<decimal> FindScore(string filename)
{
    var score = 0m;
    var readLines = File.ReadLines(filename).ToArray();
    var i = 0;
    for (; i <= readLines.Length - 3; i += 3)
    {
        var first = Encoding.ASCII.GetBytes(readLines[i]);
        var second = Encoding.ASCII.GetBytes(readLines[i + 1]);
        var third = Encoding.ASCII.GetBytes(readLines[i + 2]);
        var common = FindCommon(first, second);
        score += FindCommon(common, third).Select(GetPriority).Sum();
    }

    return new ValueTask<decimal>(score);
}

IEnumerable<byte> FindCommon(IEnumerable<byte> first, IEnumerable<byte> second)
{
    var enumerable = second as byte[] ?? second.ToArray();
    foreach (var value in first.Distinct())
    {
        if (!enumerable.Contains(value))
        {
            continue;
        }

        yield return value;
    }
}

int GetPriority(byte c)
{
    return c > 90 ? c - 96 : c - 38;
}