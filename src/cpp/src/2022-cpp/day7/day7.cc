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

#include "day7.h"

#include <algorithm>
#include <fstream>
#include <iostream>
#include <sstream>

using namespace aoc2022::day7;

static const long long DISK_SIZE = 70000000;
static const long long SPACE_REQUIRED = 30000000;
static const long long CHECK_SIZE = 100000;

long long solution::run_part1(const std::string& file) {
  auto root = read_file(file);
  return sum_directories_under(root);
}

long long solution::run_part2(const std::string& file) {
  auto root = read_file(file);
  const long long free_space = DISK_SIZE - root->size();
  const long long need_space = SPACE_REQUIRED - free_space;
  auto directories = root->get_directories();
  std::sort(directories.begin(), directories.end(),
            [](const auto& l, const auto& r) { return l->size() < r->size(); });
  for (const auto& d : directories) {
    auto size = d->size();
    if (size >= need_space) {
      return size;
    }
  }
  return -1;
}

std::shared_ptr<folder> solution::read_file(const std::string& file) {
  auto root = std::make_shared<folder>();
  root->name = "/";
  std::shared_ptr<folder> current = nullptr;
  std::string line;
  std::string temp;
  std::ifstream f(file);
  while (std::getline(f, line)) {
    std::stringstream ss(line);
    if (line.starts_with("$")) {
      std::getline(ss, line, ' ');
      ss >> temp;
      if (temp.starts_with("cd")) {
        ss >> temp;
        if (temp == "/") {
          current = root;
        } else if (temp == "..") {
          current = current->parent;
        } else {
          auto temp_folder = std::make_shared<folder>();
          temp_folder->parent = current;
          ss >> temp;
          temp_folder->name = temp;
          current->folders.push_back(temp_folder);
          current = temp_folder;
        }
      }
    } else if (!line.starts_with("dir")) {
      long long size;
      ss >> size;
      ss >> temp;
      current->files.emplace_back(temp, size);
    }
  }

  return root;
}

long long int solution::sum_directories_under(
    const std::shared_ptr<folder>& root) {
  long long sum = 0;
  auto size = root->size();
  if (size <= CHECK_SIZE) {
    sum += size;
  }

  for (const auto& f : root->folders) {
    sum += sum_directories_under(f);
  }

  return sum;
}
