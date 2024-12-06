#include "day13.h"

#include <algorithm>

using namespace aoc2022::day13;

long long solution::run_part1(std::istream& file) {
  auto sigs = read_file(file);
  int c = 0;
  int i = 0;
  for (const auto& s : sigs) {
    ++i;
    if (comparer(s->left.get(), s->right.get()) < 0) {
      c += i;
    }
  }
  return c;
}

long long solution::run_part2(std::istream& file) {
  auto sigs = read_file(file);
  packet_list values;
  for (const auto& s : sigs) {
    values.push_back(std::move(s->left));
    values.push_back(std::move(s->right));
  }

  auto d1 = std::make_shared<list_packet>();
  auto inner_d1 = std::make_unique<list_packet>();
  inner_d1->add_packet(std::make_unique<value_packet>(2));
  d1->add_packet(std::move(inner_d1));
  values.push_back(d1);

  auto d2 = std::make_shared<list_packet>();
  auto inner_d2 = std::make_unique<list_packet>();
  inner_d2->add_packet(std::make_unique<value_packet>(6));
  d2->add_packet(std::move(inner_d2));
  values.push_back(d2);

  std::sort(values.begin(), values.end(), [](auto l, auto r) {
    return solution::comparer(l.get(), r.get()) < 0;
  });

  int d1_loc = 0;
  int d2_loc = 0;
  for (std::size_t i = 0; i < values.size(); ++i) {
    const auto v1 = values[i];
    if (v1.get() == d1.get()) {
      d1_loc = static_cast<int>(i + 1);
    } else if (v1.get() == d2.get()) {
      d2_loc = static_cast<int>(i + 1);
    }
  }
  return d1_loc * d2_loc;
}

signals_list solution::read_file(std::istream& file) {
  std::string temp;
  signals_list points;
  int i = 0;
  std::unique_ptr<signals> current = std::make_unique<signals>();
  while (std::getline(file, temp)) {
    int t = 0;
    switch (i++ % 3) {
      case 0:
        current->left = parse_packet(temp, t);
        break;
      case 1:
        current->right = parse_packet(temp, t);
        points.push_back(std::move(current));
        current = std::make_unique<signals>();
        break;
      default:
        break;
    }
  }

  return points;
}

std::unique_ptr<list_packet> solution::parse_packet(const std::string& input,
                                                    int& i) {
  auto current = std::make_unique<list_packet>();
  for (; i < input.length();) {
    const auto c = input[i];
    switch (c) {
      case '[':
        ++i;
        current->add_packet(parse_packet(input, i));
        break;
      case ',':
        ++i;
        break;
      case ']':
        ++i;
        return std::move(current);
      default:
        int j = i + 1;
        for (; j < input.length() - 1; ++j) {
          const auto temp = input[j];
          if (temp == '[' || temp == ']' || temp == ',') break;
        }

        const auto temp = input.substr(i, j - i);
        current->add_packet(std::make_unique<value_packet>(std::stoi(temp)));
        i += j - i;
        break;
    }
  }

  return std::move(current);
}

int solution::comparer(const packet* left, const packet* right) {
  if (left->get_type() == packet_type::value &&
      right->get_type() == packet_type::value) {
    const auto l = dynamic_cast<const value_packet*>(left);
    const auto r = dynamic_cast<const value_packet*>(right);
    return l->get_value() - r->get_value();
  }
  if (left->get_type() == packet_type::list &&
      right->get_type() == packet_type::list) {
    const auto l = dynamic_cast<const list_packet*>(left);
    const auto r = dynamic_cast<const list_packet*>(right);
    for (std::size_t i = 0; i < std::min(l->size(), r->size()); ++i) {
      const auto lp = l->get_index(i);
      const auto rp = r->get_index(i);
      const auto res = comparer(lp.get(), rp.get());
      if (res != 0) {
        return res;
      }
    }

    return static_cast<int>(l->size() - r->size());
  }

  if (left->get_type() == packet_type::value) {
    auto l = std::make_unique<list_packet>();
    const auto v = dynamic_cast<const value_packet*>(left);
    l->add_packet(std::make_unique<value_packet>(v->get_value()));
    return comparer(l.get(), right);
  }

  auto r = std::make_unique<list_packet>();
  const auto v = dynamic_cast<const value_packet*>(right);
  r->add_packet(std::make_unique<value_packet>(v->get_value()));
  return comparer(left, r.get());
}
