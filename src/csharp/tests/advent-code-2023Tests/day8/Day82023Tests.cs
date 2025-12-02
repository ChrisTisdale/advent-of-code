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

namespace AdventOfCode2023Tests.day8;

using AdventOfCode2023.day8;

public class Day82023Tests
{
    private readonly Day82023 _target = new();

    [Fact]
    public void YearTest()
    {
        Assert.Equal(new DateOnly(2023, 12, 8), _target.Year);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileStream("samplePart1.txt"), TestContext.Current.CancellationToken);
        Assert.Equal(6L, part1Result);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileStream("samplePart2.txt"), TestContext.Current.CancellationToken);
        Assert.Equal(6L, part1Result);
    }

    [Fact(Timeout = 2000)]
    public async Task Measurements_Part_1_Matches()
    {
        var part1Result = await _target.ExecutePart1(_target.GetFileStream("measurements.txt"), TestContext.Current.CancellationToken);
        Assert.Equal(13301L, part1Result);
    }

    [Fact(Timeout = 3000)]
    public async Task Measurements_Part_2_Matches()
    {
        var part1Result = await _target.ExecutePart2(_target.GetFileStream("measurements.txt"), TestContext.Current.CancellationToken);
        Assert.Equal(7309459565207L, part1Result);
    }
}
