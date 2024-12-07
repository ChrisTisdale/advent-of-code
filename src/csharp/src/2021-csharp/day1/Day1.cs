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

namespace AdventOfCode2021.day1;

public class Day1 : Base2021AdventOfCodeDay<int>
{
    public override async ValueTask<int> ExecutePart1(Stream fileName, CancellationToken token = default) =>
        await FindIncreasingCount(fileName, 1, token);

    public override async ValueTask<int> ExecutePart2(Stream fileName, CancellationToken token = default) =>
        await FindIncreasingCount(fileName, 3, token);

    private static async ValueTask<int> FindIncreasingCount(Stream stream, int windowSize, CancellationToken token)
    {
        var lines = await EnumerateLinesAsync(stream, token).Select(int.Parse).ToArrayAsync(token);
        var count = 0;
        for (var i = 0; i < lines.Length - windowSize; ++i)
        {
            var startWindow = lines[i..(i + windowSize)];
            var endWindow = lines[(i + 1)..(i + windowSize + 1)];
            if (startWindow.Sum() < endWindow.Sum())
            {
                ++count;
            }
        }

        return count;
    }
}
