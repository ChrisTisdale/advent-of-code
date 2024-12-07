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

namespace AdventOfCode2022.day11;

using System.Text;

internal record PrintableMonkey(int Id, IReadOnlyList<long> Items, NewCalculator Calculator, Evaluator[] Evaluators)
{
    protected virtual bool PrintMembers(StringBuilder builder)
    {
        const string separator = ", ";
        const string equals = " = ";
        const string arrayStart = "[ ";
        const string arrayEnd = " ]";
        builder.Append(nameof(Id)).Append(equals).Append(Id).Append(separator);
        builder.Append(nameof(Items))
            .Append(equals)
            .Append(arrayStart)
            .Append(string.Join(separator, Items))
            .Append(arrayEnd)
            .Append(separator);

        builder.Append(nameof(Calculator)).Append(equals).Append(Calculator).Append(separator);
        builder.Append(nameof(Evaluators))
            .Append(equals)
            .Append(arrayStart)
            .Append(string.Join(", ", Evaluators.Select(x => x.ToString())))
            .Append(arrayEnd);

        return true;
    }
}
