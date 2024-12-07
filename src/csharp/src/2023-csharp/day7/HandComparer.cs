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

namespace AdventOfCode2023.day7;

public class HandComparer : IComparer<Hand>
{
    public int Compare(Hand? left, Hand? right)
    {
        if (ReferenceEquals(left, right))
        {
            return 0;
        }

        if (left is null)
        {
            return -1;
        }

        if (right is null)
        {
            return 1;
        }

        var leftScore = left.CalculateScore();
        var rightScore = right.CalculateScore();
        return leftScore == rightScore ? ComparisonInOrder(left, right) : leftScore.CompareTo(rightScore);
    }

    private static int ComparisonInOrder(Hand left, Hand right)
    {
        for (var i = 0; i < left.Numbers.Count; ++i)
        {
            if (left.Numbers[i] != right.Numbers[i])
            {
                return left.Numbers[i].CompareTo(right.Numbers[i]);
            }
        }

        return 0;
    }
}
