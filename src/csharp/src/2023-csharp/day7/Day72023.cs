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

namespace AdventOfCode2023.day7;

using Common;

public class Day72023 : BaseAdventOfCodeDay<long>
{
    public override DateOnly Year => new(2023, 12, 7);

    public override async ValueTask<long> ExecutePart1(Stream stream, CancellationToken token = default)
    {
        var hands = await ParseInput(stream, false, token);
        hands.Sort(new HandComparer());
        var count = 0L;
        for (var i = 0L; i < hands.Count; ++i)
        {
            count += (i + 1) * hands[(int)i].Bid;
        }

        return count;
    }

    public override async ValueTask<long> ExecutePart2(Stream stream, CancellationToken token = default)
    {
        var hands = await ParseInput(stream, true, token);
        hands.Sort(new HandComparer());
        var count = 0L;
        for (var i = 0L; i < hands.Count; ++i)
        {
            count += (i + 1) * hands[(int)i].Bid;
        }

        return count;
    }

    private static async ValueTask<List<Hand>> ParseInput(Stream stream, bool withJoker, CancellationToken token)
    {
        var hands = new List<Hand>();
        using var sr = new StreamReader(stream);
        while (!sr.EndOfStream)
        {
            var line = await sr.ReadLineAsync(token);
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var split = line.Split(' ');
            var cardNumbers = split[0].Select(x => x.ToCardNumber(withJoker)).ToArray();
            var bid = long.Parse(split[1]);
            hands.Add(new Hand(cardNumbers, bid));
        }

        return hands;
    }
}
