#include <fstream>
#include <iostream>

#include "day12.h"

using namespace day12;

int main(const int argc, char *argv[]) {
  if (argc != 2) {
    std::cout << "You must provide the file to load " << argc << "\n";
    return -1;
  }

  std::fstream file(argv[1]);
  auto res = solution::run_part1(file);
  std::cout << "Part 1 Results:" << res << "\n";
  file.seekg(0);
  res = solution::run_part2(file);
  std::cout << "Part 2 Results:" << res << "\n";
  return 0;
}
