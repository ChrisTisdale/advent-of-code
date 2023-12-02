namespace AdventOfCode2023Tests.day1;

using AdventOfCode2023.day1;

public class Day12023Tests
{
    private readonly Day12023 _target = new();

    [Fact]
    public void YearTest()
    {
        _target.Year.Should().Be(new DateOnly(2023, 12, 1));
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileStream("samplePart1.txt"));
        part1Result.Should().Be(142);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileStream("samplePart2.txt"));
        part1Result.Should().Be(281);
    }

    [Fact(Timeout = 2000)]
    public async Task Measurements_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileStream("measurements.txt"));
        part1Result.Should().Be(55090);
    }

    [Fact(Timeout = 2000)]
    public async Task Measurements_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileStream("measurements.txt"));
        part1Result.Should().Be(54845);
    }
}
