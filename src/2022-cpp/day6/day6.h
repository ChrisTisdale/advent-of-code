#pragma once
#include <string>

namespace day6 {
class solution {
 public:
  static int run_part1(const std::string& file);
  static int run_part2(const std::string& file);

 private:
  static int find_marker(const std::string& file, size_t length);
};
}  // namespace day6
