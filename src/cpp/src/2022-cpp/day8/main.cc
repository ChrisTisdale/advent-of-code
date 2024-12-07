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

#include <iostream>

#include "day8.h"

using namespace aoc2022::day8;

int main(const int argc, char *argv[]) {
  if (argc != 2) {
    std::cout << "You must provide the file to load " << argc << "\n";
    return -1;
  }

  auto res = solution::run_part1(std::string(argv[1]));
  std::cout << "Part 1 Results:" << res << "\n";
  res = solution::run_part2(std::string(argv[1]));
  std::cout << "Part 2 Results:" << res << "\n";
  return 0;
}
