namespace AdventOfCode2023Tests.day6;

using AdventOfCode2023.day6;

public class Day62023Tests
{
    private readonly Day62023 _target = new();

    [Fact]
    public void YearTest()
    {
        _target.Year.Should().Be(new DateOnly(2023, 12, 6));
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileStream("sample.txt"));
        part1Result.Should().Be(288L);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileStream("sample.txt"));
        part1Result.Should().Be(71503L);
    }

    [Fact(Timeout = 2000)]
    public async Task Measurements_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileStream("measurements.txt"));
        part1Result.Should().Be(1731600L);
    }

    [Fact(Timeout = 3000)]
    public async Task Measurements_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileStream("measurements.txt"));
        part1Result.Should().Be(40087680L);
    }
}
