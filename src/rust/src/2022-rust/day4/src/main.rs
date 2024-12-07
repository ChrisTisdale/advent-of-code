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

use std::fs;

fn main() {
    let mut count: i32 = 0;

    let data = fs::read_to_string("assets/day4/measurements.txt")
        .unwrap()
        .lines()
        .filter(|s| !s.is_empty())
        .map(|s| s.parse::<i32>().unwrap())
        .collect::<Vec<i32>>();

    for (i, _value) in data.iter().enumerate() {
        if i > data.len() - 4 {
            break;
        }

        let pre_total: i32 = data[i..(i + 3)].iter().sum();
        let cur_total: i32 = data[(i + 1)..(i + 4)].iter().sum();
        if pre_total < cur_total {
            count += 1;
        }
    }

    println!("{count}");
}
