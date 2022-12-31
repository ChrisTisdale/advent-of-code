namespace Common;

public abstract class BaseAdventOfCodeDay<T> : IAdventOfCodeDay
{
    private readonly IReadOnlyDictionary<Part, string[]> _files;

    protected BaseAdventOfCodeDay()
        : this(Constants.DefaultFiles)
    {
    }

    protected BaseAdventOfCodeDay(IReadOnlyDictionary<Part, string[]> files)
    {
        _files = files;
    }

    public abstract DateOnly Year { get; }

    public virtual async ValueTask ExecutePart1()
    {
        foreach (var file in _files[Part.Part1])
        {
            var results = await ExecutePart1(GetFileLocation(file));
            Console.WriteLine($"{file} Has the answer: {results}");
        }
    }

    public virtual async ValueTask ExecutePart2()
    {
        foreach (var file in _files[Part.Part2])
        {
            var results = await ExecutePart2(GetFileLocation(file));
            Console.WriteLine($"{file} Has the answer: {results}");
        }
    }

    public abstract ValueTask<T> ExecutePart1(string fileName);

    public abstract ValueTask<T> ExecutePart2(string fileName);

    public string GetFileLocation(string file)
    {
        var ns = GetType().Namespace ?? throw new InvalidOperationException();
        return Path.Combine(ns.Split('.').Last(), file);
    }
}
