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

internal record NewCalculator(string Left, string Right, Operator Operator)
{
    public long GetNew(long old)
    {
        var left = GetValue(Left, old);
        var right = GetValue(Right, old);
        checked
        {
            return Operator switch
            {
                Operator.Add => left + right,
                Operator.Subtract => left - right,
                Operator.Multiply => left * right,
                Operator.Divide => left / right,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public long GetNew(long old, long factor) => GetNew(old) % factor;

    private static long GetValue(string value, long old) => value.ToLower() == "old" ? old : long.Parse(value);
}
