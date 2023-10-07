#include "day10.h"

#include <cstring>
#include <sstream>

using namespace aoc2022::day10;

long long solution::run_part1(std::istream& file) {
  std::string line;
  std::string buffer;
  int pc = 0;
  int x = 1;
  int result = 0;
  int search_value = 20;
  while (std::getline(file, line)) {
    std::stringstream ss(line);
    std::getline(ss, buffer, ' ');
    for (int i = 0; i < 2; ++i) {
      pc += 1;
      if (pc == search_value) {
        auto res = pc * x;
        result += res;
        search_value += 40;
      }

      if (buffer == "noop") {
        ++i;
      } else if (i == 1) {
        int value;
        ss >> value;
        x += value;
      }
    }
  }

  return result;
}

std::string solution::run_part2(std::istream& file) {
  std::string line;
  std::string buffer;
  int pc = 0;
  int x = 1;
  int col = 0;
  std::string screen;
  char row[41];
  std::memset(row, 0, sizeof(row));
  while (std::getline(file, line)) {
    std::stringstream ss(line);
    std::getline(ss, buffer, ' ');
    for (int i = 0; i < 2; ++i) {
      auto input = pc % 40 - x + 1;
      row[col++] = (input >= 0 && input < 3) ? '#' : '.';
      if (col == 40) {
        col = 0;
        screen.append(row);
        screen.append("\n");
        std::memset(row, 0, sizeof(row));
      }

      pc += 1;
      if (buffer == "noop") {
        ++i;
      } else if (i == 1) {
        int value;
        ss >> value;
        x += value;
      }
    }
  }

  return screen;
}
