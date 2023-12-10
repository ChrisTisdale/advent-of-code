namespace AdventOfCode2023Tests.day7;

using System.Collections;
using AdventOfCode2023.day7;

public class HandScoreTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new Hand(new[] { CardNumber.Joker, CardNumber.Joker, CardNumber.Joker, CardNumber.Joker, CardNumber.Joker }, 0),
            HandScore.FiveOfAKind
        };
        yield return new object[]
        {
            new Hand(new[] { CardNumber.Ace, CardNumber.Ace, CardNumber.Ace, CardNumber.Ace, CardNumber.Ace }, 0),
            HandScore.FiveOfAKind
        };
        yield return new object[]
        {
            new Hand(new[] { CardNumber.Joker, CardNumber.Ace, CardNumber.Ace, CardNumber.Ace, CardNumber.Ace }, 0),
            HandScore.FiveOfAKind
        };
        yield return new object[]
        {
            new Hand(new[] { CardNumber.Ace, CardNumber.King, CardNumber.King, CardNumber.Ace, CardNumber.Ace }, 0),
            HandScore.FullHouse
        };
        yield return new object[]
        {
            new Hand(new[] { CardNumber.King, CardNumber.Joker, CardNumber.Ace, CardNumber.Ace, CardNumber.Ace }, 0),
            HandScore.FourOfAKind
        };
        yield return new object[]
        {
            new Hand(new[] { CardNumber.King, CardNumber.Joker, CardNumber.Joker, CardNumber.Ace, CardNumber.Ace }, 0),
            HandScore.FourOfAKind
        };
        yield return new object[]
        {
            new Hand(new[] { CardNumber.King, CardNumber.Joker, CardNumber.Joker, CardNumber.Queen, CardNumber.Ace }, 0),
            HandScore.ThreeOfAKind
        };
        yield return new object[]
        {
            new Hand(new[] { CardNumber.King, CardNumber.Joker, CardNumber.Queen, CardNumber.Ace, CardNumber.Ace }, 0),
            HandScore.ThreeOfAKind
        };
        yield return new object[]
        {
            new Hand(new[] { CardNumber.King, CardNumber.King, CardNumber.Ace, CardNumber.Two, CardNumber.Ace }, 0),
            HandScore.TwoPair
        };
        yield return new object[]
        {
            new Hand(new[] { CardNumber.King, CardNumber.Joker, CardNumber.Queen, CardNumber.Ace, CardNumber.Three }, 0),
            HandScore.OnePair
        };
        yield return new object[]
        {
            new Hand(new[] { CardNumber.King, CardNumber.Three, CardNumber.Two, CardNumber.Ace, CardNumber.Ace }, 0),
            HandScore.OnePair
        };
        yield return new object[]
        {
            new Hand(new[] { CardNumber.King, CardNumber.Three, CardNumber.Four, CardNumber.Ace, CardNumber.Queen }, 0),
            HandScore.HighCard
        };
        yield return new object[]
        {
            new Hand("T55J5".Select(x => x.ToCardNumber(true)).ToList(), 0),
            HandScore.FourOfAKind
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
