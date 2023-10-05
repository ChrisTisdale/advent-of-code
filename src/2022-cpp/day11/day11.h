#pragma once
#include <istream>
#include <memory>
#include <queue>
#include <string>
#include <vector>

namespace day11 {
enum class operations { add, multiply, subtract, divide };

class operation {
 public:
  [[nodiscard]] std::size_t eval(long long value) const {
    if (value % divisor) {
      return false_throw;
    }

    return true_throw;
  }

  [[nodiscard]] long long get_result(long long value) {
    switch (type) {
      case operations::add:
        return input < 0 ? value + value : input + value;
      case operations::multiply:
        return input < 0 ? value * value : input * value;
      case operations::subtract:
        return input < 0 ? 0 : input - value;
      default:
        return input < 0 ? 1 : input / value;
    }
  };

  [[nodiscard]] long long get_divisor() const { return divisor; }

  operation(std::size_t false_throw, std::size_t true_throw, long long divisor,
            long long input, operations type)
      : false_throw(false_throw),
        true_throw(true_throw),
        divisor(divisor),
        input(input),
        type(type) {}

  operation(const operation& o) = delete;

  operation(operation&& o) noexcept
      : false_throw(o.false_throw),
        true_throw(o.true_throw),
        divisor(o.divisor),
        input(o.input),
        type(o.type) {}

 private:
  std::size_t false_throw;
  std::size_t true_throw;
  long long divisor;
  long long input;
  operations type;
};

struct monkey {
  monkey(std::queue<long long> items, operation handler)
      : items(std::move(items)), handler(std::move(handler)) {}

  monkey(const monkey& o) = delete;

  monkey(monkey&& o) noexcept
      : items(std::move(o.items)), handler(std::move(o.handler)) {}

  std::queue<long long> items;
  operation handler;
};

class solution {
 public:
  static long long run_part1(std::istream& file);
  static long long run_part2(std::istream& file);

 private:
  static std::vector<monkey> read_file(std::istream& file);
  static long long run_game(std::vector<monkey>& result, bool damage,
                            int loop_count);
  static long long get_factor(const std::vector<monkey>& result);
  static monkey build_monkey(std::istream& file,
                             std::queue<long long int>&& init);
  static std::queue<long long> get_start(std::string& line);
  static long long get_input(operations type, std::string& line);
  static long long get_divisor(std::string& line);
  static size_t get_false_value(std::string& line);
  static size_t get_true_value(std::string& line);
  static operations get_operation_type(std::string& line);
};
}  // namespace day11
