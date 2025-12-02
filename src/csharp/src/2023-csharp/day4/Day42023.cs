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

namespace AdventOfCode2023.day4;

using Common;

public class Day42023 : BaseAdventOfCodeDay<long>
{
    public override DateOnly Year => new(2023, 12, 4);

    public override async ValueTask<long> ExecutePart1(Stream stream, CancellationToken token = default)
    {
        var input = await ReadInput(stream, token);
        return input.Sum(
            card => card.NumbersObtained.Where(n => card.WinningNumbers.Contains(n))
                .Aggregate(0L, (current, _) => current == 0 ? 1 : current * 2));
    }

    public override async ValueTask<long> ExecutePart2(Stream stream, CancellationToken token = default)
    {
        var input = await ReadInput(stream, token);
        var cardCount = input.ToDictionary(x => x.Id - 1, _ => 1L);
        for (var i = 0; i < input.Count; ++i)
        {
            var c = i + 1;
            var sum = cardCount[i];
            foreach (var n in input[i].NumbersObtained)
            {
                if (c >= cardCount.Count || !input[i].WinningNumbers.Contains(n))
                {
                    continue;
                }

                cardCount[c] += sum;
                ++c;
            }
        }

        return cardCount.Values.Sum();
    }

    private static async ValueTask<IReadOnlyList<Card>> ReadInput(Stream stream, CancellationToken token)
    {
        var data = new List<Card>();
        using var sr = new StreamReader(stream);
        while (await sr.ReadLineAsync(token) is { } line)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var inputs = line.Split(": ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var nums = inputs[1].Split(" | ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var winning = nums[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var obtained = nums[1]
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            data.Add(new Card(data.Count + 1, winning, obtained));
        }

        return data;
    }
}
