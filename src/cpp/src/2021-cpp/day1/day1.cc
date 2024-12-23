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

#include <functional>
#include <numeric>
#include <sstream>
#include <vector>

using namespace aoc2021::day1;

long long solution::run_part1(std::istream& file) {
  const auto result = find_increasing_count(file, 1);
  return result;
}

long long solution::run_part2(std::istream& file) {
  const auto result = find_increasing_count(file, 3);
  return result;
}

int solution::find_increasing_count(std::istream& file, int window_size) {
  std::string line;
  std::vector<int> inputs;

  while (std::getline(file, line)) {
    std::stringstream ss(line);
    int input;
    ss >> input;
    inputs.push_back(input);
  }

  int count = 0;
  const std::plus<> op;
  for (int i = 0; i < inputs.size() - window_size; ++i) {
    const auto array_start = inputs.begin();
    auto current =
        std::accumulate(array_start + i, array_start + i + window_size, 0, op);
    auto next = std::accumulate(array_start + i + 1,
                                array_start + i + window_size + 1, 0, op);
    if (next > current) {
      ++count;
    }
  }

  return count;
}
