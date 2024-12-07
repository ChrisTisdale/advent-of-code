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

namespace AdventOfCode2021.day6;

public class Day6 : Base2021AdventOfCodeDay<long>
{
    public override async ValueTask<long> ExecutePart1(Stream stream, CancellationToken token = default)
    {
        var fish = await ReadFish(stream, token);
        return DetermineFishSize(fish, 80);
    }

    public override async ValueTask<long> ExecutePart2(Stream stream, CancellationToken token = default)
    {
        var fish = await ReadFish(stream, token);
        return DetermineFishSize(fish, 256);
    }

    private static long DetermineFishSize(IReadOnlyCollection<long> fish, int length)
    {
        long count = fish.Count;
        var set = fish.GroupBy(x => x).ToDictionary(x => x.Key, x => (long)x.Count());
        for (var i = 0; i < length; ++i)
        {
            var valuePairs = set.ToArray();
            set.Clear();
            var addCount = GetNewFish(valuePairs, set);
            if (addCount <= 0)
            {
                continue;
            }

            count += addCount;
            set.Add(8, addCount);
        }

        return count;
    }

    private static long GetNewFish(IEnumerable<KeyValuePair<long, long>> valuePairs, IDictionary<long, long> set)
    {
        var addCount = 0L;
        foreach (var j in valuePairs)
        {
            var update = j.Key - 1;
            if (j.Key == 0)
            {
                addCount += j.Value;
                update = 6;
            }

            if (set.ContainsKey(update))
            {
                set[update] += j.Value;
            }
            else
            {
                set.Add(update, j.Value);
            }
        }

        return addCount;
    }

    private static async ValueTask<IReadOnlyList<long>> ReadFish(Stream stream, CancellationToken token)
    {
        var input = await EnumerateLinesAsync(stream, token).FirstOrDefaultAsync(cancellationToken: token).ConfigureAwait(false);
        return string.IsNullOrEmpty(input) ? Array.Empty<long>() : input.Split(',').Select(long.Parse).ToList();
    }
}
