namespace Common;

using System.Runtime.CompilerServices;

public abstract class BaseAdventOfCodeDay<T> : IAdventOfCodeDay
{
    private readonly IReadOnlyDictionary<Part, string[]> _files;

    protected BaseAdventOfCodeDay()
        : this(Constants.DefaultFiles)
    {
    }

    protected BaseAdventOfCodeDay(IReadOnlyDictionary<Part, string[]> files)
    {
        _files = files ?? throw new ArgumentNullException(nameof(files));
    }

    public abstract DateOnly Year { get; }

    public virtual async ValueTask ExecutePart1(CancellationToken token = default)
    {
        foreach (var file in _files[Part.Part1])
        {
            await using var fileStream = GetFileStream(file);
            var results = await ExecutePart1(fileStream, token);
            Console.WriteLine($"{file} Has the answer: {results}");
        }
    }

    public virtual async ValueTask ExecutePart2(CancellationToken token = default)
    {
        foreach (var file in _files[Part.Part2])
        {
            await using var fileStream = GetFileStream(file);
            var results = await ExecutePart2(fileStream, token);
            Console.WriteLine($"{file} Has the answer: {results}");
        }
    }

    public abstract ValueTask<T> ExecutePart1(Stream stream, CancellationToken token = default);

    public abstract ValueTask<T> ExecutePart2(Stream stream, CancellationToken token = default);

    public Stream GetFileStream(string file)
    {
        var type = GetType();
        var ns = type.Namespace ?? throw new InvalidOperationException();
        return type.Assembly.GetManifestResourceStream($"{ns}.{file}") ?? throw new InvalidOperationException();
    }

    protected static async ValueTask<IReadOnlyList<string>> ReadAllLinesAsync(Stream stream, CancellationToken token = default)
    {
        var lines = new List<string>();
        using var sr = new StreamReader(stream);
        while (!sr.EndOfStream)
        {
            var line = await sr.ReadLineAsync(token) ?? string.Empty;
            lines.Add(line);
        }

        return lines;
    }

    protected static async IAsyncEnumerable<string> EnumerateLinesAsync(
        Stream stream,
        [EnumeratorCancellation] CancellationToken token = default)
    {
        using var sr = new StreamReader(stream);
        while (!sr.EndOfStream)
        {
            yield return await sr.ReadLineAsync(token) ?? string.Empty;
        }
    }
}
