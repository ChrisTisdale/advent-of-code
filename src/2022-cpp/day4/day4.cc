#include "day4.h"

#include <fstream>
#include <sstream>
#include <unordered_set>

using namespace day4;

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
