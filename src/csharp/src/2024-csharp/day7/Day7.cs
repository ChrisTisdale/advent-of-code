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

public class Day7 : Base2024AdventOfCodeDay<ulong>
{
    public override ValueTask<ulong> ExecutePart1(Stream stream, CancellationToken token = default)
    {
        return GetResults(stream, [Operation.Multiply, Operation.Add], token);
    }

    public override ValueTask<ulong> ExecutePart2(Stream stream, CancellationToken token = default)
    {
        return GetResults(stream, [Operation.Multiply, Operation.Concatenate, Operation.Add], token);
    }

    private async ValueTask<ulong> GetResults(Stream stream, Operation[] operations, CancellationToken token)
    {
        var calibrations = await ReadOperations(stream, operations, token);
        return calibrations
            .Where(
                calibration => calibration.Operations.Select(operation => operation.Value)
                    .Any(calculated => calibration.Expected == calculated))
            .Aggregate<Calibration, ulong>(0, (current, calibration) => current + calibration.Expected);
    }

    private async ValueTask<IReadOnlyList<Calibration>> ReadOperations(Stream stream, Operation[] operations, CancellationToken token)
    {
        var calibrations = new List<Calibration>();
        await foreach (var line in EnumerateLinesAsync(stream, token))
        {
            var strings = line.Split(':');
            if (strings.Length < 2)
            {
                continue;
            }

            var expected = ulong.Parse(strings[0]);
            var count = 0;
            var ops = new Dictionary<int, IEnumerable<IOperation>>();
            foreach (var value in strings[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse))
            {
                if (ops.TryGetValue(count - 1, out var previous))
                {
                    ops[count++] = previous.SelectMany(p => BuildOperations(operations, p, value));
                }
                else
                {
                    ops[count++] = BuildOperations(operations, null, value);
                }
            }

            if (ops.TryGetValue(count - 1, out var op))
            {
                calibrations.Add(new Calibration(expected, op));
            }
        }

        return calibrations;
    }

    private IEnumerable<IOperation> BuildOperations(Operation[] operations, IOperation? previous, ulong value)
    {
        return operations.Select(
                x =>
                {
                    IOperation o = x switch
                    {
                        Operation.Add => new AddOperation(previous, value),
                        Operation.Multiply => new MultiplyOperation(previous, value),
                        Operation.Concatenate => new ConcatenateOperation(previous, value),
                        _ => throw new NotImplementedException()
                    };

                    return o;
                });
    }
}
