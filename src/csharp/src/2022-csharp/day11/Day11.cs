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

namespace AdventOfCode2022.day11;

using System.Text.RegularExpressions;

public class Day11 : Base2022AdventOfCodeDay<long>
{
    private static readonly Regex Regex = new(
        $"({Regex.Escape("+")}|{Regex.Escape("*")}|{Regex.Escape("-")}|{Regex.Escape("/")})");

    public override async ValueTask<long> ExecutePart1(Stream fileName, CancellationToken token = default) =>
        await HandleRound(fileName, true, 20, token: token);

    public override async ValueTask<long> ExecutePart2(Stream fileName, CancellationToken token = default) =>
        await HandleRound(fileName, false, 10000, token: token);

    private static async ValueTask<IReadOnlyList<Monkey>> ProcessFile(Stream fileName, CancellationToken token)
    {
        var monkeys = new List<Monkey>();
        var linesAsync = (await ReadAllLinesAsync(fileName, token)).ToArray();
        for (var i = 0; i < linesAsync.Length; i += 7)
        {
            var id = int.Parse(linesAsync[i].Split(' ')[1].TrimEnd(':'));
            var ids = linesAsync[i + 1].Split(':')[1].Split(',').Select(x => x.Trim(' ')).Select(long.Parse);
            var calc = ParseCalc(linesAsync[i + 2]);
            var evaluators = GetEvaluators(linesAsync.AsSpan(i + 3, 3));
            monkeys.Add(new Monkey(id, new Queue<long>(ids), calc, evaluators));
        }

        return monkeys;
    }

    private static async ValueTask<IReadOnlyDictionary<int, long>> EvaluateRounds(
        IReadOnlyList<Monkey> monkeys,
        int roundCount,
        bool damagesPerRound,
        bool printRounds = false)
    {
        var inspectedCount = monkeys.ToDictionary(m => m.Id, _ => 0L);
        var factor = damagesPerRound ? long.MaxValue : monkeys.Aggregate(1L, (f, m) => f * m.Evaluators.First().Divisor);
        for (var i = 0; i < roundCount; ++i)
        {
            if (printRounds && i % 1000 == 0)
            {
                Console.WriteLine(@"---------------------------");
                Console.WriteLine($@"Start of Round {i + 1}:");
            }

            await EvaluateRound(monkeys, inspectedCount, damagesPerRound, factor, printRounds && i % 1000 == 0);
        }

        return inspectedCount;
    }

    private static ValueTask EvaluateRound(
        IReadOnlyList<Monkey> monkeys,
        IDictionary<int, long> inspectCounter,
        bool damagesPerRound,
        long factor,
        bool printResult = false)
    {
        foreach (var monkey in monkeys)
        {
            while (monkey.Items.Any())
            {
                var value = monkey.Items.Dequeue();
                value = monkey.Calculator.GetNew(value, factor);
                if (damagesPerRound)
                {
                    value /= 3;
                }

                var evaluator = monkey.Evaluators.First(x => x.IsMeet(value));
                monkeys[evaluator.NextId].Items.Enqueue(value);
                inspectCounter[monkey.Id] += 1L;
            }
        }

        if (!printResult)
        {
            return ValueTask.CompletedTask;
        }

        foreach (var monkey in monkeys)
        {
            Console.WriteLine($@"Monkey-{monkey.Id} inspected: {inspectCounter[monkey.Id]}");
        }

        return ValueTask.CompletedTask;
    }

    private static NewCalculator ParseCalc(string line)
    {
        var function = line.Split(':')[1].Replace(" ", string.Empty);
        var operation = function.Split('=')[1];
        var dataPoints = Regex.Split(operation).ToArray();
        return new NewCalculator(dataPoints[0], dataPoints[2], GetOperator(dataPoints[1]));
    }

    private static Operator GetOperator(string value) =>
        value switch
        {
            "+" => Operator.Add,
            "*" => Operator.Multiply,
            "-" => Operator.Subtract,
            "/" => Operator.Divide,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };

    private static Evaluator[] GetEvaluators(ReadOnlySpan<string> lines)
    {
        var checker = int.Parse(lines[0].Split(' ').Last());
        return [GetEvaluator(checker, lines[1]), GetEvaluator(checker, lines[2])];
    }

    private static Evaluator GetEvaluator(int checker, string line)
    {
        var truther = line.Replace(":", string.Empty).Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var expected = bool.Parse(truther[1]);
        var id = int.Parse(truther.Last());
        return new Evaluator(expected, checker, id);
    }

    private static async Task<long> HandleRound(
        Stream fileName,
        bool round1,
        int roundCount,
        bool print = false,
        CancellationToken token = default)
    {
        var result = await ProcessFile(fileName, token);
        var inspected = await EvaluateRounds(result, roundCount, round1, print);
        var highestTwo = inspected.Values.OrderDescending().Take(2).ToArray();
        return highestTwo[0] * highestTwo[1];
    }
}
