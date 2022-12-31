namespace AdventOfCode2022Tests.day12;

using AdventOfCode2022.day12;

public class Day12Tests
{
    private readonly Day12 _target;

    public Day12Tests()
    {
        _target = new Day12();
    }

    [Fact]
    public void YearTest()
    {
        var date = _target.Year;
        date.Should().Be(Constants.ExpectedYear);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileLocation("sample.txt"));
        part1Result.Should().Be(31);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileLocation("sample.txt"));
        part1Result.Should().Be(29);
    }

    [Fact(Skip = "Test is too long")]
    public async Task Measurements_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileLocation("measurements.txt"));
        part1Result.Should().Be(425);
    }

    [Fact(Skip = "Test is too long")]
    public async Task Measurements_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileLocation("measurements.txt"));
        part1Result.Should().Be(418);
    }
}
