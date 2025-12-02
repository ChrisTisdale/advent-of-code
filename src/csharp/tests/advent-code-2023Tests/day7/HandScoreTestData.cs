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

namespace AdventOfCode2023Tests.day7;

using System.Collections;
using AdventOfCode2023.day7;

public class HandScoreTestData : IEnumerable<TheoryDataRow<Hand, HandScore>>
{
    public IEnumerator<TheoryDataRow<Hand, HandScore>> GetEnumerator() {
        yield return
            new TheoryDataRow<Hand, HandScore>(
                new Hand([CardNumber.Joker, CardNumber.Joker, CardNumber.Joker, CardNumber.Joker, CardNumber.Joker], 0),
                HandScore.FiveOfAKind
            );
        yield return
            new TheoryDataRow<Hand, HandScore>(
                new Hand([CardNumber.Ace, CardNumber.Ace, CardNumber.Ace, CardNumber.Ace, CardNumber.Ace], 0),
                HandScore.FiveOfAKind
            );
        yield return
            new TheoryDataRow<Hand, HandScore>(
                new Hand([CardNumber.Joker, CardNumber.Ace, CardNumber.Ace, CardNumber.Ace, CardNumber.Ace], 0),
                HandScore.FiveOfAKind
            );
        yield return
            new TheoryDataRow<Hand, HandScore>(
                new Hand([CardNumber.Ace, CardNumber.King, CardNumber.King, CardNumber.Ace, CardNumber.Ace], 0),
                HandScore.FullHouse
            );
        yield return
            new TheoryDataRow<Hand, HandScore>(
                new Hand([CardNumber.King, CardNumber.Joker, CardNumber.Ace, CardNumber.Ace, CardNumber.Ace], 0),
                HandScore.FourOfAKind
            );
        yield return
            new TheoryDataRow<Hand, HandScore>(
                new Hand([CardNumber.King, CardNumber.Joker, CardNumber.Joker, CardNumber.Ace, CardNumber.Ace], 0),
                HandScore.FourOfAKind
            );
        yield return
            new TheoryDataRow<Hand, HandScore>(
                new Hand([CardNumber.King, CardNumber.Joker, CardNumber.Joker, CardNumber.Queen, CardNumber.Ace], 0),
                HandScore.ThreeOfAKind
            );
        yield return
            new TheoryDataRow<Hand, HandScore>(
                new Hand([CardNumber.King, CardNumber.Joker, CardNumber.Queen, CardNumber.Ace, CardNumber.Ace], 0),
                HandScore.ThreeOfAKind
            );
        yield return
            new TheoryDataRow<Hand, HandScore>(
                new Hand([CardNumber.King, CardNumber.King, CardNumber.Ace, CardNumber.Two, CardNumber.Ace], 0),
                HandScore.TwoPair
            );
        yield return
            new TheoryDataRow<Hand, HandScore>(
                new Hand([CardNumber.King, CardNumber.Joker, CardNumber.Queen, CardNumber.Ace, CardNumber.Three], 0),
                HandScore.OnePair
            );
        yield return
            new TheoryDataRow<Hand, HandScore>(
                new Hand([CardNumber.King, CardNumber.Three, CardNumber.Two, CardNumber.Ace, CardNumber.Ace], 0),
                HandScore.OnePair
            );
        yield return
            new TheoryDataRow<Hand, HandScore>(
                new Hand([CardNumber.King, CardNumber.Three, CardNumber.Four, CardNumber.Ace, CardNumber.Queen], 0),
                HandScore.HighCard
            );
        yield return
            new TheoryDataRow<Hand, HandScore>(
                new Hand("T55J5".Select(x => x.ToCardNumber(true)).ToList(), 0),
                HandScore.FourOfAKind
            );
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
