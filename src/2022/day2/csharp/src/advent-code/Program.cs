var (result1, result2) = (await FindScore("sample.txt", true), await FindScore("sample.txt", false));
Console.WriteLine($"Sample Found scores (round1, round2): ({result1}, {result2})");

(result1, result2) = (await FindScore("measurements.txt", true), await FindScore("measurements.txt", false));
Console.WriteLine($"Measure Found scores (round1, round2): ({result1}, {result2})");

async ValueTask<decimal> FindScore(string filename, bool round1)
{
    var score = 0m;
    await foreach (var readLine in File.ReadLinesAsync(filename))
    {
        if (string.IsNullOrWhiteSpace(readLine))
        {
            continue;
        }

        var strings = readLine.Split(' ');
        var opponent = GetResult(strings[0]);
        var yours = GetResult(round1 ? strings[1] : readLine);
        var found = CalculateScore(opponent, yours);
        score += found;
    }

    return score;
}

Game GetResult(string value)
{
    return value.ToUpper() switch
    {
        "A" or "X" or "A Y" or "B X" or "C Z"=> Game.Rock,
        "B" or "Y" or "B Y" or "A Z" or "C X" => Game.Paper,
        "C" or "Z" or "C Y" or "A X" or "B Z" => Game.Scissors,
        _ => throw new ArgumentOutOfRangeException(nameof(value), $"The value of {value} is not valid")
    };
}

decimal CalculateScore(Game opponent, Game yours)
{
    return (decimal)yours + GetGameScore(opponent, yours);
}

decimal GetGameScore(Game opponent, Game yours)
{
    return opponent switch
    {
        Game.Rock => yours switch
        {
            Game.Rock => 3m,
            Game.Paper => 6m,
            Game.Scissors => 0m,
            _ => throw new ArgumentOutOfRangeException(nameof(yours), yours, null)
        },
        Game.Paper => yours switch
        {
            Game.Rock => 0m,
            Game.Paper => 3m,
            Game.Scissors => 6m,
            _ => throw new ArgumentOutOfRangeException(nameof(yours), yours, null)
        },
        Game.Scissors => yours switch
        {
            Game.Rock => 6m,
            Game.Paper => 0m,
            Game.Scissors => 3m,
            _ => throw new ArgumentOutOfRangeException(nameof(yours), yours, null)
        },
        _ => throw new ArgumentOutOfRangeException(nameof(opponent), opponent, null)
    };
}

public enum Game
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}
