#include "day1.h"

#include <algorithm>
#include <fstream>
#include <sstream>
#include <vector>

using namespace aoc2024::day1;

long long solution::run_part1(const std::string& file) {
  std::ifstream ifs(file);
  std::string line;
  std::vector<long long> left;
  std::vector<long long> right;
  long long sum = 0;
  while (std::getline(ifs, line)) {
    std::istringstream iss(line);
    long long num;
    iss >> num;
    left.push_back(num);
    iss >> num;
    right.push_back(num);
  }

  std::ranges::sort(left);
  std::ranges::sort(right);

  for (std::size_t i = 0; i < left.size(); ++i) {
    sum += std::abs(right[i] - left[i]);
  }

  return sum;
}

long long solution::run_part2(const std::string& file) {
  std::ifstream ifs(file);
  std::string line;
  std::vector<long long> left;
  std::vector<long long> right;
  long long sum = 0;
  while (std::getline(ifs, line)) {
    std::istringstream iss(line);
    long long num;
    iss >> num;
    left.push_back(num);
    iss >> num;
    right.push_back(num);
  }

  std::vector<long long> count;
  count.reserve(left.size());
  for (const auto i : left) {
    long long total = 0;
    for (const auto j : right) {
      if (i == j) {
        ++total;
      }
    }

    count.push_back(total);
  }

  for (std::size_t i = 0; i < count.size(); ++i) {
    sum += count[i] * left[i];
  }

  return sum;
}