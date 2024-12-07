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

public class BingoCard
{
    public static int CardSize => 5;

    public BingoCard(BingoNumber[][] card)
    {
        if (card == null)
        {
            throw new ArgumentNullException(nameof(card));
        }

        if (card.Length != CardSize || card.Any(x => x.Length != CardSize))
        {
            throw new InvalidOperationException("Bingo card must be 5x5");
        }

        Card = card;
    }

    private BingoNumber[][] Card { get; }

    public void Daub(ReadOnlySpan<int> ballCall)
    {
        for (var row = 0; row < CardSize; ++row)
        {
            for (var col = 0; col < CardSize; ++col)
            {
                var bingoNumber = Card[row][col];
                if (bingoNumber.Daubed)
                {
                    continue;
                }

                foreach (var ball in ballCall)
                {
                    if (bingoNumber.Number == ball)
                    {
                        Card[row][col] = bingoNumber with { Daubed = true };
                    }
                }
            }
        }
    }

    public bool HasWin()
    {
        for (var row = 0; row < CardSize; ++row)
        {
            int daubedRow = 0;
            int daubedCol = 0;
            for (var col = 0; col < CardSize; ++col)
            {
                var rowNumber = Card[row][col];
                var colNumber = Card[col][row];
                if (!rowNumber.Daubed && !colNumber.Daubed)
                {
                    break;
                }

                if (rowNumber.Daubed)
                {
                    ++daubedCol;
                }

                if (colNumber.Daubed)
                {
                    ++daubedRow;
                }
            }

            if (daubedRow == CardSize)
            {
                return true;
            }

            if (daubedCol == CardSize)
            {
                return true;
            }
        }

        return false;
    }

    public IEnumerable<int> UnDaubed()
    {
        for (var row = 0; row < CardSize; ++row)
        {
            for (var col = 0; col < CardSize; ++col)
            {
                var bingoNumber = Card[row][col];
                if (bingoNumber.Daubed)
                {
                    continue;
                }

                yield return bingoNumber.Number;
            }
        }
    }
}
