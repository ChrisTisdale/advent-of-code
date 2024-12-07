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

public record Readings(IReadOnlyList<long> Values)
{
    public bool AllZero { get; } = Values.All(x => x == 0L);

    public Readings GetDifference()
    {
        var next = new long[Values.Count - 1];
        for (var i = 0; i < Values.Count - 1; ++i)
        {
            next[i] = Values[i + 1] - Values[i];
        }

        return new Readings(next);
    }

    public Readings PredictNext()
    {
        if (AllZero)
        {
            return new Readings(Values.Append(0).ToArray());
        }

        var difference = GetDifference();
        var next = difference.PredictNext();
        return new Readings(Values.Append(Values[^1] + next.Values[^1]).ToArray());
    }

    public Readings PredictPrevious()
    {
        if (AllZero)
        {
            return new Readings(Values.Prepend(0).ToArray());
        }

        var difference = GetDifference();
        var next = difference.PredictPrevious();
        return new Readings(Values.Prepend(Values[0] - next.Values[0]).ToArray());
    }
}
