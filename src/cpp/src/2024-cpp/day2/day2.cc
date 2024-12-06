#include "day2.h"

#include <fstream>
#include <sstream>
#include <vector>

using namespace aoc2024::day2;

enum class State { START, INCREASING, DECREASING };

long long solution::run_part1(const std::string& file) {
  std::ifstream ifs(file);
  std::string line;
  long long safe_lines = 0;
  while (std::getline(ifs, line)) {
    if (is_good_line(get_values(line))) {
      ++safe_lines;
    }
  }

  return safe_lines;
}

long long solution::run_part2(const std::string& file) {
  std::ifstream ifs(file);
  std::string line;
  long long sum = 0;
  while (std::getline(ifs, line)) {
    auto values = get_values(line);
    if (is_good_line(values)) {
      ++sum;
      continue;
    }

    for (std::size_t i = 0; i < values.size(); ++i) {
      if (const auto new_values = remove_index(values, i);
          is_good_line(new_values)) {
        ++sum;
        break;
      }
    }
  }

  return sum;
}
std::vector<long long> solution::get_values(const std::string& line) {
  std::istringstream iss(line);
  std::string token;
  std::vector<long long> values;

  while (std::getline(iss, token, ' ')) {
    std::istringstream current(token);
    long long num;
    current >> num;
    values.push_back(num);
  }

  return values;
}

std::vector<long long> solution::remove_index(
    const std::vector<long long>& values, const std::size_t index) {
  std::vector<long long> new_values;
  new_values.reserve(values.size() - 1);
  for (std::size_t j = 0; j < values.size(); ++j) {
    if (index == j) {
      continue;
    }

    new_values.push_back(values[j]);
  }

  return new_values;
}

bool solution::is_good_line(const std::vector<long long>& values) {
  long long last = -1;
  auto state = State::START;
  for (const long long current : values) {
    if (last == -1) {
      last = current;
    } else {
      switch (state) {
        case State::START:
          if (current == last || current > last && current - last > 3 ||
              current < last && last - current > 3) {
            return false;
          }

          state = current > last ? State::INCREASING : State::DECREASING;
          break;
        case State::INCREASING:
          if (current <= last || current - last > 3) {
            return false;
          }

          break;
        case State::DECREASING:
          if (current >= last || last - current > 3) {
            return false;
          }

          break;
      }

      last = current;
    }
  }

  return true;
}