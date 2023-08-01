#include <gtest/gtest.h>

#include <fstream>

#include "day9.h"

namespace day9::tests {
TEST(day9Tests, round1_sample_results) {
  std::fstream file("samplePart1.txt");
  GTEST_ASSERT_EQ(13, solution::run_part1(file));
}

TEST(day9Tests, round2_sample_results) {
  std::fstream file("samplePart2.txt");
  GTEST_ASSERT_EQ(36, solution::run_part2(file));
}

TEST(day9Tests, round1_measurements_results) {
  std::fstream file("measurements.txt");
  GTEST_ASSERT_EQ(5683, solution::run_part1(file));
}

TEST(day9Tests, round2_measurements_results) {
  std::fstream file("measurements.txt");
  GTEST_ASSERT_EQ(2372, solution::run_part2(file));
}
}  // namespace day9::tests
