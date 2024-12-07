// Copyright (c) Christopher Tisdale 2024.
//
// Licensed under BSD-3-Clause.
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      https://spdx.org/licenses/BSD-3-Clause.html
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
