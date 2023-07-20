#pragma once
#include <memory>
#include <numeric>
#include <string>
#include <vector>

namespace day7 {
struct osfile {
  std::string name;
  long long size;

  explicit osfile(std::string name, long long size)
      : name(std::move(name)), size(size) {}
};

struct folder {
  std::vector<osfile> files;
  std::vector<std::shared_ptr<folder>> folders;
  std::shared_ptr<folder> parent;
  std::string name;

  long long size() const {
    long long size = 0;
    for (const auto& file : files) {
      size += file.size;
    }

    for (const auto folder : folders) {
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
    for (const auto f : folders) {
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
  static long long sum_directories_under(const std::shared_ptr<folder> root,
                                         long long check_size);
};
}  // namespace day7