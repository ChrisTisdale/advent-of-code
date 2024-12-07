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

#pragma once
#include <string>
#include <vector>

namespace aoc2022::day2 {
enum class played { rock = 1, paper, scissors };

struct game_round {
  char opponent;
  char you;

  game_round(const char opponent, const char you)
      : opponent(opponent), you(you) {}
};

class solution {
 public:
  static int run_part1(const std::string& file);
  static int run_part2(const std::string& file);

 private:
  static std::vector<game_round> read_file(const std::string& file);
  static played convert_opponent(char opponent);
  static played convert_you(char you);
  static played get_strategy(played opponent, char you);
  static int score(played opponent, played you);
};
}  // namespace aoc2022::day2
