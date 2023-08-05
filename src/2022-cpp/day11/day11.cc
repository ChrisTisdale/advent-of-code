#include "day11.h"

#include <algorithm>
#include <sstream>

using namespace day11;

long long solution::run_part1(std::istream& file) {
  auto result = read_file(file);
  return run_game(result, true, 20);
}

long long solution::run_part2(std::istream& file) {
  auto result = read_file(file);
  return run_game(result, false, 10000);
}

std::vector<std::shared_ptr<monkey>> solution::read_file(std::istream& file) {
  std::vector<std::shared_ptr<monkey>> monkeys;
  std::string line;
  while (true) {
    std::queue<long long> init;
    for (int i = 0; i < 3; ++i) {
      if (!std::getline(file, line)) {
        return monkeys;
      }

      switch (i) {
        case 1: {
          std::stringstream ss(line);
          std::string temp;
          std::getline(ss, temp, ':');
          while (std::getline(ss, temp, ',')) {
            char* end;
            auto value = strtoll(temp.c_str(), &end, 10);
            init.push(value);
          }
        } break;
        case 2: {
          // op
          bool is_add = line.contains("+");
          size_t index;
          if (is_add) {
            index = line.find('+');
          } else {
            index = line.find('*');
          }

          long long value;
          char* end;
          auto search = line.find("old", index + 1);
          if (search != std::string::npos) {
            value = -1;
          } else {
            value = strtoll(line.c_str() + index + 1, &end, 10);
          }

          if (!std::getline(file, line)) {
            return monkeys;
          }

          // test
          index = line.find(" by ") + 4;
          long long divisor = strtoll(line.c_str() + index, &end, 10);
          if (!std::getline(file, line)) {
            return monkeys;
          }

          // true value
          index = line.find("to monkey ") + 10;
          std::size_t true_value = strtoll(line.c_str() + index, &end, 10);
          if (!std::getline(file, line)) {
            return monkeys;
          }

          // false value
          index = line.find("to monkey ") + 10;
          std::size_t false_value = strtoll(line.c_str() + index, &end, 10);
          if (is_add) {
            monkeys.emplace_back(std::make_shared<monkey>(
                init, std::make_unique<add_operator>(value, false_value,
                                                     true_value, divisor)));
          } else {
            monkeys.emplace_back(std::make_shared<monkey>(
                init, std::make_unique<multiply_operator>(
                          value, false_value, true_value, divisor)));
          }
        } break;
        default:
          break;
      }
    }

    std::getline(file, line);
  }
}

long long solution::run_game(const std::vector<std::shared_ptr<monkey>>& result,
                             bool damage, int loop_count) {
  std::vector<long long> inspected;
  for (std::size_t i = 0; i < result.size(); ++i) {
    inspected.push_back(0);
  }

  long long factor = damage ? 1 : get_factor(result);
  for (int i = 0; i < loop_count; ++i) {
    size_t index = 0;
    for (const auto& m : result) {
      while (!m->items.empty()) {
        auto value = m->items.front();
        m->items.pop();
        value = m->handler->get_result(value);
        if (damage) {
          value /= 3;
        } else {
          value = value % factor;
        }

        auto pass = m->handler->eval(value);
        result[pass]->items.push(value);
        inspected[index]++;
      }

      ++index;
    }
  }

  std::sort(inspected.begin(), inspected.end(),
            [](auto l, auto r) { return l > r; });
  long long first = inspected[0];
  long long second = inspected[1];
  return first * second;
}

long long solution::get_factor(
    const std::vector<std::shared_ptr<monkey>>& result) {
  long long factor = 1ll;
  for (const auto& m : result) {
    factor *= m->handler->get_divisor();
  }

  return factor;
}
