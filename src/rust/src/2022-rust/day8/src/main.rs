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
    let mut cols = Vec::new();
    for d in fs::read_to_string("assets/day7/measurements.txt")
        .unwrap()
        .lines()
        .filter(|s| !s.is_empty())
    {
        let result = d
            .chars()
            .filter_map(|s| s.to_digit(10))
            .collect::<Vec<u32>>();
        cols.push(result);
    }

    let mut max = 0;
    for row in 1..(cols.len() - 1) {
        for col in 1..(cols[row].len() - 1) {
            let current = cols[row][col];
            let mut left_count = cols[row]
                .iter()
                .take(col)
                .rev()
                .take_while(|&&x| x < current)
                .count();
            if left_count < col {
                left_count += 1;
            }
            let mut right_count = cols[row]
                .iter()
                .skip(col + 1)
                .take_while(|&&x| x < current)
                .count();
            if right_count < cols[row].len() - 1 - col {
                right_count += 1;
            }
            let mut top_count = cols
                .iter()
                .take(row)
                .rev()
                .take_while(|x| x[col] < current)
                .count();
            if top_count < row {
                top_count += 1;
            }
            let mut bottom_count = cols
                .iter()
                .skip(row + 1)
                .take_while(|x| x[col] < current)
                .count();
            if bottom_count < cols.len() - 1 - row {
                bottom_count += 1;
            }
            let local = left_count * right_count * top_count * bottom_count;
            if local > max {
                max = local;
            }
        }
    }

    println!("Sizes to remove: {max}");
}
