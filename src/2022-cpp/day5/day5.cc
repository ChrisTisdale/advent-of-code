#include "day5.h"

#include <fstream>
#include <sstream>
#include <stack>

using namespace day5;

std::string solution::run_part1(const std::string& file) {
  auto game = read_file(file);
  auto& players = game.players;
  for (const auto& m : game.moves) {
    auto& from = players[m.from_player - 1];
    auto& to = players[m.to_player - 1];
    for (int i = 0; i < m.count; ++i) {
      to.crane.push_front(from.crane.front());
      from.crane.pop_front();
    }
  }

  std::string result;
  for (const auto& p : players) {
    if (p.crane.empty()) {
      continue;
    }

    result.push_back(p.crane.front());
  }

  return std::move(result);
}

std::string solution::run_part2(const std::string& file) {
  auto game = read_file(file);
  auto& players = game.players;
  for (const auto& m : game.moves) {
    std::stack<char> temp;
    auto& from = players[m.from_player - 1];
    auto& to = players[m.to_player - 1];
    for (int i = 0; i < m.count; ++i) {
      temp.push(from.crane.front());
      from.crane.pop_front();
    }

    while (!temp.empty()) {
      to.crane.push_front(temp.top());
      temp.pop();
    }
  }

  std::string result;
  for (const auto& p : players) {
    if (p.crane.empty()) {
      continue;
    }

    result.push_back(p.crane.front());
  }

  return std::move(result);
}

game solution::read_file(const std::string& file) {
  std::vector<player> players;
  std::vector<move> moves;
  std::string line;
  std::ifstream f(file);
  int current = 0;
  while (std::getline(f, line)) {
    if (!current) {
      if (line.length() == 0) {
        ++current;
      }

      if (!line.contains('[')) {
        continue;
      }

      for (int i = 0; i * 4 < line.length(); ++i) {
        if (i >= players.size()) {
          players.emplace_back();
        }

        if (line[i * 4 + 1] == ' ') {
          continue;
        }

        players[i].crane.emplace_back(line[i * 4 + 1]);
      }
    } else {
      int count, to, from;
      std::string temp;
      std::stringstream ss(line);
      std::getline(ss, line, ' ');
      ss >> count;
      ss >> temp;
      ss >> from;
      ss >> temp;
      ss >> to;
      moves.emplace_back(count, from, to);
    }
  }

  return game(std::move(players), std::move(moves));
}
