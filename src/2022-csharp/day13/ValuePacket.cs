namespace AdventOfCode2022.day13;

internal record ValuePacket(int Value) : IPacket
{
    public override string ToString()
    {
        return Value.ToString();
    }
}