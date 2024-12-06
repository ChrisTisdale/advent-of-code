#include <gtest/gtest.h>

#include "day6.h"

namespace aoc2022::day6::tests {
TEST(day6Tests2022, round1_sample_results) {
  GTEST_ASSERT_EQ(11, solution::run_part1("sample.txt"));
}

TEST(day6Tests2022, round2_sample_results) {
  GTEST_ASSERT_EQ(26, solution::run_part2("sample.txt"));
}

TEST(day6Tests2022, round1_measurements_results) {
  GTEST_ASSERT_EQ(1850, solution::run_part1("measurements.txt"));
}

TEST(day6Tests2022, round2_measurements_results) {
  GTEST_ASSERT_EQ(2823, solution::run_part2("measurements.txt"));
}
}  // namespace aoc2022::day6::tests
