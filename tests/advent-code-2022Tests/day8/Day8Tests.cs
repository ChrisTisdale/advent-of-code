namespace AdventOfCode2022Tests.day8;

using AdventOfCode2022.day8;

public class Day8Tests
{
    private readonly Day8 _target;

    public Day8Tests()
    {
        _target = new Day8();
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
        var expectedScore = new TreeScore(21, 8);
        var part1Result = await _target.ExecutePart1(_target.GetFileLocation("sample.txt"));
        part1Result.Should().Be(expectedScore.VisibleTrees);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_2_Matches()
    {
        var expectedScore = new TreeScore(21, 8);
        var part1Result = await _target.ExecutePart2(_target.GetFileLocation("sample.txt"));
        part1Result.Should().Be(expectedScore.BestScore);
    }

    [Fact(Timeout = 2000)]
    public async Task Measurements_Part_1_Matches()
    {
        var expectedScore = new TreeScore(1719, 590824);
        var part1Result = await _target.ExecutePart1(_target.GetFileLocation("measurements.txt"));
        part1Result.Should().Be(expectedScore.VisibleTrees);
    }

    [Fact(Timeout = 2000)]
    public async Task Measurements_Part_2_Matches()
    {
        var expectedScore = new TreeScore(1719, 590824);
        var part1Result = await _target.ExecutePart2(_target.GetFileLocation("measurements.txt"));
        part1Result.Should().Be(expectedScore.BestScore);
    }
}
