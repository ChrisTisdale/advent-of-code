﻿// Copyright (c) Christopher Tisdale 2024.
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

using System.Text.RegularExpressions;

public class Day13 : Base2022AdventOfCodeDay<int>
{
    private static readonly Regex Regex = new($"({Regex.Escape(",")}|{Regex.Escape("[")}|{Regex.Escape("]")})");

    public override async ValueTask<int> ExecutePart1(Stream fileName, CancellationToken token = default) =>
        await HandleFilePart1(fileName, token);

    public override async ValueTask<int> ExecutePart2(Stream fileName, CancellationToken token = default) => await HandleFile(fileName, token);

    private static async Task<int> HandleFile(Stream file, CancellationToken token)
    {
        var result = await GetPackets(file, token);
        var starter = new ListPacket([new ListPacket([new ValuePacket(2)])]);
        var ender = new ListPacket([new ListPacket([new ValuePacket(6)])]);
        var sortedList =
            new SortedSet<IPacket>(result.SelectMany(x => x).Append(starter).Append(ender), new PacketComparer());
        var count = 0;
        var startIndex = 0;
        var endIndex = 0;
        foreach (var p in sortedList)
        {
            ++count;
            if (Equals(p, starter))
            {
                startIndex = count;
            }

            if (Equals(p, ender))
            {
                endIndex = count;
            }
        }

        return startIndex * endIndex;
    }

    private static async Task<int> HandleFilePart1(Stream file, CancellationToken token)
    {
        var result = await GetPackets(file, token);
        var count = 0;
        var comparer = new PacketComparer();
        for (var i = 0; i < result.Count; ++i)
        {
            var checker = result[i];
            if (comparer.Compare(checker.Left, checker.Right) < 0)
            {
                count += i + 1;
            }
        }

        return count;
    }

    private static async ValueTask<IReadOnlyList<PacketChecker>> GetPackets(Stream file, CancellationToken token)
    {
        var lines = await ReadAllLinesAsync(file, token);
        var comparisons = new List<PacketChecker>();
        for (var i = 0; i < lines.Count; i += 3)
        {
            var left = ParseLine(lines[i]);
            var right = ParseLine(lines[i + 1]);
            var packetChecker = new PacketChecker(left, right);
            comparisons.Add(packetChecker);
        }

        return comparisons;
    }

    private static ListPacket ParseLine(string line)
    {
        var values = Regex.Split(line).Where(x => !string.IsNullOrEmpty(x)).ToArray();
        var count = 1; // skip first is must always be [
        return ParseValues(values, ref count);
    }

    private static ListPacket ParseValues(IReadOnlyList<string> values, ref int i)
    {
        var data = new List<IPacket>();
        for (; i < values.Count; ++i)
        {
            var value = values[i];
            switch (value)
            {
                case "[":
                    ++i;
                    data.Add(ParseValues(values, ref i));
                    break;
                case "]":
                    ++i;
                    return new ListPacket(data);
                case ",":
                    break;
                default:
                    data.Add(new ValuePacket(int.Parse(value)));
                    break;
            }
        }

        return new ListPacket(data);
    }
}
