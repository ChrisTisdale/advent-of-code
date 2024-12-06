#include "day6.h"

#include <fstream>
#include <stack>
#include <unordered_set>

using namespace aoc2022::day6;

int solution::run_part1(const std::string& file) {
  return find_marker(file, 4);
}

int solution::run_part2(const std::string& file) {
  return find_marker(file, 14);
}

int solution::find_marker(const std::string& file, size_t length) {
  std::fstream fs(file);
  std::string line;
  std::getline(fs, line);
  std::deque<char> keys;
  std::unordered_set<char> found;
  int count = 0;
  for (const char i : line) {
    ++count;
    auto result = found.insert(i);
    if (!result.second) {
      while (keys.back() != i) {
        found.erase(keys.back());
        keys.pop_back();
      }

      keys.pop_back();
    }

    keys.push_front(i);
    if (keys.size() == length) {
      return count;
    }
  }

  return -1;
}
