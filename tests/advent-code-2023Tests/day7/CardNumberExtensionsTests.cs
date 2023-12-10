namespace AdventOfCode2023Tests.day7;

using AdventOfCode2023.day7;

public class CardNumberExtensionsTests
{
    [Theory]
    [ClassData(typeof(HandScoreTestData))]
    public void HandScoreTest(Hand hand, HandScore expectedScore)
    {
        hand.CalculateScore().Should().Be(expectedScore);
    }
}
