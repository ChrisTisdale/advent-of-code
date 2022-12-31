namespace AdventOfCode2022Tests.day7;

using AdventOfCode2022.day7;

public class Day7Tests
{
    private readonly Day7 _target;

    public Day7Tests()
    {
        _target = new Day7();
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
        part1Result.Should().Be(95437L);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileLocation("sample.txt"));
        part1Result.Should().Be(24933642L);
    }

    [Fact(Timeout = 2000)]
    public async Task Measurements_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileLocation("measurements.txt"));
        part1Result.Should().Be(1490523L);
    }

    [Fact(Timeout = 2000)]
    public async Task Measurements_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileLocation("measurements.txt"));
        part1Result.Should().Be(12390492L);
    }
}
