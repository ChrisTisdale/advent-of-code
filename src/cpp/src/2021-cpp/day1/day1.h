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

#pragma once
#include <istream>

namespace aoc2021::day1 {
class solution {
 public:
  static long long run_part1(std::istream& file);
  static long long run_part2(std::istream& file);

 private:
  static int find_increasing_count(std::istream& file, int window_size);
};
}  // namespace aoc2021::day1
