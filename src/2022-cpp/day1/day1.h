#pragma once
#include <memory>
#include <numeric>
#include <string>
#include <vector>

namespace day1 {
struct elf {
  std::vector<int> calories;

  const long long total() {
    return std::accumulate(calories.begin(), calories.end(), 0LL);
  }
};

class solution {
 public:
  static int runPart1(std::string file);
  static int runPart2(std::string file);

 private:
  static std::vector<std::shared_ptr<elf>> readFile(std::string file);
};
}  // namespace day1
