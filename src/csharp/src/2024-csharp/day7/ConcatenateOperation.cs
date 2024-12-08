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

namespace AdventOfCode2024.day7;

public sealed record ConcatenateOperation(IOperation? Left, ulong Right) : IOperation
{
    public Operation Operation => Operation.Concatenate;

    public ulong Value { get; } = ulong.Parse($"{Left?.Value ?? 0}{Right}");

    public void Deconstruct(out IOperation? left, out ulong right)
    {
        left = Left;
        right = Right;
    }
}
