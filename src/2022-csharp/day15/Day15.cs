namespace AdventOfCode2022.day15;

using Common;

public class Day15 : Base2022<long>
{
    public override async ValueTask<long> ExecutePart1(string fileName)
    {
        var (sensors, points) = await GetSensors(fileName);
        var expectedY = Path.GetFileName(fileName) == Constants.DefaultFiles[0] ? 10 : 2000000;
        var set = new HashSet<Point<int>>(10000000);
        foreach (var sensor in sensors)
        {
            foreach (var beacon in GetPointsWithoutBeacons(sensor, points, expectedY))
            {
                set.Add(beacon);
            }
        }

        return set.Count;
    }

    public override async ValueTask<long> ExecutePart2(string fileName)
    {
        const long xMultiple = 4000000;
        var (sensors, points) = await GetSensors(fileName);
        var location = GetLocation(sensors, points);
        checked
        {
            return location.X * xMultiple + location.Y;
        }
    }

    private static async ValueTask<(IReadOnlyList<Sensor>, HashSet<Point<int>>)> GetSensors(string fileName)
    {
        var sensors = new List<Sensor>();
        var points = new HashSet<Point<int>>();
        await foreach (var line in File.ReadLinesAsync(fileName))
        {
            var dataPoints = line.Split(':');
            var sensorData = dataPoints[0].Split('=', ',');
            var sensorPoint = new Point<int>(int.Parse(sensorData[1]), int.Parse(sensorData[3]));
            var closestData = dataPoints[1].Split('=', ',');
            var closestPoint = new Point<int>(int.Parse(closestData[1]), int.Parse(closestData[3]));
            sensors.Add(new Sensor(sensorPoint, closestPoint));
            points.Add(closestPoint);
            points.Add(sensorPoint);
        }

        return (sensors, points);
    }

    private static Point<int> GetLocation(IReadOnlyList<Sensor> sensors, IReadOnlySet<Point<int>> dataPoints)
    {
        var maxX = sensors.Select(x => x.Location.X).Max() - 1;
        var maxY = sensors.Select(x => x.Location.Y).Max() - 1;
        const int maxValue = 4000000;
        const int minValue = 0;
        for (var x = minValue; x < Math.Min(maxValue, maxX); ++x)
        {
            var y = 0;
            while (y < Math.Min(maxValue, maxY))
            {
                var point = new Point<int>(x, y);
                if (dataPoints.Contains(point))
                {
                    ++y;
                    continue;
                }

                var amount = BeaconCanNotExist(sensors, in point);
                if (amount < 0)
                {
                    return point;
                }

                y += amount + 1;
            }
        }

        throw new InvalidDataException();
    }

    private static int BeaconCanNotExist(IReadOnlyList<Sensor> sensors, in Point<int> checkPoint)
    {
        var maxMove = -1;
        for (var index = 0; index < sensors.Count; index++)
        {
            var t = sensors[index];
            var amount = BeaconCanNotExist(t, in checkPoint);
            if (amount > maxMove)
            {
                maxMove = amount;
            }
        }

        return maxMove;
    }

    private static int BeaconCanNotExist(Sensor sensor, in Point<int> checkPoint)
    {
        var location = sensor.Location;
        var distance = sensor.Distance();
        return distance - location.ManhattanDistance(in checkPoint);
    }

    private static IEnumerable<Point<int>> GetPointsWithoutBeacons(
        Sensor sensor,
        IReadOnlySet<Point<int>> dataPoints,
        int expectedY)
    {
        var location = sensor.Location;
        var distance = sensor.Distance();
        if (location.Y + distance < expectedY || location.Y - distance > expectedY)
        {
            yield break;
        }

        var yOffset = Math.Abs(Math.Abs(location.Y) - Math.Abs(expectedY));
        for (var x = 0; x <= distance - yOffset; ++x)
        {
            var p1 = new Point<int>(location.X + x, expectedY);
            if (!dataPoints.Contains(p1))
            {
                yield return p1;
            }

            if (x is 0)
            {
                continue;
            }

            var p2 = new Point<int>(location.X - x, expectedY);
            if (!dataPoints.Contains(p2))
            {
                yield return p2;
            }
        }
    }
}
