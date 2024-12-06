namespace AdventOfCode2022.day6;

using System.Text;

public record Results(IReadOnlyList<int> MarkerLocations)
{
    public Results(params int[] markerLocations)
        : this(new List<int>(markerLocations))
    {
    }

    public virtual bool Equals(Results? other) =>
        !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || MarkerLocations.SequenceEqual(other.MarkerLocations));

    public override int GetHashCode() => MarkerLocations.Aggregate(typeof(int).GetHashCode(), HashCode.Combine);

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        const string separator = ", ";
        const string equals = " = ";
        const string arrayStart = "[ ";
        const string arrayEnd = " ]";
        builder.Append(nameof(MarkerLocations))
            .Append(equals)
            .Append(arrayStart)
            .Append(string.Join(separator, MarkerLocations))
            .Append(arrayEnd);
        return true;
    }
}
