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
#include <string>
#include <vector>

namespace aoc2022::day4 {
struct assignment {
  int start;
  int stop;

  explicit assignment(int start, int stop) : start(start), stop(stop) {}
};

struct section {
  assignment elf1;
  assignment elf2;

  explicit section(assignment elf1, assignment elf2) : elf1(elf1), elf2(elf2) {}
};

class solution {
 public:
  static int run_part1(const std::string& file);
  static int run_part2(const std::string& file);

 private:
  static std::vector<section> read_file(const std::string& file);
};
}  // namespace aoc2022::day4
