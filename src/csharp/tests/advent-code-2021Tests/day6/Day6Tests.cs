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

namespace AdventOfCode2021Tests.day6;

using AdventOfCode2021.day6;

public class Day6Tests
{
    private readonly Day6 _target = new();

    [Fact]
    public void YearTest()
    {
        var date = _target.Year;
        Assert.Equal(Constants.ExpectedYear, date);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileStream("sample.txt"), TestContext.Current.CancellationToken);
        Assert.Equal(5934L, part1Result);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileStream("sample.txt"), TestContext.Current.CancellationToken);
        Assert.Equal(26984457539L, part1Result);
    }

    [Fact(Timeout = 2000)]
    public async Task Measurements_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileStream("measurements.txt"), TestContext.Current.CancellationToken);
        Assert.Equal(353274L, part1Result);
    }

    [Fact(Timeout = 2000)]
    public async Task Measurements_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileStream("measurements.txt"), TestContext.Current.CancellationToken);
        Assert.Equal(1609314870967L, part1Result);
    }
}
