namespace AdventOfCode2023.day3;

using Common;

public class Day32023 : BaseAdventOfCodeDay<long>
{
    public override DateOnly Year => new(2023, 12, 3);

    public override async ValueTask<long> ExecutePart1(Stream stream)
    {
        var data = await ReadInput(stream);
        return ProcessPart1(data);
    }

    public override async ValueTask<long> ExecutePart2(Stream stream)
    {
        var data = await ReadInput(stream);
        return ProcessPart2(data);
    }

    private static long ProcessPart2(IReadOnlyList<string> input)
    {
        var count = 0L;
        for (var i = 0; i < input.Count; ++i)
        {
            for (var j = 0; j < input[i].Length; ++j)
            {
                if (input[i][j] != '*')
                {
                    continue;
                }

                var adjacent = GetAdjacent(input, i, j);
                if (adjacent.Count == 2)
                {
                    count += adjacent[0] * adjacent[1];
                }
            }
        }

        return count;
    }

    private static int GetInt(string input, int current, out int r)
    {
        var left = current - 1;
        var right = current + 1;
        r = current;
        var result = int.Parse(input.AsSpan(current, 1));
        while (left >= 0 && int.TryParse(input.AsSpan(left, current - left + 1), out var found))
        {
            result = found;
            --left;
        }

        ++left;
        while (right < input.Length && int.TryParse(input.AsSpan(left, right - left + 1), out var found))
        {
            r = right;
            result = found;
            ++right;
        }

        return result;
    }

    private static IReadOnlyList<int> GetAdjacent(IReadOnlyList<string> input, int currentLine, int currentStartIndex)
    {
        var topSearch = Math.Max(0, currentLine - 1);
        var left = Math.Max(0, currentStartIndex - 1);
        var right = Math.Min(input[currentStartIndex].Length - 1, currentStartIndex + 1);
        var bottom = Math.Min(input.Count - 1, currentLine + 1);
        var found = new List<int>();
        if (topSearch != currentLine)
        {
            for (var i = left; i <= right;)
            {
                if (!int.TryParse(input[topSearch].AsSpan(i, 1), out _))
                {
                    ++i;
                    continue;
                }

                found.Add(GetInt(input[topSearch], i, out var r));
                i = r + 1;
            }
        }

        if (bottom != currentLine)
        {
            for (var i = left; i <= right;)
            {
                if (!int.TryParse(input[bottom].AsSpan(i, 1), out _))
                {
                    ++i;
                    continue;
                }

                found.Add(GetInt(input[bottom], i, out var r));
                i = r + 1;
            }
        }

        if (left != currentStartIndex && int.TryParse(input[currentLine].AsSpan(left, 1), out _))
        {
            found.Add(GetInt(input[currentLine], left, out _));
        }

        if (right != currentStartIndex && int.TryParse(input[currentLine].AsSpan(right, 1), out _))
        {
            found.Add(GetInt(input[currentLine], right, out _));
        }

        return found;
    }

    private static async ValueTask<IReadOnlyList<string>> ReadInput(Stream stream)
    {
        using var sr = new StreamReader(stream);
        var data = new List<string>();
        while (!sr.EndOfStream)
        {
            var line = await sr.ReadLineAsync();
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            data.Add(line);
        }

        return data;
    }

    private static long ProcessPart1(IReadOnlyList<string> input)
    {
        var count = 0L;
        for (var i = 0; i < input.Count; ++i)
        {
            for (var j = 0; j < input[i].Length;)
            {
                var increment = 1;
                int? lastInt = null;
                while (int.TryParse(input[i].AsSpan(j, increment), out var cur))
                {
                    lastInt = cur;
                    ++increment;
                    if (j + increment > input[i].Length)
                    {
                        break;
                    }
                }

                if (lastInt.HasValue && IsPartNumber(input, i, j, j + increment - 2))
                {
                    count += lastInt.Value;
                }

                j += increment;
            }
        }

        return count;
    }

    private static bool IsPartNumber(IReadOnlyList<string> input, int currentLine, int currentStartIndex, int endingIndex)
    {
        var topSearch = Math.Max(0, currentLine - 1);
        var left = Math.Max(0, currentStartIndex - 1);
        var right = Math.Min(input[currentStartIndex].Length - 1, endingIndex + 1);
        var bottom = Math.Min(input.Count - 1, currentLine + 1);
        if (topSearch != currentLine)
        {
            for (var i = left; i <= right; ++i)
            {
                if (!int.TryParse(input[topSearch].AsSpan(i, 1), out var _) && input[topSearch][i] != '.')
                {
                    return true;
                }
            }
        }

        if (bottom != currentLine)
        {
            for (var i = left; i <= right; ++i)
            {
                if (!int.TryParse(input[bottom].AsSpan(i, 1), out var _) && input[bottom][i] != '.')
                {
                    return true;
                }
            }
        }

        if (left != currentStartIndex && !int.TryParse(input[currentLine].AsSpan(left, 1), out var _) && input[currentLine][left] != '.')
        {
            return true;
        }

        if (right != currentStartIndex)
        {
            return !int.TryParse(input[currentLine].AsSpan(right, 1), out var _) && input[currentLine][right] != '.';
        }

        return false;
    }
}
