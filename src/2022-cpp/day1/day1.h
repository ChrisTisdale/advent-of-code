#pragma once
#include <memory>
#include <numeric>
#include <string>
#include <vector>

namespace day1 {
struct elf {
  std::vector<int> calories;

  long long total() {
    return std::accumulate(calories.begin(), calories.end(), 0LL);
  }
};

class solution {
 public:
  static long long run_part1(const std::string& file);
  static long long run_part2(const std::string& file);

 private:
  static std::vector<std::shared_ptr<elf>> read_file(const std::string& file);
};
}  // namespace day1
