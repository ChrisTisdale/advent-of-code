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

namespace AdventOfCode2022.day7;

using System.Text;

public record DirectoryInfo(string FullName, ISet<IContainingItem> ContainingItems) : IContainingItem
{
    public string Name => FullName == "/" ? FullName : FullName[(FullName.LastIndexOf('/') + 1)..];

    public long Size => ContainingItems.Sum(x => x.Size);

    public virtual bool Equals(DirectoryInfo? other) =>
        !ReferenceEquals(null, other) &&
        (ReferenceEquals(this, other) || FullName == other.FullName && ContainingItems.SequenceEqual(other.ContainingItems));

    public override int GetHashCode() =>
        HashCode.Combine(Name, ContainingItems.Aggregate(typeof(int).GetHashCode(), HashCode.Combine));

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        const string separator = ", ";
        const string equals = " = ";
        const string arrayStart = "[ ";
        const string arrayEnd = " ]";

        builder.Append(nameof(FullName)).Append(equals).Append(FullName).Append(separator);
        builder.Append(nameof(Name)).Append(equals).Append(Name).Append(separator);
        builder.Append(nameof(Size)).Append(equals).Append(Size).Append(separator);
        builder.Append(nameof(ContainingItems))
            .Append(equals)
            .Append(arrayStart)
            .Append(string.Join(separator, ContainingItems))
            .Append(arrayEnd);

        return true;
    }
}
