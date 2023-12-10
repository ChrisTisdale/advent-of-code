namespace AdventOfCode2023.day7;

public static class CardNumberExtensions
{
    public static CardNumber ToCardNumber(this char value, bool useJoker) =>
        value switch
        {
            '2' => CardNumber.Two,
            '3' => CardNumber.Three,
            '4' => CardNumber.Four,
            '5' => CardNumber.Five,
            '6' => CardNumber.Six,
            '7' => CardNumber.Seven,
            '8' => CardNumber.Eight,
            '9' => CardNumber.Nine,
            'T' => CardNumber.Ten,
            'J' => useJoker ? CardNumber.Joker : CardNumber.Jack,
            'Q' => CardNumber.Queen,
            'K' => CardNumber.King,
            _ => CardNumber.Ace
        };

    public static HandScore CalculateScore(this Hand left)
    {
        var leftJokers = left.Numbers.Count(x => x == CardNumber.Joker);
        var leftSort = left.Numbers
            .Where(x => x != CardNumber.Joker)
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());
        if (leftSort.Count is 1 or 0)
        {
            return HandScore.FiveOfAKind;
        }

        var leftMax = leftSort.Count > 0
            ? leftSort.MaxBy(x => x.Value)
            : new KeyValuePair<CardNumber, int>(CardNumber.Joker, leftJokers);
        if (leftMax.Value + leftJokers == 4)
        {
            return HandScore.FourOfAKind;
        }

        if (leftSort.Keys.Count == 2)
        {
            return HandScore.FullHouse;
        }

        if (leftMax.Value + leftJokers == 3)
        {
            return HandScore.ThreeOfAKind;
        }

        var leftPairs = leftSort.Count(x => x.Value == 2) + leftJokers;
        return leftPairs switch
        {
            2 => HandScore.TwoPair,
            1 => HandScore.OnePair,
            _ => HandScore.HighCard
        };
    }
}
