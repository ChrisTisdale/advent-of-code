#include <gtest/gtest.h>

#include "day8.h"

namespace day8::tests {
TEST(day8Tests, round1_sample_results) {
  GTEST_ASSERT_EQ(21, solution::run_part1("sample.txt"));
}

TEST(day8Tests, round2_sample_results) {
  GTEST_ASSERT_EQ(8, solution::run_part2("sample.txt"));
}

TEST(day8Tests, round1_measurements_results) {
  GTEST_ASSERT_EQ(1719, solution::run_part1("measurements.txt"));
}

TEST(day8Tests, round2_measurements_results) {
  GTEST_ASSERT_EQ(590824, solution::run_part2("measurements.txt"));
}
}  // namespace day8::tests
