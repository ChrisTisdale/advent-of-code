#include <gtest/gtest.h>

#include <fstream>

#include "day13.h"

namespace aoc2022::day13::tests {
TEST(day13Tests2022, round1_sample_results) {
  std::fstream file("sample.txt");
  GTEST_ASSERT_EQ(13, solution::run_part1(file));
}

TEST(day13Tests2022, round2_sample_results) {
  std::fstream file("sample.txt");
  GTEST_ASSERT_EQ(140, solution::run_part2(file));
}

TEST(day13Tests2022, round1_measurements_results) {
  std::fstream file("measurements.txt");
  GTEST_ASSERT_EQ(5806, solution::run_part1(file));
}

TEST(day13Tests2022, round2_measurements_results) {
  std::fstream file("measurements.txt");
  GTEST_ASSERT_EQ(23600, solution::run_part2(file));
}
}  // namespace aoc2022::day13::tests
