namespace AdventOfCode2023.day8;

public enum Move
{
    Left,
    Right
}

public record Leaf(string Key, string Left, string Right);

public record Tree(IDictionary<string, Leaf> Leaves, IReadOnlyList<Move> Moves);
