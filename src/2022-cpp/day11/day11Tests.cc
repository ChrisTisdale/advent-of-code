#include <gtest/gtest.h>

#include <fstream>

#include "day11.h"

namespace day11::tests {
TEST(day11Tests, round1_sample_results) {
  std::fstream file("sample.txt");
  GTEST_ASSERT_EQ(10605, solution::run_part1(file));
}

TEST(day11Tests, round2_sample_results) {
  std::fstream file("sample.txt");
  GTEST_ASSERT_EQ(2713310158, solution::run_part2(file));
}

TEST(day11Tests, round1_measurements_results) {
  std::fstream file("measurements.txt");
  GTEST_ASSERT_EQ(50616, solution::run_part1(file));
}

TEST(day11Tests, round2_measurements_results) {
  std::fstream file("measurements.txt");
  GTEST_ASSERT_EQ(11309046332, solution::run_part2(file));
}
}  // namespace day11::tests
