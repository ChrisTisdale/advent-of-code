#pragma once
#include <queue>
#include <string>
#include <utility>

namespace aoc2022::day5 {
struct player {
  std::deque<char> crane;
};

struct move {
  int count;
  int from_player;
  int to_player;

  explicit move(int count, int from_player, int to_player)
      : count(count), from_player(from_player), to_player(to_player) {}
};

struct game {
  std::vector<player> players;
  std::vector<move> moves;

  explicit game(std::vector<player> players, std::vector<move> moves)
      : players(std::move(players)), moves(std::move(moves)) {}
};

class solution {
 public:
  static std::string run_part1(const std::string& file);
  static std::string run_part2(const std::string& file);

 private:
  static game read_file(const std::string& file);
};
}  // namespace aoc2022::day5
