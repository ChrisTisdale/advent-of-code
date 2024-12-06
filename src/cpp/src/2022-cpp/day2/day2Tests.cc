#include <gtest/gtest.h>

#include "day2.h"

namespace aoc2022::day2::tests {
TEST(day2Tests2022, round1_sample_results) {
  GTEST_ASSERT_EQ(15, solution::run_part1("sample.txt"));
}

TEST(day2Tests2022, round2_sample_results) {
  GTEST_ASSERT_EQ(12, solution::run_part2("sample.txt"));
}

TEST(day2Tests2022, round1_measurements_results) {
  GTEST_ASSERT_EQ(11906, solution::run_part1("measurements.txt"));
}

TEST(day2Tests2022, round2_measurements_results) {
  GTEST_ASSERT_EQ(11186, solution::run_part2("measurements.txt"));
}
}  // namespace aoc2022::day2::tests
