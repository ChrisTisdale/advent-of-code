namespace AdventOfCode2022.day15;

using Common;

public class Sensor
{
    private readonly Point<int> _closestBeacon;
    private readonly Point<int> _location;

    public Sensor(Point<int> location, Point<int> closestBeacon)
    {
        _location = location;
        _closestBeacon = closestBeacon;
    }

    public Point<int> Location => _location;

    public Point<int> ClosestBeacon => _closestBeacon;

    public int Distance() => Point<int>.ManhattanDistance(in _location, in _closestBeacon);
}
