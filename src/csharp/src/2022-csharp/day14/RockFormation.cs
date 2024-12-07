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

namespace AdventOfCode2022.day14;

using System.Text;
using Common;

internal record RockFormation(IReadOnlyList<Line<int>> Lines)
{
    public bool IsInFormation(Point<int> point)
    {
        for (var i = 0; i < Lines.Count; ++i)
        {
            var line = Lines[i];
            if (line.IsInLine(point))
            {
                return true;
            }
        }

        return false;
    }

    public IEnumerable<Point<int>> GetPoints() => Lines.SelectMany(x => x.GetPoints());

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append(nameof(Lines)).Append(" = ").Append(" [ ").Append(string.Join(", ", Lines)).Append(" ] ");
        return true;
    }
}
