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
#include <istream>
#include <memory>
#include <string>
#include <vector>

namespace aoc2022::day13 {
enum class packet_type { list, value };
class packet {
 private:
  packet_type type;

 public:
  [[nodiscard]] packet_type get_type() const { return type; }
  virtual ~packet() = default;

 protected:
  explicit packet(packet_type type) : type(type) {}
};

using packet_list = std::vector<std::shared_ptr<packet>>;

class list_packet : public packet {
 private:
  packet_list packets;

 public:
  packet_list::iterator begin() { return packets.begin(); }
  [[nodiscard]] packet_list::const_iterator begin() const {
    return packets.begin();
  }
  packet_list::iterator end() { return packets.end(); }
  [[nodiscard]] std::vector<std::shared_ptr<packet>>::const_iterator end()
      const {
    return packets.end();
  }
  void add_packet(std::unique_ptr<packet> p) {
    packets.push_back(std::move(p));
  }
  std::shared_ptr<packet> operator[](std::size_t index) const {
    return packets[index];
  }
  [[nodiscard]] std::shared_ptr<packet> get_index(std::size_t index) const {
    return packets[index];
  }
  [[nodiscard]] std::size_t size() const { return packets.size(); }
  list_packet() : packet(packet_type::list) {}
  list_packet(list_packet&& other) = default;
  list_packet(const list_packet& other) = default;
  list_packet& operator=(const list_packet&) = default;
};

class value_packet : public packet {
 private:
  int value;

 public:
  [[nodiscard]] int get_value() const { return value; }
  explicit value_packet(int value) : value(value), packet(packet_type::value) {}
  value_packet(value_packet&& other) = default;
  value_packet(const value_packet& other) = default;
  value_packet& operator=(const value_packet&) = default;
};

struct signals {
  std::unique_ptr<list_packet> left;
  std::unique_ptr<list_packet> right;
};

using signals_list = std::vector<std::unique_ptr<signals>>;

class solution {
 public:
  static long long run_part1(std::istream& file);
  static long long run_part2(std::istream& file);

 private:
  static signals_list read_file(std::istream& file);
  static std::unique_ptr<list_packet> parse_packet(const std::string& input,
                                                   int& i);
  static int comparer(const packet* left, const packet* right);
};
}  // namespace aoc2022::day13
