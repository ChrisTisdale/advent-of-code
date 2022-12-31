namespace AdventOfCode2022Tests.day15;

using AdventOfCode2022.day15;

public class Day15Tests
{
    private readonly Day15 _target;

    public Day15Tests()
    {
        _target = new Day15();
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
        await Part_1_Matches("sample.txt", 26L);
    }

    [Fact(Skip = "Test is too long")]
    public async Task Measurement_Part_1_Matches()
    {
        await Part_1_Matches("measurements.txt", 6078701L);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_2_Matches()
    {
        await Part_2_Matches("sample.txt", 56000011L);
    }

    [Fact(Skip = "Test is too long")]
    public async Task Measurement_Part_2_Matches()
    {
        await Part_2_Matches("measurements.txt", 12567351400528L);
    }

    private async Task Part_1_Matches(string fileName, long expectedResult)
    {
        var part1Result = await _target.ExecutePart1(fileName, _target.GetFileStream(fileName));
        part1Result.Should().Be(expectedResult);
    }

    private async Task Part_2_Matches(string fileName, long expectedResult)
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileStream(fileName));
        part1Result.Should().Be(expectedResult);
    }
}
