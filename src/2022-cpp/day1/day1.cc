#include "day1.h"

#include <algorithm>
#include <fstream>
#include <iostream>
#include <sstream>

using namespace day1;

long long solution::run_part1(const std::string& file) {
  const auto elfs = read_file(file);
  long long max = 0;
  for (const auto& e : elfs) {
    max = std::max(e->total(), max);
  }

  return max;
}

long long solution::run_part2(const std::string& file) {
  auto elfs = read_file(file);
  std::sort(elfs.begin(), elfs.end(),
            [](const auto l, const auto r) { return l->total() > r->total(); });
  long long sum = 0;
  for (int i = 0; i < std::min(3, static_cast<int>(elfs.size())); ++i) {
    sum += elfs[i]->total();
  }

  return sum;
}

std::vector<std::shared_ptr<elf>> solution::read_file(const std::string& file) {
  std::string line;
  std::vector<std::shared_ptr<elf>> elfs;
  std::ifstream f(file);
  auto current = std::make_shared<elf>();
  while (std::getline(f, line)) {
    if (line.empty()) {
      if (!current->calories.empty()) {
        elfs.push_back(current);
      }

      current = std::make_shared<elf>();
      continue;
    }

    int calories;
    std::stringstream ss(line);
    ss >> calories;
    current->calories.push_back(calories);
  }

  if (!current->calories.empty()) {
    elfs.push_back(current);
  }

  return elfs;
}
