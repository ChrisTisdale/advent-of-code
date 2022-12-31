namespace AdventOfCode2022.day7;

using System.Text;

public record DirectoryInfo(string FullName, ISet<IContainingItem> ContainingItems) : IContainingItem
{
    public string Name => FullName == "/" ? FullName : FullName[(FullName.LastIndexOf('/') + 1)..];

    public long Size => ContainingItems.Sum(x => x.Size);

    public virtual bool Equals(DirectoryInfo? other) =>
        !ReferenceEquals(null, other) &&
        (ReferenceEquals(this, other) || FullName == other.FullName && ContainingItems.SequenceEqual(other.ContainingItems));

    public override int GetHashCode() =>
        HashCode.Combine(Name, ContainingItems.Aggregate(typeof(int).GetHashCode(), HashCode.Combine));

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        const string separator = ", ";
        const string equals = " = ";
        const string arrayStart = "[ ";
        const string arrayEnd = " ]";

        builder.Append(nameof(FullName)).Append(equals).Append(FullName).Append(separator);
        builder.Append(nameof(Name)).Append(equals).Append(Name).Append(separator);
        builder.Append(nameof(Size)).Append(equals).Append(Size).Append(separator);
        builder.Append(nameof(ContainingItems))
            .Append(equals)
            .Append(arrayStart)
            .Append(string.Join(separator, ContainingItems))
            .Append(arrayEnd);

        return true;
    }
}
