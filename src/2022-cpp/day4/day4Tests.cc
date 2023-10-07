#include <gtest/gtest.h>

#include "day4.h"

namespace aoc2022::day4::tests {
TEST(day4Tests2022, round1_sample_results) {
  GTEST_ASSERT_EQ(2, solution::run_part1("sample.txt"));
}

TEST(day4Tests2022, round2_sample_results) {
  GTEST_ASSERT_EQ(4, solution::run_part2("sample.txt"));
}

TEST(day4Tests2022, round1_measurements_results) {
  GTEST_ASSERT_EQ(498, solution::run_part1("measurements.txt"));
}

TEST(day4Tests2022, round2_measurements_results) {
  GTEST_ASSERT_EQ(859, solution::run_part2("measurements.txt"));
}
}  // namespace aoc2022::day4::tests
