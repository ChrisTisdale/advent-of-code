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

namespace AdventOfCode2023.day1;

using Common;

public class Day12023 : BaseAdventOfCodeDay<long>
{
    private static readonly IReadOnlyDictionary<Part, string[]> Files = new Dictionary<Part, string[]>
    {
        { Part.Part1, ["samplePart1.txt", "measurements.txt"] },
        { Part.Part2, ["samplePart1.txt", "measurements.txt"] }
    };

    private static readonly string[] DigitsAsText =
    [
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine"
    ];

    public Day12023()
        : base(Files)
    {
    }

    public override DateOnly Year => new(2023, 12, 1);

    public override ValueTask<long> ExecutePart1(Stream stream, CancellationToken token = default) => GetSnowCalibration(stream, false, token);

    public override ValueTask<long> ExecutePart2(Stream stream, CancellationToken token = default) => GetSnowCalibration(stream, true, token);

    private static async ValueTask<long> GetSnowCalibration(Stream stream, bool canHaveText, CancellationToken token)
    {
        long count = 0;
        using var sr = new StreamReader(stream);
        while (await sr.ReadLineAsync(token) is { } line)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            SnowCalibration calibration = new(null, 0);
            for (var i = 0; i < line.Length; ++i)
            {
                calibration = CheckCharacter(line, i, canHaveText, calibration);
            }

            if (calibration.HasData)
            {
                count += calibration.Total;
            }
        }

        return count;
    }

    private static SnowCalibration CheckCharacter(
        string line,
        int i,
        bool digitsAsText,
        SnowCalibration calibration)
    {
        if (int.TryParse(line.AsSpan(i, 1), out var res))
        {
            return new SnowCalibration(calibration.Start ?? res, res);
        }

        if (!digitsAsText)
        {
            return calibration;
        }

        var found = FindDigitAsText(line, i);
        return found < 0 ? calibration : new SnowCalibration(calibration.Start ?? found + 1, found + 1);
    }

    private static int FindDigitAsText(string line, int i)
    {
        var found = -1;
        for (var j = 0; j < DigitsAsText.Length; ++j)
        {
            var currentText = DigitsAsText[j];
            if (line.Length < i + currentText.Length)
            {
                continue;
            }

            var searchText = line.AsSpan(i, currentText.Length);
            if (!searchText.Equals(currentText.AsSpan(), StringComparison.Ordinal))
            {
                continue;
            }

            found = j;
            break;
        }

        return found;
    }
}
