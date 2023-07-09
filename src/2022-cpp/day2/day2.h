#pragma once
#include <string>
#include <vector>

namespace day2 {
enum played { rock = 1, paper, scissors };

struct round {
  char opponent;
  char you;

  round(char opponent, char you) : opponent(opponent), you(you) {}
};

class solution {
 public:
  static int runPart1(std::string file);
  static int runPart2(std::string file);

 private:
  static std::vector<round> readFile(std::string file);
  static played convert_opponent(char opponent);
  static played convert_you(char you);
  static played get_strategy(played opponent, char you);
  static int score(played opponent, played you);
};
}  // namespace day2
