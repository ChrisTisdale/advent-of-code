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
#include <queue>
#include <string>
#include <utility>

namespace aoc2022::day5 {
struct player {
  std::deque<char> crane;
};

struct move {
  int count;
  int from_player;
  int to_player;

  explicit move(int count, int from_player, int to_player)
      : count(count), from_player(from_player), to_player(to_player) {}
};

struct game {
  std::vector<player> players;
  std::vector<move> moves;

  explicit game(std::vector<player> players, std::vector<move> moves)
      : players(std::move(players)), moves(std::move(moves)) {}
};

class solution {
 public:
  static std::string run_part1(const std::string& file);
  static std::string run_part2(const std::string& file);

 private:
  static game read_file(const std::string& file);
};
}  // namespace aoc2022::day5
