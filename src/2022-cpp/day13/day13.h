#pragma once
#include <istream>
#include <memory>
#include <string>
#include <vector>

namespace day13 {
enum packet_type { list, value };
class packet {
 private:
  packet_type type;

 public:
  [[nodiscard]] packet_type get_type() const { return type; }
  virtual ~packet() = default;

 protected:
  explicit packet(packet_type type) : type(type) {}
};

class list_packet : public packet {
 private:
  std::vector<std::shared_ptr<packet>> packets;

 public:
  std::vector<std::shared_ptr<packet>>::iterator begin() {
    return packets.begin();
  }
  [[nodiscard]] std::vector<std::shared_ptr<packet>>::const_iterator begin()
      const {
    return packets.begin();
  }
  std::vector<std::shared_ptr<packet>>::iterator end() { return packets.end(); }
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

class solution {
 public:
  static long long run_part1(std::istream& file);
  static long long run_part2(std::istream& file);

 private:
  static std::vector<std::unique_ptr<signals>> read_file(std::istream& file);
  static std::unique_ptr<list_packet> parse_packet(const std::string& input,
                                                   int& i);
  static int comparer(const packet* left, const packet* right);
};
}  // namespace day13
