#include "day2.h"

#include <fstream>
#include <numeric>
#include <sstream>

using namespace day2;

int solution::runPart1(std::string file) {
  auto game = readFile(file);
  int result = 0;
  for (auto r : game) {
    result += score(convert_opponent(r.opponent), convert_you(r.you));
  }

  return result;
}

int solution::runPart2(std::string file) {
  auto game = readFile(file);
  int result = 0;
  for (auto r : game) {
    auto they_played = convert_opponent(r.opponent);
    result +=
        score(convert_opponent(r.opponent), get_strategy(they_played, r.you));
  }

  return result;
}

std::vector<round> solution::readFile(std::string file) {
  std::string line;
  std::vector<round> game;
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
    game.push_back(round(first, second));
  }

  return game;
}

played solution::convert_opponent(char opponent) {
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

played solution::convert_you(char you) {
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

played solution::get_strategy(played opponent, char you) {
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

int solution::score(played opponent, played you) {
  if (opponent == you) return 3 + (int)you;
  if (opponent == played::paper && you == played::rock) return (int)you;
  if (opponent == played::rock && you == played::scissors) return (int)you;
  if (opponent == played::scissors && you == played::paper) return (int)you;
  return 6 + (int)you;
}