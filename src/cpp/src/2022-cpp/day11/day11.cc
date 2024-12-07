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

#include "day11.h"

#include <algorithm>
#include <numeric>
#include <sstream>

using namespace aoc2022::day11;

long long solution::run_part1(std::istream& file) {
  auto result = read_file(file);
  return run_game(result, true, 20);
}

long long solution::run_part2(std::istream& file) {
  auto result = read_file(file);
  return run_game(result, false, 10000);
}

long long solution::get_input(operations type, std::string& line) {
  size_t index;
  switch (type) {
    case operations::add:
      index = line.find('+');
      break;
    case operations::multiply:
      index = line.find('*');
      break;
    case operations::subtract:
      index = line.find('-');
      break;
    default:
      index = line.find('/');
      break;
  }

  char* end;
  auto search = line.find("old", index + 1);
  if (search != std::string::npos) {
    return -1;
  } else {
    return strtoll(line.c_str() + index + 1, &end, 10);
  }
}

long long solution::get_divisor(std::string& line) {
  auto index = line.find(" by ") + 4;
  char* end;
  return strtoll(line.c_str() + index, &end, 10);
}

size_t solution::get_false_value(std::string& line) {
  auto index = line.find("to monkey ") + 10;
  char* end;
  return strtoll(line.c_str() + index, &end, 10);
}

size_t solution::get_true_value(std::string& line) {
  auto index = line.find("to monkey ") + 10;
  char* end;
  return strtoll(line.c_str() + index, &end, 10);
}

operations solution::get_operation_type(std::string& line) {
  if (line.contains('+')) return operations::add;
  if (line.contains('*')) return operations::multiply;
  if (line.contains('-')) return operations::subtract;
  return operations::divide;
}

monkey solution::build_monkey(std::istream& file,
                              std::queue<long long>&& init) {
  std::string line;
  std::getline(file, line);
  auto type = get_operation_type(line);
  auto value = get_input(type, line);
  std::getline(file, line);
  auto divisor = get_divisor(line);
  std::getline(file, line);
  auto true_value = get_true_value(line);
  std::getline(file, line);
  auto false_value = get_false_value(line);
  return {std::move(init),
          operation(false_value, true_value, divisor, value, type)};
}

std::queue<long long> solution::get_start(std::string& line) {
  std::queue<long long> init;
  std::stringstream ss(line);
  std::string temp;
  std::getline(ss, temp, ':');
  char* end;
  while (std::getline(ss, temp, ',')) {
    auto value = strtoll(temp.c_str(), &end, 10);
    init.push(value);
  }

  return init;
}

std::vector<monkey> solution::read_file(std::istream& file) {
  std::vector<monkey> monkeys;
  std::string line;
  while (true) {
    if (!std::getline(file, line) || !std::getline(file, line)) {
      return monkeys;
    }

    monkeys.push_back(build_monkey(file, get_start(line)));
    std::getline(file, line);
  }
}

long long solution::run_game(std::vector<monkey>& result, bool damage,
                             int loop_count) {
  std::vector<long long> inspected(result.size());
  std::generate(inspected.begin(), inspected.end(), [] { return 0; });
  long long factor = damage ? 1 : get_factor(result);
  for (int i = 0; i < loop_count; ++i) {
    size_t index = 0;
    for (auto& m : result) {
      while (!m.items.empty()) {
        auto value = m.items.front();
        m.items.pop();
        value = m.handler.get_result(value);
        value = damage ? value / 3 : value % factor;
        auto pass = m.handler.eval(value);
        result[pass].items.push(value);
        inspected[index]++;
      }

      ++index;
    }
  }

  std::sort(inspected.begin(), inspected.end(),
            [](const auto l, const auto r) { return l > r; });
  long long first = inspected[0];
  long long second = inspected[1];
  return first * second;
}

long long solution::get_factor(const std::vector<monkey>& result) {
  return std::accumulate(
      result.begin(), result.end(), 1ll,
      [](const auto l, const auto& r) { return l * r.handler.get_divisor(); });
}
