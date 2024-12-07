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
