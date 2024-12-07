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

#pragma once
#include <memory>
#include <numeric>
#include <string>
#include <vector>

namespace aoc2022::day7 {
struct os_file {
  std::string name;
  long long size;

  explicit os_file(std::string name, long long size)
      : name(std::move(name)), size(size) {}
};

struct folder {
  std::vector<os_file> files;
  std::vector<std::shared_ptr<folder>> folders;
  std::shared_ptr<folder> parent;
  std::string name;

  [[nodiscard]] long long size() const {
    long long size = 0;
    for (const auto& file : files) {
      size += file.size;
    }

    for (const auto& folder : folders) {
      size += folder->size();
    }

    return size;
  }

  std::vector<std::shared_ptr<folder>> get_directories() {
    std::vector<std::shared_ptr<folder>> f;
    get_directories(f);
    return f;
  }

 private:
  void get_directories(std::vector<std::shared_ptr<folder>>& list) {
    for (const auto& f : folders) {
      list.push_back(f);
      f->get_directories(list);
    }
  }
};

class solution {
 public:
  static long long run_part1(const std::string& file);
  static long long run_part2(const std::string& file);

 private:
  static std::shared_ptr<folder> read_file(const std::string& file);
  static long long int sum_directories_under(
      const std::shared_ptr<folder>& root);
};
}  // namespace aoc2022::day7
