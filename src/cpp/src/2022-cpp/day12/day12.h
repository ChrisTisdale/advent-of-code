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
#include <istream>
#include <memory>
#include <queue>
#include <string>
#include <vector>

namespace aoc2022::day12 {
struct point {
  char value;
  int x;
  int y;

  explicit point(char value, int x, int y) : value(value), x(x), y(y) {}
  point() = default;
  point(point&& other) = default;
  point(const point& other) = default;
  point& operator=(const point&) = default;
  bool operator==(const point& rhs) const {
    return std::tie(value, x, y) == std::tie(rhs.value, rhs.x, rhs.y);
  }
  bool operator!=(const point& rhs) const { return !(rhs == *this); }
  bool operator<(const point& rhs) const {
    return std::tie(value, x, y) < std::tie(rhs.value, rhs.x, rhs.y);
  }
  bool operator>(const point& rhs) const { return rhs < *this; }
  bool operator<=(const point& rhs) const { return !(rhs < *this); }
  bool operator>=(const point& rhs) const { return !(*this < rhs); }
};

struct distance {
  point p;
  int dist;

  explicit distance(point p, int dist) : p(p), dist(dist) {}
  bool operator<(const distance& rhs) const { return dist > rhs.dist; }
  bool operator>(const distance& rhs) const { return rhs < *this; }
  bool operator<=(const distance& rhs) const { return !(rhs < *this); }
  bool operator>=(const distance& rhs) const { return !(*this < rhs); }
  bool operator==(const distance& rhs) const {
    return std::tie(p, dist) == std::tie(rhs.p, rhs.dist);
  }
  bool operator!=(const distance& rhs) const { return !(rhs == *this); }
};

struct hill {
  std::vector<std::vector<point>> points;
  point start;
  point end;

  explicit hill(std::vector<std::vector<point>> points, point start, point end)
      : points(std::move(points)), start(start), end(end) {}
  hill() : points({}), start({}), end({}){};
};

typedef std::priority_queue<distance> graph_queue;

class solution {
 public:
  static long long run_part1(std::istream& file);
  static long long run_part2(std::istream& file);

 private:
  static hill read_file(std::istream& file);
  static long long get_end_distance(const hill& h, graph_queue queue);
  static void add_points(const hill& h, const point& p, graph_queue& queue,
                         int offset);
};
}  // namespace aoc2022::day12
