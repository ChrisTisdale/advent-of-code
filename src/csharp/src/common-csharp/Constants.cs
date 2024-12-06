namespace Common;

public static class Constants
{
    public static readonly IReadOnlyDictionary<Part, string[]> DefaultFiles = new Dictionary<Part, string[]>
    {
        { Part.Part1, new[] { "sample.txt", "measurements.txt" } },
        { Part.Part2, new[] { "sample.txt", "measurements.txt" } }
    }.AsReadOnly();
}
