#include <gtest/gtest.h>

#include "day1.h"

namespace day1::tests {
TEST(day1Tests, round1_sample_results) {
  GTEST_ASSERT_EQ(24000, solution::run_part1("sample.txt"));
}

TEST(day1Tests, round2_sample_results) {
  GTEST_ASSERT_EQ(45000, solution::run_part2("sample.txt"));
}

TEST(day1Tests, round1_measurements_results) {
  GTEST_ASSERT_EQ(69912, solution::run_part1("measurements.txt"));
}

TEST(day1Tests, round2_measurements_results) {
  GTEST_ASSERT_EQ(208180, solution::run_part2("measurements.txt"));
}
}  // namespace day1::tests
