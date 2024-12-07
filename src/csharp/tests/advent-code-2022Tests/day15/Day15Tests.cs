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

namespace AdventOfCode2022Tests.day15;

using AdventOfCode2022.day15;

public class Day15Tests
{
    private readonly Day15 _target;

    public Day15Tests()
    {
        _target = new Day15();
    }

    [Fact]
    public void YearTest()
    {
        var date = _target.Year;
        date.Should().Be(Constants.ExpectedYear);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_1_Matches()
    {
        await Part_1_Matches("sample.txt", 26L);
    }

    [Fact(Skip = "Test is too long")]
    public async Task Measurement_Part_1_Matches()
    {
        await Part_1_Matches("measurements.txt", 6078701L);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_2_Matches()
    {
        await Part_2_Matches("sample.txt", 56000011L);
    }

    [Fact(Skip = "Test is too long")]
    public async Task Measurement_Part_2_Matches()
    {
        await Part_2_Matches("measurements.txt", 12567351400528L);
    }

    private async Task Part_1_Matches(string fileName, long expectedResult)
    {
        var part1Result = await _target.ExecutePart1(fileName, _target.GetFileStream(fileName));
        part1Result.Should().Be(expectedResult);
    }

    private async Task Part_2_Matches(string fileName, long expectedResult)
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileStream(fileName));
        part1Result.Should().Be(expectedResult);
    }
}
