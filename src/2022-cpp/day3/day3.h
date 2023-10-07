#pragma once
#include <string>
#include <vector>

namespace aoc2022::day3 {
struct rucksack {
  std::string data;

  explicit rucksack(std::string data) : data(std::move(data)) {}
};

class solution {
 public:
  static int run_part1(const std::string& file);
  static int run_part2(const std::string& file);

 private:
  static std::vector<rucksack> read_file(const std::string& file);
  static int get_value(char character);
};
}  // namespace aoc2022::day3
