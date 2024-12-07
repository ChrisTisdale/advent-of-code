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

#include "day12.h"

#include <sstream>

using namespace aoc2022::day12;

long long solution::run_part1(std::istream& file) {
  auto map = read_file(file);
  graph_queue queue;
  add_points(map, map.start, queue, 0);
  return get_end_distance(map, queue);
}

long long solution::run_part2(std::istream& file) {
  auto map = read_file(file);
  graph_queue queue;
  for (const auto& row : map.points) {
    for (const auto& col : row) {
      if (col.value == 'a') {
        add_points(map, col, queue, 0);
      }
    }
  }

  return get_end_distance(map, queue);
}

long long solution::get_end_distance(const hill& h, graph_queue queue) {
  std::vector<point> found;
  found.push_back(h.start);
  while (!queue.empty()) {
    const auto c = queue.top();
    queue.pop();
    if (std::find(found.begin(), found.end(), c.p) != found.end()) {
      continue;
    }

    if (c.p == h.end) {
      return c.dist;
    }

    add_points(h, c.p, queue, c.dist);
    found.push_back(c.p);
  }

  return -1;
}

hill solution::read_file(std::istream& file) {
  point start{};
  point end{};
  std::vector<std::vector<point>> points;
  char value;
  int x = 0;
  int y = 0;
  std::string line;
  while (std::getline(file, line)) {
    std::stringstream ss(line);
    points.emplace_back();
    while (ss.read(&value, 1)) {
      switch (value) {
        case 'S':
          value = 'a';
          start = point(value, x, y);
          break;
        case 'E':
          value = 'z';
          end = point(value, x, y);
          break;
        default:
          break;
      }

      points[x].emplace_back(value, x, y);
      y++;
    }

    ++x;
    y = 0;
  }

  return hill(points, start, end);
}

void solution::add_points(const hill& h, const point& p, graph_queue& queue,
                          int offset) {
  if (p.x > 0 && (h.points[p.x - 1][p.y].value - p.value) <= 1) {
    queue.emplace(h.points[p.x - 1][p.y], 1 + offset);
  }
  if (p.x < (h.points.size() - 1) &&
      (h.points[p.x + 1][p.y].value - p.value) <= 1) {
    queue.emplace(h.points[p.x + 1][p.y], 1 + offset);
  }
  if (p.y < (h.points[0].size() - 1) &&
      (h.points[p.x][p.y + 1].value - p.value) <= 1) {
    queue.emplace(h.points[p.x][p.y + 1], 1 + offset);
  }
  if (p.y > 0 && (h.points[p.x][p.y - 1].value - p.value) <= 1) {
    queue.emplace(h.points[p.x][p.y - 1], 1 + offset);
  }
}
