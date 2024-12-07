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

namespace CommonTests;

using Common;

public class PointTests
{
    [Theory]
    [InlineData(1, 1, 2, 2, 2)]
    [InlineData(2, 2, 1, 1, 2)]
    [InlineData(1, 1, 1, 2, 1)]
    [InlineData(1, 1, 2, 1, 1)]
    [InlineData(1, 1, 1, 1, 0)]
    public void ManhattanDistanceStaticMethodTest(int x1, int y1, int x2, int y2, int expectedDistance)
    {
        var p1 = new Point<int>(x1, y1);
        var p2 = new Point<int>(x2, y2);
        Point<int>.ManhattanDistance(p1, p2).Should().Be(expectedDistance);
    }

    [Theory]
    [InlineData(1L, 1L, 2L, 2L, 2L)]
    [InlineData(2L, 2L, 1L, 1L, 2L)]
    [InlineData(1L, 1L, 1L, 2L, 1L)]
    [InlineData(1L, 1L, 2L, 1L, 1L)]
    [InlineData(1L, 1L, 1L, 1L, 0L)]
    public void ManhattanDistanceMethodTest(long x1, long y1, long x2, long y2, long expectedDistance)
    {
        var p1 = new Point<long>(x1, y1);
        var p2 = new Point<long>(x2, y2);
        p1.ManhattanDistance(p2).Should().Be(expectedDistance);
    }

    [Fact]
    public void DeconstructTest()
    {
        const double expectedX = 0.2;
        const double expectedY = 0.3;
        var p = new Point<double>(expectedX, expectedY);
        var (x, y) = p;
        x.Should().Be(expectedX);
        y.Should().Be(expectedY);
    }

    [Theory]
    [InlineData(1d, 1d, 1d, 1d, true)]
    [InlineData(0.1d, 0.2d, 0.1d, 0.2d, true)]
    [InlineData(0.1d, 0.2d, 0.2d, 0.1d, false)]
    public void EqualsTest(double x1, double y1, double x2, double y2, bool expectedResult)
    {
        var p1 = new Point<double>
        {
            X = x1,
            Y = y1
        };

        var p2 = new Point<double>
        {
            X = x2,
            Y = y2
        };

        p1.Equals(p2).Should().Be(expectedResult);
        p2.Equals(p1).Should().Be(expectedResult);
        (p1 == p2).Should().Be(expectedResult);
    }
}
