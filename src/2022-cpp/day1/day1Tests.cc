#include <gtest/gtest.h>

#include "day1.h"

using namespace day1;

TEST(day1Tests, round1_sample_results) {
  GTEST_ASSERT_EQ(24000, solution::runPart1("sample.txt"));
}

TEST(day1Tests, round2_sample_results) {
  GTEST_ASSERT_EQ(45000, solution::runPart2("sample.txt"));
}

TEST(day1Tests, round1_measurements_results) {
  GTEST_ASSERT_EQ(69912, solution::runPart1("measurements.txt"));
}

TEST(day1Tests, round2_measurements_results) {
  GTEST_ASSERT_EQ(208180, solution::runPart2("measurements.txt"));
}
