namespace AdventOfCode2022Tests.day14;

using AdventOfCode2022.day14;

public class Day14Tests
{
    private readonly Day14 _target;

    public Day14Tests()
    {
        _target = new Day14();
    }

    [Fact(Timeout = 2000)]
    public async Task Sample_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileLocation("sample.txt"));
        part1Result.Should().Be(24);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileLocation("sample.txt"));
        part1Result.Should().Be(93);
    }

    [Fact(Skip = "Test is too long")]
    public async Task Measurements_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileLocation("measurements.txt"));
        part1Result.Should().Be(665);
    }

    [Fact(Skip = "Test is too long")]
    public async Task Measurements_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileLocation("measurements.txt"));
        part1Result.Should().Be(25434);
    }
}
