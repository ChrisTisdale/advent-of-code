#pragma once

#include <string>
#include <vector>

namespace aoc2024::day2 {
class solution {
 public:
  static long long run_part1(const std::string& file);
  static long long run_part2(const std::string& file);

 private:
  static std::vector<long long> get_values(const std::string& line);
  static std::vector<long long> remove_index(
      const std::vector<long long>& values, std::size_t index);
  static bool is_good_line(const std::vector<long long>& values);
};
}  // namespace aoc2024::day2
