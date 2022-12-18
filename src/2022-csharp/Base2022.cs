namespace AdventOfCode2022;

using Common;

public abstract class Base2022<T> : IAdventOfCodeDay
{
    private static readonly string[] DefaultFiles = { "sample.txt", "measurements.txt" };

    public DateOnly Year => new(2022, 12, 1);

    private readonly string[] _files;

    protected Base2022() : this(DefaultFiles)
    {
    }

    protected Base2022(string[] files)
    {
        _files = files;
    }

    public async ValueTask ExecutePart1()
    {
        foreach (var file in _files)
        {
            var results = await ExecutePart1(GetFileLocation(file));
            Console.WriteLine($"{file} Has the answer: {results}");
        }
    }

    public async ValueTask ExecutePart2()
    {
        foreach (var file in _files)
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
