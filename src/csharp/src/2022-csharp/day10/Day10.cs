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

namespace AdventOfCode2022.day10;

public class Day10 : Base2022AdventOfCodeDay<int>
{
    public override async ValueTask<int> ExecutePart1(Stream fileName, CancellationToken token = default) =>
        await ProcessSignals(fileName, token);

    public override async ValueTask<int> ExecutePart2(Stream fileName, CancellationToken token = default) =>
        await ProcessSignals(fileName, token);

    private static async ValueTask<int> ProcessSignals(Stream fileName, CancellationToken token)
    {
        var cycles = new[] { 20, 60, 100, 140, 180, 220 };
        var result = await GetSignalStrengths(await ProcessFile(fileName, token), cycles);
        return result.Sum();
    }

    private static async ValueTask<IReadOnlyList<ICommand>> ProcessFile(Stream fileName, CancellationToken token)
    {
        var items = new List<ICommand>();
        using var sr = new StreamReader(fileName);
        while (!sr.EndOfStream)
        {
            var line = await sr.ReadLineAsync(token);
            if (line == null)
            {
                continue;
            }

            var inputs = line.Split(' ');
            var lower = inputs[0].ToLower();
            switch (lower)
            {
                case "noop":
                    items.Add(new NoOp());
                    break;
                case "addx":
                    items.Add(Add.Parse(inputs));
                    break;
            }
        }

        return items;
    }

    private static ValueTask<IReadOnlyList<int>> GetSignalStrengths(IEnumerable<ICommand> inputs, params int[] tracking)
    {
        var points = new int[tracking.Length];
        var clockCycles = 0;
        var currentValue = 1;
        var currentPos = 0;
        var image = new char[40];
        foreach (var input in inputs)
        {
            for (var i = 0; i < input.ClockCycles; ++i)
            {
                var lookLoc = clockCycles % 40 - currentValue + 1;
                if (lookLoc is >= 0 and < 3)
                {
                    image[currentPos++] = '#';
                }
                else
                {
                    image[currentPos++] = '.';
                }

                if (currentPos >= image.Length)
                {
                    currentPos = 0;
                }

                ++clockCycles;
                var indexOf = Array.IndexOf(tracking, clockCycles);
                if (indexOf != -1)
                {
                    points[indexOf] = currentValue * clockCycles;
                }
            }

            currentValue = input.Execute(currentValue);
        }

        return new ValueTask<IReadOnlyList<int>>(points);
    }
}
