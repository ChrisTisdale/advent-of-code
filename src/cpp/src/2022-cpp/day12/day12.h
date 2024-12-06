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
