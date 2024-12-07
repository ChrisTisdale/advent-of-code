// Copyright (c) Christopher Tisdale 2024.
//
// Licensed under BSD-3-Clause.
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      https://spdx.org/licenses/BSD-3-Clause.html
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace AdventOfCode2022.day2;

public class Day2 : Base2022AdventOfCodeDay<decimal>
{
    public override async ValueTask<decimal> ExecutePart1(Stream fileName, CancellationToken token = default) =>
        await FindScore(fileName, true, token);

    public override async ValueTask<decimal> ExecutePart2(Stream fileName, CancellationToken token = default) =>
        await FindScore(fileName, false, token);

    private static async ValueTask<decimal> FindScore(Stream filename, bool round1, CancellationToken token)
    {
        var score = 0m;
        using var sr = new StreamReader(filename);
        while (!sr.EndOfStream)
        {
            var readLine = await sr.ReadLineAsync(token);
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

    private static Game GetResult(string value)
    {
        return value.ToUpper() switch
        {
            "A" or "X" or "A Y" or "B X" or "C Z" => Game.Rock,
            "B" or "Y" or "B Y" or "A Z" or "C X" => Game.Paper,
            "C" or "Z" or "C Y" or "A X" or "B Z" => Game.Scissors,
            _ => throw new ArgumentOutOfRangeException(nameof(value), $@"The value of {value} is not valid")
        };
    }

    private static decimal CalculateScore(Game opponent, Game yours) => (decimal)yours + GetGameScore(opponent, yours);

    private static decimal GetGameScore(Game opponent, Game yours)
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
}
