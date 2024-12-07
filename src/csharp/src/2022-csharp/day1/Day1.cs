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

namespace AdventOfCode2022.day1;

public class Day1 : Base2022AdventOfCodeDay<decimal>
{
    public override async ValueTask<decimal> ExecutePart1(Stream fileName, CancellationToken token = default) =>
        await FindBest(fileName, 1, token);

    public override async ValueTask<decimal> ExecutePart2(Stream fileName, CancellationToken token = default) =>
        await FindBest(fileName, 3, token);

    private static async ValueTask<decimal> FindBest(Stream filename, int takeCount, CancellationToken token)
    {
        var cur = 0m;
        var values = new List<decimal>();
        using var sr = new StreamReader(filename);
        while (!sr.EndOfStream)
        {
            var readLine = await sr.ReadLineAsync(token);
            if (string.IsNullOrWhiteSpace(readLine))
            {
                values.Add(cur);
                cur = 0;
            }
            else
            {
                cur += decimal.Parse(readLine);
            }
        }

        if (cur != 0)
        {
            values.Add(cur);
        }

        return values.OrderDescending().Take(takeCount).Sum();
    }
}
