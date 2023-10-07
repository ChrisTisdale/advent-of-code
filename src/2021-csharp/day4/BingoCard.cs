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

    public ReadOnlySpan<BingoNumber> this[int row] => Card[row];

    public BingoNumber GetNumber(int row, int col) => Card[row][col];

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
