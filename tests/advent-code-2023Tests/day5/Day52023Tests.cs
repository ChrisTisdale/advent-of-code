namespace AdventOfCode2023Tests.day5;

using AdventOfCode2023.day5;

public class Day52023Tests
{
    private readonly Day52023 _target = new();

    [Fact]
    public void YearTest()
    {
        _target.Year.Should().Be(new DateOnly(2023, 12, 5));
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileStream("sample.txt"));
        part1Result.Should().Be(35L);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileStream("sample.txt"));
        part1Result.Should().Be(46L);
    }

    [Fact(Timeout = 2000)]
    public async Task Measurements_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileStream("measurements.txt"));
        part1Result.Should().Be(836040384L);
    }

    [Fact(Skip = "Test is too long")]
    public async Task Measurements_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileStream("measurements.txt"));
        part1Result.Should().Be(10834440L);
    }
}
