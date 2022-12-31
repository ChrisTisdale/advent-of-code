namespace AdventOfCode2022.day13;

using System.Collections;

internal record PacketChecker(IPacket Left, IPacket Right) : IEnumerable<IPacket>
{
    public IEnumerator<IPacket> GetEnumerator()
    {
        yield return Left;
        yield return Right;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
