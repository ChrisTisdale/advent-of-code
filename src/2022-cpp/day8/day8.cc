#include "day8.h"

#include <algorithm>
#include <fstream>
#include <iostream>
#include <numeric>

using namespace day8;

long long solution::run_part1(const std::string& file) {
  auto graph = read_file(file);
  long long count = 0;
  for (const auto& kv : graph) {
    if (is_visible(kv.first, kv.second, graph)) {
      ++count;
    }
  }

  return count;
}

long long solution::run_part2(const std::string& file) {
  auto graph = read_file(file);
  long long max = 0;
  for (const auto& kv : graph) {
    const auto score = calculate_scenic(kv.first, kv.second, graph);
    if (score > max) {
      max = score;
    }
  }

  return max;
}

std::map<tree, std::map<direction, edge>> solution::read_file(
    const std::string& file) {
  std::ifstream f(file);
  std::string line;
  std::map<tree, std::map<direction, edge>> graph;
  std::vector<std::string> lines;
  while (std::getline(f, line)) {
    lines.push_back(line);
  }

  for (std::size_t row = 0; row < lines.size(); ++row) {
    for (std::size_t col = 0; col < lines[row].size(); ++col) {
      tree t(lines[row][col], row, col);
      std::map<direction, edge> edges;
      if (row > 0) {
        edges.emplace(Up, edge(Up, t, tree(lines[row - 1][col], row - 1, col)));
      }

      if (col > 0) {
        edges.emplace(Left,
                      edge(Left, t, tree(lines[row][col - 1], row, col - 1)));
      }

      if (row + 1 < lines.size()) {
        edges.emplace(Down,
                      edge(Down, t, tree(lines[row + 1][col], row + 1, col)));
      }

      if (col + 1 < lines[row].size()) {
        edges.emplace(Right,
                      edge(Right, t, tree(lines[row][col + 1], row, col + 1)));
      }

      graph.emplace(t, edges);
    }
  }

  return std::move(graph);
}

bool solution::is_visible(
    const tree& current, const std::map<direction, edge>& edges,
    const std::map<tree, std::map<direction, edge>>& graph) {
  if (edges.size() != 4) {
    return true;
  }

  auto count = 0xf;
  for (const auto& dvk : edges) {
    auto m = &edges;
    while (m->contains(dvk.first)) {
      if (m->at(dvk.first).end.height >= current.height) {
        count &= ~(1 << dvk.first);
        break;
      }

      m = &graph.at(m->at(dvk.first).end);
    }
  }

  return count > 0;
}

long long solution::calculate_scenic(
    const tree& current, const std::map<direction, edge>& edges,
    const std::map<tree, std::map<direction, edge>>& graph) {
  if (edges.size() != 4) {
    return 0;
  }

  auto counts = std::vector<long long>{0, 0, 0, 0};
  for (const auto& dvk : edges) {
    auto m = &edges;
    while (m->contains(dvk.first)) {
      counts[dvk.first] = counts[dvk.first] + 1;
      if (m->at(dvk.first).end.height >= current.height) {
        break;
      }

      m = &graph.at(m->at(dvk.first).end);
    }
  }

  return std::accumulate(counts.begin(), counts.end(), 1ll,
                         std::multiplies<>());
}
