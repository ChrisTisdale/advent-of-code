#pragma once
#include <istream>

namespace aoc2021::day1 {
class solution {
 public:
  static long long run_part1(std::istream& file);
  static long long run_part2(std::istream& file);

 private:
  static int find_increasing_count(std::istream& file, int window_size);
};
}  // namespace aoc2021::day1
