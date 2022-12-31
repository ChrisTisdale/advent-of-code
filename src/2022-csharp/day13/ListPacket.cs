namespace AdventOfCode2022.day13;

using System.Text;

internal record ListPacket(IReadOnlyList<IPacket> Values) : IPacket
{
    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append(nameof(Values)).Append(" = ").Append(" [ ").Append(string.Join(",", Values)).Append(" ]");
        return true;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append('[').Append(string.Join(",", Values)).Append(']');
        return sb.ToString();
    }
}
