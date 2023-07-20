#include <gtest/gtest.h>

#include "day7.h"

namespace day7::tests {
TEST(day7Tests, round1_sample_results) {
  GTEST_ASSERT_EQ(95437, solution::run_part1("sample.txt"));
}

TEST(day7Tests, round2_sample_results) {
  GTEST_ASSERT_EQ(24933642, solution::run_part2("sample.txt"));
}

TEST(day7Tests, round1_measurements_results) {
  GTEST_ASSERT_EQ(1490523, solution::run_part1("measurements.txt"));
}

TEST(day7Tests, round2_measurements_results) {
  GTEST_ASSERT_EQ(12390492, solution::run_part2("measurements.txt"));
}
}  // namespace day7::tests
