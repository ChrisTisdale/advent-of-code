#include "day3.h"

#include <fstream>
#include <sstream>
#include <unordered_set>

using namespace day3;

int solution::run_part1(const std::string& file) {
  auto sacks = read_file(file);
  int count = 0;
  for (const auto& sack : sacks) {
    std::unordered_set<char> values;
    std::unordered_set<char> looked;
    const std::string& data = sack.data;
    for (std::size_t i = 0; i < data.length() / 2; ++i) {
      values.insert(data[i]);
    }

    for (std::size_t i = data.length() / 2; i < data.length(); ++i) {
      auto v = data[i];
      if (values.contains(v) && looked.insert(v).second) {
        count += get_value(v);
      }
    }
  }

  return count;
}

int solution::run_part2(const std::string& file) {
  auto sacks = read_file(file);
  int count = 0;
  for (std::size_t i = 0; i < sacks.size() - 2; i += 3) {
    std::unordered_set<char> values;
    std::unordered_set<char> current;
    std::string& data = sacks[i].data;
    for (char j : data) {
      values.insert(j);
    }

    data = sacks[i + 1].data;
    for (char v : data) {
      if (values.contains(v)) {
        current.insert(v);
      }
    }

    data = sacks[i + 2].data;
    values.clear();
    for (char v : data) {
      if (current.contains(v)) {
        values.insert(v);
      }
    }

    for (const auto c : values) {
      count += get_value(c);
    }
  }

  return count;
}

std::vector<rucksack> solution::read_file(const std::string& file) {
  std::vector<rucksack> sacks;
  std::string line;
  std::ifstream f(file);
  while (std::getline(f, line)) {
    sacks.emplace_back(line);
  }

  return sacks;
}

int solution::get_value(char character) {
  return character > 'Z' ? 1 + (character - 'a') : 27 + (character - 'A');
}
