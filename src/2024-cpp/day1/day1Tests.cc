#include <gtest/gtest.h>

#include "day1.h"

namespace aoc2024::day1::tests {
TEST(day1Tests2024, round1_sample_results) {
  GTEST_ASSERT_EQ(11, solution::run_part1("sample.txt"));
}

TEST(day1Tests2024, round2_sample_results) {
  GTEST_ASSERT_EQ(31, solution::run_part2("sample.txt"));
}

TEST(day1Tests2024, round1_measurements_results) {
  GTEST_ASSERT_EQ(1258579, solution::run_part1("measurements.txt"));
}

TEST(day1Tests2024, round2_measurements_results) {
  GTEST_ASSERT_EQ(23981443, solution::run_part2("measurements.txt"));
}
}  // namespace aoc2024::day1::tests
