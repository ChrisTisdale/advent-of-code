namespace AdventOfCode2023.day7;

using Common;

public class Day72023 : BaseAdventOfCodeDay<long>
{
    public override DateOnly Year => new(2023, 12, 7);

    public override async ValueTask<long> ExecutePart1(Stream stream)
    {
        var hands = await ParseInput(stream, false);
        hands.Sort(new HandComparer());
        var count = 0L;
        for (var i = 0L; i < hands.Count; ++i)
        {
            count += (i + 1) * hands[(int)i].Bid;
        }

        return count;
    }

    public override async ValueTask<long> ExecutePart2(Stream stream)
    {
        var hands = await ParseInput(stream, true);
        hands.Sort(new HandComparer());
        var count = 0L;
        for (var i = 0L; i < hands.Count; ++i)
        {
            count += (i + 1) * hands[(int)i].Bid;
        }

        return count;
    }

    private static async ValueTask<List<Hand>> ParseInput(Stream stream, bool withJoker)
    {
        var hands = new List<Hand>();
        using var sr = new StreamReader(stream);
        while (!sr.EndOfStream)
        {
            var line = await sr.ReadLineAsync();
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
