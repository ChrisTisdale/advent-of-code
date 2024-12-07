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

#include "day4.h"

#include <fstream>
#include <sstream>
#include <unordered_set>

using namespace aoc2022::day4;

int solution::run_part1(const std::string& file) {
  auto sections = read_file(file);
  int count = 0;
  for (auto section : sections) {
    if ((section.elf1.start <= section.elf2.start &&
         section.elf1.stop >= section.elf2.stop) ||
        (section.elf2.start <= section.elf1.start &&
         section.elf2.stop >= section.elf1.stop)) {
      ++count;
    }
  }

  return count;
}

int solution::run_part2(const std::string& file) {
  auto sections = read_file(file);
  int count = 0;
  for (auto section : sections) {
    if ((section.elf1.start <= section.elf2.start &&
         section.elf1.stop >= section.elf2.start) ||
        (section.elf2.start <= section.elf1.start &&
         section.elf2.stop >= section.elf1.start)) {
      ++count;
    }
  }

  return count;
}

std::vector<section> solution::read_file(const std::string& file) {
  std::vector<section> sections;
  std::string line;
  std::ifstream f(file);
  std::string discard;
  while (std::getline(f, line)) {
    std::stringstream ss(line);
    int start, stop;
    ss >> start;
    std::getline(ss, line, '-');
    ss >> stop;
    auto elf1 = assignment(start, stop);
    std::getline(ss, line, ',');
    ss >> start;
    std::getline(ss, line, '-');
    ss >> stop;
    auto elf2 = assignment(start, stop);
    sections.emplace_back(elf1, elf2);
  }

  return sections;
}
