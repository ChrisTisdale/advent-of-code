#include <gtest/gtest.h>

#include "day3.h"

namespace aoc2022::day3::tests {
TEST(day3Tests2022, round1_sample_results) {
  GTEST_ASSERT_EQ(157, solution::run_part1("sample.txt"));
}

TEST(day3Tests2022, round2_sample_results) {
  GTEST_ASSERT_EQ(70, solution::run_part2("sample.txt"));
}

TEST(day3Tests2022, round1_measurements_results) {
  GTEST_ASSERT_EQ(7746, solution::run_part1("measurements.txt"));
}

TEST(day3Tests2022, round2_measurements_results) {
  GTEST_ASSERT_EQ(2604, solution::run_part2("measurements.txt"));
}
}  // namespace aoc2022::day3::tests
