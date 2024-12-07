// Copyright (c) Christopher Tisdale 2024.
//
// Licensed under BSD-3-Clause.
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      https://spdx.org/licenses/BSD-3-Clause.html
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
