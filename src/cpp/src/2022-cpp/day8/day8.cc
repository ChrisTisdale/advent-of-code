// Copyright (c) Christopher Tisdale 2024.
//
// Licensed under BSD-3-Clause.
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      https://spdx.org/licenses/BSD-3-Clause.html
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#include "day8.h"

#include <algorithm>
#include <fstream>
#include <iostream>
#include <numeric>

using namespace aoc2022::day8;

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
        edges.emplace(
            direction::Up,
            edge(direction::Up, t, tree(lines[row - 1][col], row - 1, col)));
      }

      if (col > 0) {
        edges.emplace(
            direction::Left,
            edge(direction::Left, t, tree(lines[row][col - 1], row, col - 1)));
      }

      if (row + 1 < lines.size()) {
        edges.emplace(
            direction::Down,
            edge(direction::Down, t, tree(lines[row + 1][col], row + 1, col)));
      }

      if (col + 1 < lines[row].size()) {
        edges.emplace(
            direction::Right,
            edge(direction::Right, t, tree(lines[row][col + 1], row, col + 1)));
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

  for (const auto& dvk : edges) {
    auto m = &edges;
    bool hidden = false;
    while (m->contains(dvk.first)) {
      if (m->at(dvk.first).end.height >= current.height) {
        hidden = true;
        break;
      }

      m = &graph.at(m->at(dvk.first).end);
    }

    if (!hidden) {
      return true;
    }
  }

  return false;
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
      auto index = static_cast<size_t>(dvk.first);
      counts[index] = counts[index] + 1;
      if (m->at(dvk.first).end.height >= current.height) {
        break;
      }

      m = &graph.at(m->at(dvk.first).end);
    }
  }

  return std::accumulate(counts.begin(), counts.end(), 1ll,
                         std::multiplies<>());
}
