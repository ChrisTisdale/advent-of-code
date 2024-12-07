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
#include <memory>
#include <numeric>
#include <string>
#include <vector>

namespace aoc2022::day1 {
struct elf {
  std::vector<int> calories;

  long long total() {
    return std::accumulate(calories.begin(), calories.end(), 0LL);
  }
};

class solution {
 public:
  static long long run_part1(const std::string& file);
  static long long run_part2(const std::string& file);

 private:
  static std::vector<std::shared_ptr<elf>> read_file(const std::string& file);
};
}  // namespace aoc2022::day1
