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

#include "day9.h"

#include <memory>
#include <set>
#include <sstream>

using namespace aoc2022::day9;

long long solution::run_part1(std::istream& file) {
  auto inputs = read_file(file);
  return find_unique_spaces(inputs, 0);
}

long long solution::run_part2(std::istream& file) {
  auto inputs = read_file(file);
  return find_unique_spaces(inputs, 8);
}

std::vector<input> solution::read_file(std::istream& file) {
  std::vector<input> data;
  std::string line;
  while (std::getline(file, line)) {
    std::stringstream ss(line);
    char d;
    std::size_t c;
    ss >> d;
    ss >> c;
    data.emplace_back(d, c);
  }

  return data;
}

long long solution::find_unique_spaces(const std::vector<input>& data,
                                       const std::size_t middle_count) {
  const long count = static_cast<long>(middle_count + 2);
  std::vector<point> middle(count, point());
  auto head = &middle[0];
  auto tail = &middle[middle.size() - 1];
  std::set<point> found;
  for (auto move : data) {
    for (std::size_t i = 0; i < move.count; ++i) {
      switch (move.direction) {
        case 'U':
          head->y += 1;
          break;
        case 'D':
          head->y -= 1;
          break;
        case 'L':
          head->x -= 1;
          break;
        default:
          head->x += 1;
          break;
      }

      for (long j = 1; j < count; ++j) {
        update_current_location(middle[j - 1], middle[j]);
      }

      found.insert(*tail);
    }
  }

  return static_cast<long long>(found.size());
}

void solution::update_current_location(const point& parent, point& current) {
  auto x_updated = std::abs(current.x - parent.x) > 1;
  auto y_updated = std::abs(current.y - parent.y) > 1;

  if (x_updated && current.y != parent.y ||
      y_updated && current.x != parent.x) {
    current.x = current.x > parent.x ? current.x - 1 : current.x + 1;
    current.y = current.y > parent.y ? current.y - 1 : current.y + 1;
    return;
  }

  if (x_updated) {
    current.x = current.x > parent.x ? current.x - 1 : current.x + 1;
  }

  if (y_updated) {
    current.y = current.y > parent.y ? current.y - 1 : current.y + 1;
  }
}
