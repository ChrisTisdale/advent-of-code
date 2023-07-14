#include <gtest/gtest.h>

#include "day5.h"

namespace day5::tests {
TEST(day5Tests, round1_sample_results) {
  GTEST_ASSERT_EQ("CMZ", solution::run_part1("sample.txt"));
}

TEST(day5Tests, round2_sample_results) {
  GTEST_ASSERT_EQ("MCD", solution::run_part2("sample.txt"));
}

TEST(day5Tests, round1_measurements_results) {
  GTEST_ASSERT_EQ("CFFHVVHNC", solution::run_part1("measurements.txt"));
}

TEST(day5Tests, round2_measurements_results) {
  GTEST_ASSERT_EQ("FSZWBPTBG", solution::run_part2("measurements.txt"));
}
}  // namespace day5::tests
