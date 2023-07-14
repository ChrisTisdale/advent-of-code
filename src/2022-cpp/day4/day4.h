#pragma once
#include <string>
#include <vector>

namespace day4 {
struct assignment {
  int start;
  int stop;

  explicit assignment(int start, int stop) : start(start), stop(stop) {}
};

struct section {
  assignment elf1;
  assignment elf2;

  explicit section(assignment elf1, assignment elf2) : elf1(elf1), elf2(elf2) {}
};

class solution {
 public:
  static int run_part1(const std::string& file);
  static int run_part2(const std::string& file);

 private:
  static std::vector<section> read_file(const std::string& file);
};
}  // namespace day4
