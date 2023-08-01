#include "day9.h"

#include <memory>
#include <set>
#include <sstream>

using namespace day9;

long long solution::run_part1(std::istream& file) {
  auto inputs = read_file(file);
  return find_unique_spaces(inputs, 0);
}

long long solution::run_part2(std::istream& file) {
  auto inputs = read_file(file);
  return find_unique_spaces(inputs, 8);
}

std::vector<input> solution::read_file(std::istream& file) {
  std::vector<input> data;
  std::string line;
  while (std::getline(file, line)) {
    std::stringstream ss(line);
    char d;
    std::size_t c;
    ss >> d;
    ss >> c;
    data.emplace_back(d, c);
  }

  return data;
}

long long solution::find_unique_spaces(const std::vector<input>& data,
                                       const std::size_t middle_count) {
  const long count = static_cast<long>(middle_count);
  std::unique_ptr<point[]> middle(new point[count]);
  point head;
  point tail;
  std::set<point> found;
  for (auto move : data) {
    for (std::size_t i = 0; i < move.count; ++i) {
      switch (move.direction) {
        case 'U':
          head.y += 1;
          break;
        case 'D':
          head.y -= 1;
          break;
        case 'L':
          head.x -= 1;
          break;
        default:
          head.x += 1;
          break;
      }

      for (long j = 0; j < count; ++j) {
        update_current_location(j == 0 ? head : middle[j - 1], middle[j]);
      }

      update_current_location(middle_count == 0 ? head : middle[count - 1],
                              tail);
      found.insert(tail);
    }
  }

  return static_cast<long long>(found.size());
}

void solution::update_current_location(const point& parent, point& current) {
  auto x_updated = std::abs(current.x - parent.x) > 1;
  auto y_updated = std::abs(current.y - parent.y) > 1;

  if (x_updated && current.y != parent.y ||
      y_updated && current.x != parent.x) {
    current.x = current.x > parent.x ? current.x - 1 : current.x + 1;
    current.y = current.y > parent.y ? current.y - 1 : current.y + 1;
    return;
  }

  if (x_updated) {
    current.x = current.x > parent.x ? current.x - 1 : current.x + 1;
  }

  if (y_updated) {
    current.y = current.y > parent.y ? current.y - 1 : current.y + 1;
  }
}
