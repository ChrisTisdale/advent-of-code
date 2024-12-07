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

namespace AdventOfCode2021.day4;

public sealed class Day4 : Base2021AdventOfCodeDay<int>
{
    public override async ValueTask<int> ExecutePart1(Stream fileName, CancellationToken token = default)
    {
        var bingoGame = await GetInput(fileName, token);
        return GetPart1Win(bingoGame);
    }

    public override async ValueTask<int> ExecutePart2(Stream fileName, CancellationToken token = default)
    {
        var bingoGame = await GetInput(fileName, token);
        return GetPart2Win(bingoGame);
    }

    private static int GetPart1Win(BingoGame bingoGame)
    {
        for (var i = BingoCard.CardSize; i <= bingoGame.BallCall.Length; ++i)
        {
            foreach (var card in bingoGame.BingoCards)
            {
                card.Daub(new ReadOnlySpan<int>(bingoGame.BallCall, i == BingoCard.CardSize ? 0 : i - 1, i == BingoCard.CardSize ? i : 1));
                if (card.HasWin())
                {
                    return card.UnDaubed().Sum() * bingoGame.BallCall[i - 1];
                }
            }
        }

        return 0;
    }

    private static int GetPart2Win(BingoGame bingoGame)
    {
        var cards = bingoGame.BingoCards.ToList();
        for (var i = BingoCard.CardSize; i <= bingoGame.BallCall.Length; ++i)
        {
            foreach (var card in cards.ToArray())
            {
                card.Daub(new ReadOnlySpan<int>(bingoGame.BallCall, i == BingoCard.CardSize ? 0 : i - 1, i == BingoCard.CardSize ? i : 1));
                if (!card.HasWin())
                {
                    continue;
                }

                if (cards.Count == 1)
                {
                    return card.UnDaubed().Sum() * bingoGame.BallCall[i - 1];
                }

                cards.Remove(card);
            }
        }

        return 0;
    }

    private async ValueTask<BingoGame> GetInput(Stream file, CancellationToken token)
    {
        var input = (await ReadAllLinesAsync(file, token)).ToArray();
        var ballCall = input[0].Split(',').Select(int.Parse).ToArray();
        var cards = new List<BingoCard>();
        for (var i = 2; i <= input.Length - BingoCard.CardSize; i += BingoCard.CardSize + 1)
        {
            cards.Add(ReadCard(new ReadOnlySpan<string>(input, i, BingoCard.CardSize)));
        }

        return new BingoGame(ballCall, cards);
    }

    private BingoCard ReadCard(ReadOnlySpan<string> cardString)
    {
        if (cardString.Length < BingoCard.CardSize)
        {
            throw new InvalidOperationException();
        }

        var card = new BingoNumber[BingoCard.CardSize][];
        for (var row = 0; row < BingoCard.CardSize; ++row)
        {
            card[row] = cardString[row]
                .Split(' ')
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => new BingoNumber(int.Parse(x), false))
                .ToArray();
        }

        return new BingoCard(card);
    }
}
