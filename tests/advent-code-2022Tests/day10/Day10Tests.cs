namespace AdventOfCode2022Tests.day10;

using AdventOfCode2022.day10;

public class Day10Tests
{
    private readonly Day10 _target;

    public Day10Tests()
    {
        _target = new Day10();
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
        part1Result.Should().Be(13140);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileLocation("sample.txt"));
        part1Result.Should().Be(13140);
    }

    [Fact(Timeout = 2000)]
    public async Task Measurements_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileLocation("measurements.txt"));
        part1Result.Should().Be(13760);
    }

    [Fact(Timeout = 2000)]
    public async Task Measurements_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileLocation("measurements.txt"));
        part1Result.Should().Be(13760);
    }
}
