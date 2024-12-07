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
#include <map>
#include <string>
#include <vector>

namespace aoc2022::day8 {
enum class direction { Up = 0, Down, Left, Right };

struct tree {
  char height;
  std::size_t row;
  std::size_t col;

  explicit tree(char height, std::size_t row, std::size_t col)
      : height(height), row(row), col(col) {}

  bool operator<(const tree& rhs) const {
    return std::tie(height, row, col) < std::tie(rhs.height, rhs.row, rhs.col);
  }
 
  bool operator>(const tree& rhs) const { return rhs < *this; }

  bool operator<=(const tree& rhs) const { return !(rhs < *this); }

  bool operator>=(const tree& rhs) const { return !(*this < rhs); }

  bool operator==(const tree& rhs) const {
    return std::tie(height, row, col) == std::tie(rhs.height, rhs.row, rhs.col);
  }

  bool operator!=(const tree& rhs) const { return !(rhs == *this); }
};

struct edge {
  direction dir;
  tree start;
  tree end;

  explicit edge(direction dir, tree start, tree end)
      : dir(dir), start(start), end(end) {}

  bool operator<(const edge& rhs) const {
    return std::tie(dir, start, end) < std::tie(rhs.dir, rhs.start, rhs.end);
  }

  bool operator==(const edge& rhs) const {
    return std::tie(dir, start, end) == std::tie(rhs.dir, rhs.start, rhs.end);
  }

  bool operator!=(const edge& rhs) const { return !(rhs == *this); }

  bool operator>(const edge& rhs) const { return rhs < *this; }

  bool operator<=(const edge& rhs) const { return !(rhs < *this); }

  bool operator>=(const edge& rhs) const { return !(*this < rhs); }
};

class solution {
 public:
  static long long run_part1(const std::string& file);
  static long long run_part2(const std::string& file);

 private:
  static std::map<tree, std::map<direction, edge>> read_file(
      const std::string& file);
  static bool is_visible(
      const tree& current, const std::map<direction, edge>& edges,
      const std::map<tree, std::map<direction, edge>>& graph);
  static long long calculate_scenic(
      const tree& current, const std::map<direction, edge>& edges,
      const std::map<tree, std::map<direction, edge>>& graph);
};
}  // namespace aoc2022::day8
