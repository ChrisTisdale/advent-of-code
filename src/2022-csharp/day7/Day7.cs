namespace AdventOfCode2022.day7;

public class Day7 : Base2022AdventOfCodeDay<long>
{
    public override async ValueTask<long> ExecutePart1(string fileName)
    {
        var (directories, _) = await GetDirectories(fileName);
        var result = await GetSumOfDirectoriesAtMostSize(directories, 100000);
        return result;
    }

    public override async ValueTask<long> ExecutePart2(string fileName)
    {
        var (directories, root) = await GetDirectories(fileName);
        var result = await FindBestDeletionSize(directories, root, 70000000, 30000000);
        return result;
    }

    private static ValueTask<long> FindBestDeletionSize(
        IEnumerable<DirectoryInfo> directories,
        IContainingItem root,
        long totalSize,
        long freeSize)
    {
        var rootSize = root.Size;
        var remainingSize = totalSize - rootSize;
        var needingSize = freeSize - remainingSize;
        var directoryInfo = directories.Where(x => x.Size >= needingSize).OrderBy(x => x.Size).First();
        return new ValueTask<long>(directoryInfo.Size);
    }

    private static ValueTask<long> GetSumOfDirectoriesAtMostSize(IEnumerable<DirectoryInfo> directories, long size) =>
        ValueTask.FromResult(directories.Where(x => x.Size <= size).Sum(x => x.Size));

    private static async ValueTask<(IReadOnlyList<DirectoryInfo> directories, DirectoryInfo root)> GetDirectories(string filename)
    {
        var folders = new Dictionary<string, DirectoryInfo>();

        var currentDir = "";
        await foreach (var line in File.ReadLinesAsync(filename))
        {
            if (line.StartsWith("$"))
            {
                currentDir = HandleCommand(line, currentDir, folders);
            }
            else
            {
                ProcessListDirectory(line, currentDir, folders);
            }
        }

        return (folders.Select(k => k.Value).ToArray(), folders["/"]);
    }

    private static void ProcessListDirectory(string line, string currentDir, Dictionary<string, DirectoryInfo> folders)
    {
        var inputs = line.Split(' ');
        switch (inputs[0])
        {
            case "dir":
                var directory = GetCurrentDir(currentDir, inputs[1]);
                if (!folders.ContainsKey(directory))
                {
                    folders.TryAdd(directory, new DirectoryInfo(directory, new HashSet<IContainingItem>()));
                }

                folders[currentDir].ContainingItems.Add(folders[directory]);
                break;
            default:
                var size = long.Parse(inputs[0]);
                folders[currentDir].ContainingItems.Add(new FileInfo(inputs[1], size));
                break;
        }
    }

    private static string HandleCommand(string line, string currentDir, IDictionary<string, DirectoryInfo> folders)
    {
        var inputs = line.Split(' ');
        return inputs[1] switch
        {
            "cd" => HandleChangeDirectory(currentDir, folders, inputs),
            "ls" => currentDir,
            _ => throw new InvalidOperationException()
        };
    }

    private static string HandleChangeDirectory(string currentDir, IDictionary<string, DirectoryInfo> folders, IReadOnlyList<string> inputs)
    {
        switch (inputs[2])
        {
            case "..":
                var indexOf = currentDir.LastIndexOf('/');
                return currentDir[..indexOf];
            default:
                currentDir = GetCurrentDir(currentDir, inputs[2]);
                if (!folders.ContainsKey(currentDir))
                {
                    folders.Add(currentDir, new DirectoryInfo(currentDir, new HashSet<IContainingItem>()));
                }

                return currentDir;
        }
    }

    private static string GetCurrentDir(string currentDir, string name)
    {
        if (!currentDir.EndsWith("/") && name != "/")
        {
            return string.Concat(currentDir, "/", name);
        }

        return string.Concat(currentDir, name);
    }
}
