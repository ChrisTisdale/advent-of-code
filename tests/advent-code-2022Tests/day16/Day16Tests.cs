namespace AdventOfCode2022Tests.day16;

using AdventOfCode2022.day16;

public class Day16Tests
{
    private readonly Day16 _target;

    public Day16Tests()
    {
        _target = new Day16();
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_1_Matches()
    {
        await Part_1_Matches("sample.txt", 1651L);
    }

    [Fact()]
    public async Task Measurement_Part_1_Matches()
    {
        await Part_1_Matches("measurements.txt", 6078701L);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_2_Matches()
    {
        await Part_2_Matches("sample.txt", 56000011L);
    }

    [Fact()]
    public async Task Measurement_Part_2_Matches()
    {
        await Part_2_Matches("measurements.txt", 12567351400528L);
    }

    private async Task Part_1_Matches(string fileName, long expectedResult)
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileLocation(fileName));
        part1Result.Should().Be(expectedResult);
    }

    private async Task Part_2_Matches(string fileName, long expectedResult)
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileLocation(fileName));
        part1Result.Should().Be(expectedResult);
    }
}
