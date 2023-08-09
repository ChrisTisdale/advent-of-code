use std::collections::{HashMap, VecDeque};
use std::fs;

fn main() {
    for d in fs::read_to_string("assets/day6/measurements.txt")
        .unwrap()
        .lines()
        .filter(|s| !s.is_empty())
    {
        let mut m = HashMap::new();
        let mut l = VecDeque::new();
        let mut count: i32 = 0;
        println!("Checking: {d}");
        for c in d.chars().enumerate() {
            count += 1;
            if m.contains_key(&c.1) {
                let mut x = l.pop_front().unwrap();
                m.remove(&x);
                while x != c.1 {
                    x = l.pop_front().unwrap();
                    m.remove(&x);
                }
            }

            m.insert(c.1, 0);
            l.push_back(c.1);
            if m.keys().count() == 14 {
                break;
            }
        }

        println!("{count}");
    }
}
