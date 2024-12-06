namespace AdventOfCode2023Tests.day8;

using AdventOfCode2023.day8;

public class Day82023Tests
{
    private readonly Day82023 _target = new();

    [Fact]
    public void YearTest()
    {
        _target.Year.Should().Be(new DateOnly(2023, 12, 8));
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileStream("samplePart1.txt"));
        part1Result.Should().Be(6L);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileStream("samplePart2.txt"));
        part1Result.Should().Be(6L);
    }

    [Fact(Timeout = 2000)]
    public async Task Measurements_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileStream("measurements.txt"));
        part1Result.Should().Be(13301L);
    }

    [Fact(Timeout = 3000)]
    public async Task Measurements_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileStream("measurements.txt"));
        part1Result.Should().Be(7309459565207L);
    }
}
