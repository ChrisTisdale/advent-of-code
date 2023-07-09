#include <algorithm>
#include <iostream>

#include "day1.h"

using namespace day1;

int main(int argc, char *argv[]) {
  if (argc != 2) {
    std::cout << "You must provde the file to load " << argc << "\n";
    return -1;
  }

  auto res = solution::runPart1(std::string(argv[1]));
  std::cout << "Part 1 Results:" << res << "\n";
  res = solution::runPart2(std::string(argv[1]));
  std::cout << "Part 1 Results:" << res << "\n";
  return 0;
}
