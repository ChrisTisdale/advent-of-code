#include "day2.h"

#include <fstream>
#include <sstream>

using namespace day2;

int solution::run_part1(const std::string& file) {
  const auto game = read_file(file);
  int result = 0;
  for (const auto& r : game) {
    result += score(convert_opponent(r.opponent), convert_you(r.you));
  }

  return result;
}

int solution::run_part2(const std::string& file) {
  const auto game = read_file(file);
  int result = 0;
  for (const auto& r : game) {
    const auto they_played = convert_opponent(r.opponent);
    result +=
        score(convert_opponent(r.opponent), get_strategy(they_played, r.you));
  }

  return result;
}

std::vector<game_round> solution::read_file(const std::string& file) {
  std::string line;
  std::vector<game_round> game;
  std::ifstream f(file);
  while (std::getline(f, line)) {
    if (line.empty()) {
      break;
    }

    char first, second;
    std::stringstream ss(line);
    ss >> first;
    ss >> second;
    ss >> second;
    game.emplace_back(first, second);
  }

  return game;
}

played solution::convert_opponent(const char opponent) {
  switch (opponent) {
    case 'A':
    case 'a':
      return rock;
    case 'B':
    case 'b':
      return paper;
    default:
      return scissors;
  }
}

played solution::convert_you(const char you) {
  switch (you) {
    case 'X':
    case 'x':
      return rock;
    case 'Y':
    case 'y':
      return paper;
    default:
      return scissors;
  }
}

played solution::get_strategy(const played opponent, const char you) {
  switch (you) {
    case 'X':
    case 'x':
      switch (opponent) {
        case rock:
          return scissors;
        case scissors:
          return paper;
        default:
          return rock;
      }
    case 'Y':
    case 'y':
      return opponent;
    default:
      switch (opponent) {
        case rock:
          return paper;
        case scissors:
          return rock;
        default:
          return scissors;
      }
  }
}

int solution::score(const played opponent, const played you) {
  if (opponent == you) return 3 + static_cast<int>(you);
  if (opponent == paper && you == rock) return you;
  if (opponent == rock && you == scissors) return you;
  if (opponent == scissors && you == paper) return you;
  return 6 + static_cast<int>(you);
}
