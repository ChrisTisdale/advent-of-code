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

internal class PacketComparer : IComparer<IPacket>
{
    public int Compare(IPacket? x, IPacket? y) =>
        InCorrectOrder(
            x ?? throw new ArgumentNullException(nameof(x)),
            y ?? throw new ArgumentNullException(nameof(y)));

    private static int InCorrectOrder(IPacket left, IPacket right)
    {
        while (true)
        {
            switch (left)
            {
                case ValuePacket valLeft when right is ValuePacket valRight:
                    return valLeft.Value.CompareTo(valRight.Value);
                case ListPacket listLeft when right is ListPacket listRight:
                {
                    for (var i = 0; i < Math.Min(listLeft.Values.Count, listRight.Values.Count); ++i)
                    {
                        var leftValue = listLeft.Values[i];
                        var rightValue = listRight.Values[i];
                        var res = InCorrectOrder(leftValue, rightValue);
                        if (res != 0)
                        {
                            return res;
                        }
                    }

                    return listLeft.Values.Count.CompareTo(listRight.Values.Count);
                }
                default:
                    if (left is ValuePacket)
                    {
                        left = new ListPacket([left]);
                        continue;
                    }

                    right = new ListPacket([right]);
                    continue;
            }
        }
    }
}
