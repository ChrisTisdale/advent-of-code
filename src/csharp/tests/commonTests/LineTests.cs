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

using System.Diagnostics;
using Common;

public class LineTests
{
    [Theory]
    [InlineData(0, 0, 1, 0, 2)]
    [InlineData(0, 0, 0, 1, 2)]
    [InlineData(1, 0, 0, 0, 2)]
    [InlineData(0, 1, 0, 0, 2)]
    [Conditional("DEBUG")]
    public void InvalidIncrementLineTest(int x1, int y1, int x2, int y2, int increment)
    {
        Assert.Throws<InvalidDataException>(() => new Line<int>(new Point<int>(x1, y1), new Point<int>(x2, y2), increment));
    }

    [Theory]
    [InlineData(1, 1, 1, 4, 1, 2, true)]
    [InlineData(1, 1, 4, 1, 2, 1, true)]
    [InlineData(1, 1, 4, 1, 1, 1, true)]
    [InlineData(1, 1, 4, 1, 4, 1, true)]
    [InlineData(1, 1, 1, 4, 2, 2, false)]
    [InlineData(1, 1, 4, 1, 2, 2, false)]
    public void PointIsInLineIntTest(int x1, int y1, int x2, int y2, int checkX, int checkY, bool expectedResult)
    {
        var p1 = new Point<int>(x1, y1);
        var p2 = new Point<int>(x2, y2);
        var checkPoint = new Point<int>(checkX, checkY);
        var line = new Line<int>(p1, p2);
        var result = line.IsInLine(checkPoint);
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void DeconstructWithoutIncrementTest()
    {
        const decimal x1 = 0.1M;
        const decimal x2 = 0.3M;
        const decimal y1 = 0.4M;
        const decimal y2 = 0.8M;
        var line = new Line<decimal>(new Point<decimal>(x1, y1), new Point<decimal>(x2, y2), 0.1M);
        var (left, right) = line;
        Assert.Equal(x1, left.X);
        Assert.Equal(y1, left.Y);
        Assert.Equal(x2, right.X);
        Assert.Equal(y2, right.Y);
    }

    [Fact]
    public void DeconstructWithIncrementTest()
    {
        const decimal x1 = 0.1M;
        const decimal x2 = 0.3M;
        const decimal y1 = 0.4M;
        const decimal y2 = 0.8M;
        const decimal expectedIncrement = 0.001M;
        var line = new Line<decimal>(new Point<decimal>(x1, y1), new Point<decimal>(x2, y2), expectedIncrement);
        var (left, right, increment) = line;
        Assert.Equal(x1, left.X);
        Assert.Equal(y1, left.Y);
        Assert.Equal(x2, right.X);
        Assert.Equal(y2, right.Y);
        Assert.Equal(expectedIncrement, increment);
    }

    [Theory]
    [InlineData(1, 1, 1, 1, 1, 1, 1, 1, true)]
    [InlineData(1, 2, 1, 2, 1, 2, 1, 2, true)]
    [InlineData(1, 2, 2, 1, 2, 1, 1, 2, false)]
    public void EqualsTest(
        int x1,
        int y1,
        int x2,
        int y2,
        int x3,
        int y3,
        int x4,
        int y4,
        bool expectedResult)
    {
        var p1 = new Point<int>(x1, y1);
        var p2 = new Point<int>(x2, y2);
        var p3 = new Point<int>(x3, y3);
        var p4 = new Point<int>(x4, y4);
        var line1 = new Line<int>(p1, p2);
        var line2 = new Line<int>(p3, p4);
        Assert.Equal(expectedResult, line1.Equals(line2));
        Assert.Equal(expectedResult, line2.Equals(line1));
        Assert.Equal(expectedResult, line1 == line2);
    }

    [Theory]
    [InlineData(1L, 1L, 2L, 2L, 2L)]
    [InlineData(2L, 2L, 1L, 1L, 2L)]
    [InlineData(1L, 1L, 1L, 2L, 1L)]
    [InlineData(1L, 1L, 2L, 1L, 1L)]
    [InlineData(1L, 1L, 1L, 1L, 0L)]
    public void ManhattanDistanceTest(long x1, long y1, long x2, long y2, long expectedDistance)
    {
        var p1 = new Point<long>(x1, y1);
        var p2 = new Point<long>(x2, y2);
        var line = new Line<long>(p1, p2);
        Assert.Equal(expectedDistance, line.ManhattanDistance());
    }

    [Fact]
    public void GetPointsYIncrementTest()
    {
        var expectedPoints = new[]
        {
            new Point<int>(0, 0),
            new Point<int>(0, 1),
            new Point<int>(0, 2),
            new Point<int>(0, 3),
            new Point<int>(0, 4)
        };

        var line = new Line<int>(new Point<int>(0, 0), new Point<int>(0, 4));
        var results = line.GetPoints();
        Assert.Equal(expectedPoints, results);
    }

    [Fact]
    public void GetPointsXIncrementTest()
    {
        var expectedPoints = new[]
        {
            new Point<int>(0, 0),
            new Point<int>(1, 0),
            new Point<int>(2, 0),
            new Point<int>(3, 0),
            new Point<int>(4, 0)
        };

        var line = new Line<int>(new Point<int>(0, 0), new Point<int>(4, 0));
        var results = line.GetPoints();
        Assert.Equal(expectedPoints, results);
    }
}
