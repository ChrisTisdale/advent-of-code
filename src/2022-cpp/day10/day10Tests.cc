#include <gtest/gtest.h>

#include <fstream>

#include "day10.h"

namespace aoc2022::day10::tests {
TEST(day10Tests2022, round1_sample_results) {
  std::fstream file("sample.txt");
  GTEST_ASSERT_EQ(13140, solution::run_part1(file));
}

TEST(day10Tests2022, round2_sample_results) {
  std::fstream file("sample.txt");
  std::string expected =
      "##..##..##..##..##..##..##..##..##..##..\n"
      "###...###...###...###...###...###...###.\n"
      "####....####....####....####....####....\n"
      "#####.....#####.....#####.....#####.....\n"
      "######......######......######......####\n"
      "#######.......#######.......#######.....\n";
  GTEST_ASSERT_EQ(expected, solution::run_part2(file));
}

TEST(day10Tests2022, round1_measurements_results) {
  std::fstream file("measurements.txt");
  GTEST_ASSERT_EQ(13760, solution::run_part1(file));
}

TEST(day10Tests2022, round2_measurements_results) {
  std::fstream file("measurements.txt");
  std::string expected =
      "###..####.#..#.####..##..###..####.####.\n"
      "#..#.#....#.#.....#.#..#.#..#.#....#....\n"
      "#..#.###..##.....#..#....#..#.###..###..\n"
      "###..#....#.#...#...#....###..#....#....\n"
      "#.#..#....#.#..#....#..#.#....#....#....\n"
      "#..#.#....#..#.####..##..#....####.#....\n";
  GTEST_ASSERT_EQ(expected, solution::run_part2(file));
}
}  // namespace aoc2022::day10::tests
