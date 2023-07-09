#pragma once
#include <string>
#include <vector>

namespace day2 {
enum played { rock = 1, paper, scissors };

struct game_round {
  char opponent;
  char you;

  game_round(const char opponent, const char you) : opponent(opponent), you(you) {}
};

class solution {
 public:
  static int run_part1(const std::string& file);
  static int run_part2(const std::string& file);

 private:
  static std::vector<game_round> read_file(const std::string& file);
  static played convert_opponent(const char opponent);
  static played convert_you(const char you);
  static played get_strategy(const played opponent, const char you);
  static int score(const played opponent, const played you);
};
}  // namespace day2
