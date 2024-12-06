#include <gtest/gtest.h>

#include "day2.h"

namespace aoc2024::day2::tests {
TEST(day2Tests2024, round1_sample_results) {
  GTEST_ASSERT_EQ(2, solution::run_part1("sample.txt"));
}

TEST(day2Tests2024, round2_sample_results) {
  GTEST_ASSERT_EQ(4, solution::run_part2("sample.txt"));
}

TEST(day2Tests2024, round1_measurements_results) {
  GTEST_ASSERT_EQ(252, solution::run_part1("measurements.txt"));
}

TEST(day2Tests2024, round2_measurements_results) {
  GTEST_ASSERT_EQ(324, solution::run_part2("measurements.txt"));
}
}  // namespace aoc2024::day2::tests
