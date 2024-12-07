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

namespace AdventOfCode2022.day13;

using System.Text;

internal record ListPacket(IReadOnlyList<IPacket> Values) : IPacket
{
    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append(nameof(Values)).Append(" = ").Append(" [ ").Append(string.Join(",", Values)).Append(" ]");
        return true;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append('[').Append(string.Join(",", Values)).Append(']');
        return sb.ToString();
    }
}
