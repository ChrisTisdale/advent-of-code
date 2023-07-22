#include <iostream>

#include "day8.h"

using namespace day8;

int main(const int argc, char *argv[]) {
  if (argc != 2) {
    std::cout << "You must provide the file to load " << argc << "\n";
    return -1;
  }

  auto res = solution::run_part1(std::string(argv[1]));
  std::cout << "Part 1 Results:" << res << "\n";
  res = solution::run_part2(std::string(argv[1]));
  std::cout << "Part 1 Results:" << res << "\n";
  return 0;
}
