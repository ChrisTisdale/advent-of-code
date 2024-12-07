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

#include "day1.h"

namespace aoc2024::day1::tests {
TEST(day1Tests2024, round1_sample_results) {
  GTEST_ASSERT_EQ(11, solution::run_part1("sample.txt"));
}

TEST(day1Tests2024, round2_sample_results) {
  GTEST_ASSERT_EQ(31, solution::run_part2("sample.txt"));
}

TEST(day1Tests2024, round1_measurements_results) {
  GTEST_ASSERT_EQ(1258579, solution::run_part1("measurements.txt"));
}

TEST(day1Tests2024, round2_measurements_results) {
  GTEST_ASSERT_EQ(23981443, solution::run_part2("measurements.txt"));
}
}  // namespace aoc2024::day1::tests
