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
      return played::rock;
    case 'B':
    case 'b':
      return played::paper;
    default:
      return played::scissors;
  }
}

played solution::convert_you(const char you) {
  switch (you) {
    case 'X':
    case 'x':
      return played::rock;
    case 'Y':
    case 'y':
      return played::paper;
    default:
      return played::scissors;
  }
}

played solution::get_strategy(const played opponent, const char you) {
  switch (you) {
    case 'X':
    case 'x':
      switch (opponent) {
        case played::rock:
          return played::scissors;
        case played::scissors:
          return played::paper;
        default:
          return played::rock;
      }
    case 'Y':
    case 'y':
      return opponent;
    default:
      switch (opponent) {
        case played::rock:
          return played::paper;
        case played::scissors:
          return played::rock;
        default:
          return played::scissors;
      }
  }
}

int solution::score(const played opponent, const played you) {
  const auto your_value = static_cast<int>(you);
  if (opponent == you) return 3 + your_value;
  if (opponent == played::paper && you == played::rock) return your_value;
  if (opponent == played::rock && you == played::scissors) return your_value;
  if (opponent == played::scissors && you == played::paper) return your_value;
  return 6 + your_value;
}
