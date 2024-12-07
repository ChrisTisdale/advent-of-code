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

namespace AdventOfCode2024.day1;

public class Day1 : Base2024AdventOfCodeDay<long>
{
    public override async ValueTask<long> ExecutePart1(Stream stream, CancellationToken token = default)
    {
        var (left, right) = await GetLists(stream, token);

        left.Sort();
        right.Sort();
        long sum = 0;
        for (var i = 0; i < left.Count; ++i)
        {
            sum += Math.Abs(right[i] - left[i]);
        }

        return sum;
    }

    public override async ValueTask<long> ExecutePart2(Stream stream, CancellationToken token = default)
    {
        var (left, right) = await GetLists(stream, token);
        long sum = 0;
        for (var i = 0; i < left.Count; ++i)
        {
            var leftValue = left[i];
            var count = 0;
            for (var j = 0; j < right.Count; ++j)
            {
                if (leftValue == right[j])
                {
                    ++count;
                }
            }

            sum += count * leftValue;
        }

        return sum;
    }

    private async ValueTask<(List<long> left, List<long> right)> GetLists(Stream stream, CancellationToken token)
    {
        List<long> left = [];
        List<long> right = [];
        await foreach (var line in EnumerateLinesAsync(stream, token))
        {
            var split = line.IndexOf(' ');
            if (split == -1)
            {
                continue;
            }

            var leftString = line.AsSpan(0, split);
            var rightString = line.AsSpan(split+1);
            var a = long.Parse(leftString);
            var b = long.Parse(rightString);
            left.Add(a);
            right.Add(b);
        }

        return (left, right);
    }
}
