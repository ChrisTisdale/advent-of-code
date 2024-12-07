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

internal class Monkey
{
    public Monkey(int id, Queue<long> items, NewCalculator calculator, Evaluator[] evaluators)
    {
        Id = id;
        Items = items;
        Calculator = calculator;
        Evaluators = evaluators;
    }

    public int Id { get; }

    public Queue<long> Items { get; }

    public NewCalculator Calculator { get; }

    public Evaluator[] Evaluators { get; }

    public override string ToString() =>
        new PrintableMonkey(Id, Items.ToArray(), Calculator, Evaluators).ToString()!;
}
