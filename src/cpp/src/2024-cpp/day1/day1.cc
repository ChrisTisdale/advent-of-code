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

#include "day1.h"

#include <algorithm>
#include <fstream>
#include <sstream>
#include <vector>

using namespace aoc2024::day1;

long long solution::run_part1(const std::string& file) {
  std::ifstream ifs(file);
  std::string line;
  std::vector<long long> left;
  std::vector<long long> right;
  long long sum = 0;
  while (std::getline(ifs, line)) {
    std::istringstream iss(line);
    long long num;
    iss >> num;
    left.push_back(num);
    iss >> num;
    right.push_back(num);
  }

  std::ranges::sort(left);
  std::ranges::sort(right);

  for (std::size_t i = 0; i < left.size(); ++i) {
    sum += std::abs(right[i] - left[i]);
  }

  return sum;
}

long long solution::run_part2(const std::string& file) {
  std::ifstream ifs(file);
  std::string line;
  std::vector<long long> left;
  std::vector<long long> right;
  long long sum = 0;
  while (std::getline(ifs, line)) {
    std::istringstream iss(line);
    long long num;
    iss >> num;
    left.push_back(num);
    iss >> num;
    right.push_back(num);
  }

  std::vector<long long> count;
  count.reserve(left.size());
  for (const auto i : left) {
    long long total = 0;
    for (const auto j : right) {
      if (i == j) {
        ++total;
      }
    }

    count.push_back(total);
  }

  for (std::size_t i = 0; i < count.size(); ++i) {
    sum += count[i] * left[i];
  }

  return sum;
}