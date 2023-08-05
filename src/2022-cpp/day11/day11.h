#pragma once
#include <istream>
#include <memory>
#include <queue>
#include <string>
#include <vector>

namespace day11 {
class operation {
 public:
  [[nodiscard]] std::size_t eval(long long value) const {
    if (value % divisor) {
      return false_throw;
    }

    return true_throw;
  }

  [[nodiscard]] virtual long long get_result(long long value) = 0;

  [[nodiscard]] long long get_divisor() const { return divisor; }

  virtual ~operation() = default;

 protected:
  operation(std::size_t false_throw, std::size_t true_throw, long long divisor)
      : false_throw(false_throw), true_throw(true_throw), divisor(divisor) {}

 private:
  std::size_t false_throw;
  std::size_t true_throw;
  long long divisor;
};

class add_operator : public operation {
 public:
  explicit add_operator(long long input, std::size_t false_throw,
                        std::size_t true_throw, long long divisor)
      : input(input), operation(false_throw, true_throw, divisor) {}

 protected:
  long long get_result(long long value) override {
    return input < 0 ? value + value : input + value;
  }

 private:
  long long input;
};

class multiply_operator : public operation {
 public:
  explicit multiply_operator(long long input, std::size_t false_throw,
                             std::size_t true_throw, long long divisor)
      : input(input), operation(false_throw, true_throw, divisor) {}

 protected:
  long long get_result(long long value) override {
    return input < 0 ? value * value : input * value;
  }

 private:
  long long input;
};

class monkey {
 public:
  monkey(std::queue<long long> items, std::unique_ptr<operation> handler)
      : items(std::move(items)), handler(std::move(handler)) {}
  monkey(const monkey& o) = delete;
  monkey(monkey&& o) noexcept {
    items = std::move(o.items);
    handler = std::move(o.handler);
  }

  std::queue<long long> items;
  std::unique_ptr<operation> handler;
};

class solution {
 public:
  static long long run_part1(std::istream& file);
  static long long run_part2(std::istream& file);

 private:
  static std::vector<std::shared_ptr<monkey>> read_file(std::istream& file);
  static long long run_game(const std::vector<std::shared_ptr<monkey>>& result,
                            bool damage, int loop_count);
  static long long get_factor(
      const std::vector<std::shared_ptr<monkey>>& result);
};
}  // namespace day11
