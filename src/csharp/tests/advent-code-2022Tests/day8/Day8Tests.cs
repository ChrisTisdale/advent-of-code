﻿// Copyright (c) Christopher Tisdale 2024.
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

namespace AdventOfCode2022Tests.day8;

using AdventOfCode2022.day8;

public class Day8Tests
{
    private readonly Day8 _target;

    public Day8Tests()
    {
        _target = new Day8();
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
        var expectedScore = new TreeScore(21, 8);
        var part1Result = await _target.ExecutePart1(_target.GetFileStream("sample.txt"));
        part1Result.Should().Be(expectedScore.VisibleTrees);
    }

    [Fact(Timeout = 1000)]
    public async Task Sample_Part_2_Matches()
    {
        var expectedScore = new TreeScore(21, 8);
        var part1Result = await _target.ExecutePart2(_target.GetFileStream("sample.txt"));
        part1Result.Should().Be(expectedScore.BestScore);
    }

    [Fact(Timeout = 2000)]
    public async Task Measurements_Part_1_Matches()
    {
        var expectedScore = new TreeScore(1719, 590824);
        var part1Result = await _target.ExecutePart1(_target.GetFileStream("measurements.txt"));
        part1Result.Should().Be(expectedScore.VisibleTrees);
    }

    [Fact(Timeout = 2000)]
    public async Task Measurements_Part_2_Matches()
    {
        var expectedScore = new TreeScore(1719, 590824);
        var part1Result = await _target.ExecutePart2(_target.GetFileStream("measurements.txt"));
        part1Result.Should().Be(expectedScore.BestScore);
    }
}
