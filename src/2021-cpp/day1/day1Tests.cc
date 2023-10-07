#include <gtest/gtest.h>

#include <fstream>

#include "day1.h"

namespace aoc2021::day1::tests {
TEST(day1Tests2021, round1_sample_results) {
  std::fstream file("sample.txt");
  GTEST_ASSERT_EQ(7, solution::run_part1(file));
}

TEST(day1Tests2021, round2_sample_results) {
  std::fstream file("sample.txt");
  GTEST_ASSERT_EQ(5, solution::run_part2(file));
}

TEST(day1Tests2021, round1_measurements_results) {
  std::fstream file("measurements.txt");
  GTEST_ASSERT_EQ(1722, solution::run_part1(file));
}

TEST(day1Tests2021, round2_measurements_results) {
  std::fstream file("measurements.txt");
  GTEST_ASSERT_EQ(1748, solution::run_part2(file));
}
}  // namespace aoc2021::day1::tests
