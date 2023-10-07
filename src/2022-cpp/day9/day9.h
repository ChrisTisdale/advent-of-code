#pragma once

#include <istream>
#include <tuple>
#include <vector>

namespace aoc2022::day9 {
struct point {
  long long x;
  long long y;

  point() : x(0), y(0) {}

  explicit point(long long x, long long y) : x(x), y(y) {}

  bool operator==(const point& rhs) const {
    return std::tie(x, y) == std::tie(rhs.x, rhs.y);
  }

  bool operator!=(const point& rhs) const { return !(rhs == *this); }
  bool operator<(const point& rhs) const {
    return std::tie(x, y) < std::tie(rhs.x, rhs.y);
  }

  bool operator>(const point& rhs) const { return rhs < *this; }
  bool operator<=(const point& rhs) const { return !(rhs < *this); }
  bool operator>=(const point& rhs) const { return !(*this < rhs); }
};

struct input {
  char direction;
  std::size_t count;

  explicit input(char direction, std::size_t count)
      : direction(direction), count(count) {}
};

class solution {
 public:
  static long long run_part1(std::istream& file);
  static long long run_part2(std::istream& file);

 private:
  static std::vector<input> read_file(std::istream& file);
  static long long find_unique_spaces(const std::vector<input>& data,
                                      std::size_t middle_count);
  static void update_current_location(const point& parent, point& current);
};
}  // namespace aoc2022::day9
