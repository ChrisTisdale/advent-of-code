#include <gtest/gtest.h>

#include <fstream>

#include "day12.h"

namespace aoc2022::day12::tests {
TEST(day12Tests2022, round1_sample_results) {
  std::fstream file("sample.txt");
  GTEST_ASSERT_EQ(31, solution::run_part1(file));
}

TEST(day12Tests2022, round2_sample_results) {
  std::fstream file("sample.txt");
  GTEST_ASSERT_EQ(29, solution::run_part2(file));
}

TEST(day12Tests2022, round1_measurements_results) {
  std::fstream file("measurements.txt");
  GTEST_ASSERT_EQ(425, solution::run_part1(file));
}

TEST(day12Tests2022, round2_measurements_results) {
  std::fstream file("measurements.txt");
  GTEST_ASSERT_EQ(418, solution::run_part2(file));
}
}  // namespace aoc2022::day12::tests
