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

namespace AdventOfCode2023.day9;

using Common;

public class Day92023 : BaseAdventOfCodeDay<long>
{
    public override DateOnly Year => new(2023, 12, 9);

    public override async ValueTask<long> ExecutePart1(Stream stream, CancellationToken token = default) =>
        await ReadLinesAsync(stream, token).Select(x => x.PredictNext()).Select(next => next.Values[^1]).SumAsync(token);

    public override async ValueTask<long> ExecutePart2(Stream stream, CancellationToken token = default) =>
        await ReadLinesAsync(stream, token).Select(x => x.PredictPrevious()).Select(next => next.Values[0]).SumAsync(token);

    private static IAsyncEnumerable<Readings> ReadLinesAsync(Stream stream, CancellationToken token) =>
        EnumerateLinesAsync(stream, token)
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray())
            .Select(values => new Readings(values));
}
