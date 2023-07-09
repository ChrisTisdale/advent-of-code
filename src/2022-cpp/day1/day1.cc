#include "day1.h"

#include <algorithm>
#include <fstream>
#include <iostream>
#include <sstream>

using namespace day1;

int solution::runPart1(std::string file) {
  auto elfs = readFile(file);
  long long max = 0;
  for (auto e : elfs) {
    max = std::max(e->total(), max);
  }

  return max;
}

int solution::runPart2(std::string file) {
  auto elfs = readFile(file);
  std::sort(elfs.begin(), elfs.end(),
            [](auto l, auto r) { return l->total() > r->total(); });
  int sum = 0;
  for (int i = 0; i < std::min(3, static_cast<int>(elfs.size())); ++i) {
    sum += elfs[i]->total();
  }

  return sum;
}

std::vector<std::shared_ptr<elf>> solution::readFile(std::string file) {
  std::string line;
  std::vector<std::shared_ptr<elf>> elfs;
  std::ifstream f(file);
  std::shared_ptr<elf> current = std::make_shared<elf>();
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