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

namespace AdventOfCode2022.day6;

using System.Text;

public record Results(IReadOnlyList<int> MarkerLocations)
{
    public Results(params int[] markerLocations)
        : this(new List<int>(markerLocations))
    {
    }

    public virtual bool Equals(Results? other) =>
        !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || MarkerLocations.SequenceEqual(other.MarkerLocations));

    public override int GetHashCode() => MarkerLocations.Aggregate(typeof(int).GetHashCode(), HashCode.Combine);

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        const string separator = ", ";
        const string equals = " = ";
        const string arrayStart = "[ ";
        const string arrayEnd = " ]";
        builder.Append(nameof(MarkerLocations))
            .Append(equals)
            .Append(arrayStart)
            .Append(string.Join(separator, MarkerLocations))
            .Append(arrayEnd);
        return true;
    }
}
