// Copyright (c) Christopher Tisdale 2025.
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

use std::ops::Range;

fn is_number_matched_twice(input: &str) -> bool {
    let chars = input.chars();
    let count = chars.count();
    if !count.is_multiple_of(2) {
        return false;
    }

    let left = &input[0..count / 2];
    let right = &input[count / 2..];
    left == right
}

fn any_numbers_repeated(input: &str, times: usize) -> bool {
    let chars = input.chars().collect::<Vec<char>>();
    let count = chars.len();

    if count < times {
        return false;
    }

    if !count.is_multiple_of(times) {
        return any_numbers_repeated(input, times + 1);
    }

    let mut values = Vec::<&str>::with_capacity(times);
    let r = count / times;
    for i in 0..times {
        values.push(input[i * r..(i + 1) * r].as_ref());
    }

    let first = values[0];
    for text in values {
        if first != text {
            return any_numbers_repeated(input, times + 1);
        }
    }

    true
}

#[must_use]
pub fn count_invalid_ids(input: &str, all_options: bool) -> usize {
    let ranges = input.split(',').filter(|s| !s.is_empty()).map(|s| {
        let mut items = s.split('-').map(|x| x.parse::<usize>().unwrap());
        Range {
            start: items.next().unwrap(),
            end: items.next().unwrap(),
        }
    });

    let mut total = 0;
    for range in ranges {
        for id in range.start..=range.end {
            if !all_options && is_number_matched_twice(id.to_string().as_str())
                || all_options && any_numbers_repeated(id.to_string().as_str(), 2)
            {
                total += id;
            }
        }
    }

    total
}

#[cfg(test)]
mod test {
    use super::*;

    #[test]
    fn part_1_sample() {
        let result = count_invalid_ids(SAMPLE1, false);
        assert_eq!(1227775554, result);
    }

    #[test]
    fn part_1_measure() {
        let result = count_invalid_ids(MEASUREMENT, false);
        assert_eq!(15873079081, result);
    }

    #[test]
    fn part_2_sample() {
        let result = count_invalid_ids(SAMPLE1, true);
        assert_eq!(4174379265, result);
    }

    #[ignore]
    fn part_2_measure() {
        let result = count_invalid_ids(MEASUREMENT, true);
        assert_eq!(22617871034, result);
    }

    const SAMPLE1: &str = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";

    const MEASUREMENT: &str = "851786270-851907437,27-47,577-1044,1184-1872,28214317-28368250,47766-78575,17432-28112,2341-4099,28969-45843,5800356-5971672,6461919174-6461988558,653055-686893,76-117,2626223278-2626301305,54503501-54572133,990997-1015607,710615-802603,829001-953096,529504-621892,8645-12202,3273269-3402555,446265-471330,232-392,179532-201093,233310-439308,95134183-95359858,3232278502-3232401602,25116215-25199250,5489-8293,96654-135484,2-17";
}
