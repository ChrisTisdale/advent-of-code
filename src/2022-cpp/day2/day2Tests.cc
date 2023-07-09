#include <gtest/gtest.h>

#include "day2.h"

using namespace day2;

TEST(day2Tests, round1_sample_results) {
  GTEST_ASSERT_EQ(15, solution::runPart1("sample.txt"));
}

TEST(day2Tests, round2_sample_results) {
  GTEST_ASSERT_EQ(12, solution::runPart2("sample.txt"));
}

TEST(day2Tests, round1_measurements_results) {
  GTEST_ASSERT_EQ(11906, solution::runPart1("measurements.txt"));
}

TEST(day2Tests, round2_measurements_results) {
  GTEST_ASSERT_EQ(11186, solution::runPart2("measurements.txt"));
}
