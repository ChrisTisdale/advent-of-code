namespace AdventOfCode2023Tests.day1;

using AdventOfCode2023.day1;

public class Day12023Tests
{
    private readonly Day12023 _target = new();

    [Fact]
    public void YearTest()
    {
        _target.Year.Should().Be(Constants.ExpectedYear);
    }
}
