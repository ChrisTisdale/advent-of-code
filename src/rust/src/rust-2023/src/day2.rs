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

use std::collections::HashMap;

struct Game {
    r: u32,
    g: u32,
    b: u32,
}

#[must_use]
pub fn part_1(input: &str) -> usize {
    let mut total: usize = 0;
    for (i, s) in input.lines().enumerate() {
        let value = s
            .split(&[';', ':'])
            .skip(1)
            .map(str::trim)
            .map(|d| {
                d.split(';')
                    .map(|g| {
                        g.trim()
                            .split(',')
                            .map(str::trim)
                            .map(get_marble_details)
                            .collect::<HashMap<char, u32>>()
                    })
                    .map(|x| round_possible(i, &x))
            })
            .map(|mut f| {
                if f.all(|d| d.is_some()) {
                    return Some(i);
                }

                None
            })
            .all(|v| v.is_some());

        if value {
            total += i + 1;
        }
    }

    total
}

#[must_use]
pub fn part_2(input: &str) -> usize {
    let mut total: usize = 0;
    for s in input.lines() {
        let value = s
            .split(&[';', ':'])
            .skip(1)
            .map(str::trim)
            .map(|d| {
                d.split(';').map(|g| {
                    g.trim()
                        .split(',')
                        .map(str::trim)
                        .map(get_marble_details)
                        .collect::<HashMap<char, u32>>()
                })
            })
            .map(|d| {
                let res = d.collect::<Vec<HashMap<char, u32>>>();
                let r = res
                    .iter()
                    .filter_map(|m| m.get(&'r').copied())
                    .max()
                    .unwrap_or(0);
                let g = res
                    .iter()
                    .filter_map(|m| m.get(&'g').copied())
                    .max()
                    .unwrap_or(0);
                let b = res
                    .iter()
                    .filter_map(|m| m.get(&'b').copied())
                    .max()
                    .unwrap_or(0);
                Game { r, g, b }
            })
            .collect::<Vec<Game>>();

        let r = value.iter().map(|g| g.r as usize).max().unwrap_or(0);
        let g = value.iter().map(|g| g.g as usize).max().unwrap_or(0);
        let b = value.iter().map(|g| g.b as usize).max().unwrap_or(0);
        total += r * g * b;
    }

    total
}

fn round_possible(i: usize, x: &HashMap<char, u32>) -> Option<usize> {
    if x.get(&'r').filter(|v| **v > 12).is_some() {
        return None;
    }

    if x.get(&'g').filter(|v| **v > 13).is_some() {
        return None;
    }

    if x.get(&'b').filter(|v| **v > 14).is_some() {
        return None;
    }

    Some(i)
}

fn get_marble_details(x: &str) -> (char, u32) {
    let mut split = x.split(' ');
    let value = split.next().unwrap().parse::<u32>().unwrap();
    let char = match split.next().unwrap() {
        "red" => 'r',
        "green" => 'g',
        _ => 'b',
    };

    (char, value)
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn part_1_sample() {
        let actual = part_1(SAMPLE);
        assert_eq!(actual, 8);
    }

    #[test]
    fn part_1_measure() {
        let actual = part_1(MEASURE);
        assert_eq!(actual, 2278);
    }

    #[test]
    fn part_2_sample() {
        let actual = part_2(SAMPLE);
        assert_eq!(actual, 2286);
    }

    #[test]
    fn part_2_measure() {
        let actual = part_2(MEASURE);
        assert_eq!(actual, 67953);
    }

    const SAMPLE: &str = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";

    const MEASURE: &str = "Game 1: 9 red, 5 blue, 6 green; 6 red, 13 blue; 2 blue, 7 green, 5 red
Game 2: 6 red, 2 green, 2 blue; 12 green, 11 red, 17 blue; 2 blue, 10 red, 11 green; 13 green, 17 red; 15 blue, 20 red, 3 green; 3 blue, 11 red, 1 green
Game 3: 20 green, 1 blue, 7 red; 20 green, 7 blue; 18 red, 8 green, 3 blue; 7 red, 6 blue, 11 green; 11 red, 6 blue, 16 green
Game 4: 6 blue, 6 green; 2 blue, 5 green, 1 red; 9 blue, 1 red, 1 green; 1 red, 6 green, 8 blue; 4 green, 1 red, 1 blue
Game 5: 5 red, 4 blue, 11 green; 10 green, 3 blue, 18 red; 13 red, 13 green, 2 blue
Game 6: 1 blue, 15 green, 7 red; 2 blue, 4 green, 1 red; 1 blue, 13 green, 11 red; 2 blue, 10 red, 9 green
Game 7: 8 green, 1 blue, 1 red; 2 red, 2 green, 3 blue; 2 red, 1 blue
Game 8: 5 green, 9 blue, 2 red; 7 green, 3 red, 1 blue; 7 blue, 2 red, 1 green; 4 blue, 2 red, 14 green; 1 red, 5 blue, 12 green; 7 green, 9 blue, 3 red
Game 9: 11 red, 6 blue; 2 blue, 8 red, 9 green; 8 green, 13 red, 14 blue; 2 blue, 7 red, 9 green
Game 10: 3 red, 3 blue, 4 green; 4 red, 3 green, 2 blue; 13 red, 4 blue, 3 green; 6 blue, 5 green, 8 red; 10 red, 5 blue, 3 green
Game 11: 8 blue, 2 green, 4 red; 2 red, 13 blue, 2 green; 7 red, 3 green, 13 blue; 8 blue, 4 red; 12 blue, 6 red; 18 blue, 3 red, 1 green
Game 12: 15 red, 2 blue; 3 red, 5 green, 1 blue; 2 blue, 3 green, 6 red; 9 red, 4 green, 4 blue; 3 green
Game 13: 3 red, 3 green, 14 blue; 3 red, 14 blue, 1 green; 3 green, 4 blue; 7 blue, 1 red, 4 green
Game 14: 1 green, 2 red, 4 blue; 3 green, 5 blue, 11 red; 12 red, 2 green; 1 blue, 3 green, 4 red
Game 15: 1 red, 3 green, 4 blue; 2 red, 3 green, 2 blue; 10 green, 3 red, 3 blue; 5 red, 11 green, 3 blue
Game 16: 5 red, 12 blue, 12 green; 8 red, 5 blue; 11 green, 5 blue, 12 red; 4 green, 10 blue, 1 red; 1 blue
Game 17: 18 green, 15 red, 5 blue; 5 blue, 4 green, 14 red; 4 red, 7 blue, 9 green
Game 18: 2 red, 12 blue, 2 green; 15 blue, 4 red; 14 red; 11 red, 5 green, 5 blue
Game 19: 4 red, 2 blue, 4 green; 5 red; 7 green, 1 blue; 1 green, 4 red, 2 blue
Game 20: 5 green, 1 blue; 3 blue, 9 green; 14 blue, 7 green; 7 green, 1 red, 1 blue; 7 green, 2 blue
Game 21: 6 blue, 3 green, 8 red; 9 red, 1 green, 1 blue; 4 green, 7 red; 1 blue, 1 green, 12 red; 4 green, 9 red, 5 blue
Game 22: 1 red, 3 blue, 2 green; 12 green, 5 blue, 1 red; 1 green, 3 blue, 3 red; 1 red, 8 green, 2 blue
Game 23: 2 blue, 9 red, 14 green; 7 blue, 10 red; 7 blue, 7 green, 1 red
Game 24: 19 red, 3 green; 7 blue, 4 green, 12 red; 14 red, 3 green, 1 blue; 3 green, 14 red; 5 green, 7 blue, 18 red
Game 25: 12 red, 4 green, 3 blue; 3 blue, 12 red, 11 green; 3 red, 11 green, 2 blue
Game 26: 11 green, 2 red; 5 blue, 4 red; 1 green, 6 blue, 3 red; 9 red, 7 blue; 1 blue, 6 red, 1 green
Game 27: 10 red, 8 blue, 7 green; 6 green, 7 blue; 4 red, 10 green, 9 blue; 9 red, 2 green, 1 blue; 11 blue, 15 red, 9 green
Game 28: 3 blue, 2 red, 8 green; 3 red, 10 green; 11 green, 1 blue; 5 blue, 6 green, 7 red; 3 blue, 2 green
Game 29: 18 red, 1 blue; 3 red, 4 blue, 7 green; 1 blue, 16 green, 2 red; 3 blue, 6 green, 15 red; 1 red, 1 blue; 17 red, 6 green
Game 30: 10 red, 6 blue, 13 green; 2 green, 10 red, 4 blue; 4 red, 2 green, 2 blue
Game 31: 5 red, 13 green, 5 blue; 5 green, 12 blue, 5 red; 5 red, 3 green, 5 blue; 2 green, 3 red, 14 blue
Game 32: 2 blue, 14 red, 13 green; 11 red, 3 green, 1 blue; 9 red, 2 blue, 2 green; 5 blue, 3 red, 2 green; 4 blue, 8 green, 6 red; 12 red, 4 green, 5 blue
Game 33: 1 green, 15 blue; 1 red, 4 blue; 1 red, 1 green, 5 blue
Game 34: 1 green, 2 blue, 3 red; 11 red, 10 blue; 6 blue, 3 red
Game 35: 2 blue, 3 red, 1 green; 1 green, 9 blue, 8 red; 2 blue, 5 red; 2 green, 2 red, 2 blue; 1 red, 10 blue; 5 red, 9 blue
Game 36: 8 green, 1 red, 2 blue; 7 red, 5 green, 9 blue; 1 red, 10 green, 13 blue; 1 red, 10 green
Game 37: 1 green, 1 red; 2 green, 2 red; 2 green, 6 red; 7 red; 1 blue, 2 red
Game 38: 8 red, 7 green, 11 blue; 6 green, 10 blue, 11 red; 13 blue, 18 green, 7 red; 2 red, 7 blue, 12 green
Game 39: 4 blue, 8 red; 1 blue, 11 red, 2 green; 2 green, 3 blue, 12 red; 6 red, 1 green, 9 blue; 6 red, 1 blue, 1 green
Game 40: 2 blue, 17 red, 2 green; 4 red, 7 green; 4 blue, 1 green, 10 red; 6 green, 2 red; 6 red, 1 blue, 4 green; 5 green, 9 red, 4 blue
Game 41: 1 red, 8 blue; 3 green, 5 red, 3 blue; 8 blue, 1 green; 1 red, 9 blue; 5 red, 3 blue; 1 green, 4 red, 3 blue
Game 42: 7 green, 1 red, 10 blue; 11 blue, 1 green; 1 red, 17 blue, 2 green; 1 red, 4 green; 1 green, 3 blue; 11 blue, 1 red
Game 43: 5 green, 1 red; 5 blue, 3 green, 14 red; 7 green, 2 red, 11 blue; 3 red, 10 green, 4 blue; 5 green, 3 blue, 9 red; 8 green, 3 blue, 2 red
Game 44: 10 blue, 1 red, 2 green; 5 blue, 2 green, 2 red; 2 red, 2 green, 5 blue; 7 blue, 14 red, 1 green; 1 red, 2 green, 5 blue
Game 45: 16 green, 11 blue, 7 red; 6 blue, 8 red, 9 green; 7 green, 8 blue, 10 red; 13 red, 15 green, 8 blue; 3 red, 12 green
Game 46: 7 red, 2 green, 4 blue; 3 green, 7 blue; 2 blue, 5 red, 2 green; 3 green, 8 blue, 2 red
Game 47: 6 blue, 5 red; 5 red, 4 green, 5 blue; 4 green, 8 red; 5 red, 4 blue, 4 green; 5 blue, 5 green, 3 red; 5 blue, 2 green, 3 red
Game 48: 11 blue, 7 green, 2 red; 3 red, 8 green, 1 blue; 3 red
Game 49: 8 blue, 1 green, 3 red; 2 blue, 4 red; 6 red, 1 green; 2 red, 10 blue, 10 green
Game 50: 1 red, 8 green; 1 blue, 2 red, 8 green; 7 red, 1 blue; 7 red, 1 blue, 5 green; 6 green, 3 red
Game 51: 10 blue, 6 red; 10 red; 12 red, 5 blue; 11 red, 3 green, 3 blue
Game 52: 11 green, 7 red, 3 blue; 1 red, 9 blue, 8 green; 16 green, 2 blue, 8 red; 8 blue, 6 green; 3 blue, 5 red, 10 green; 8 red, 9 blue, 12 green
Game 53: 1 green, 4 blue, 11 red; 1 green, 12 red, 6 blue; 1 green, 5 red, 12 blue; 5 red, 11 blue; 1 blue, 11 red; 8 blue, 4 red, 1 green
Game 54: 3 blue, 2 green, 8 red; 2 blue, 5 red; 3 blue, 2 red, 2 green; 1 red, 9 blue; 5 red
Game 55: 1 green, 11 blue, 5 red; 16 blue, 11 green, 8 red; 16 blue, 2 red, 13 green
Game 56: 8 green, 6 blue, 6 red; 10 blue, 6 red, 9 green; 3 green, 13 blue, 6 red; 4 green, 5 blue, 3 red
Game 57: 6 green, 6 blue; 1 green, 1 red; 14 green, 1 blue
Game 58: 1 blue; 1 red; 1 red, 3 green, 1 blue; 1 red
Game 59: 5 green, 10 red; 1 green, 2 blue, 6 red; 8 red, 3 green, 2 blue; 4 green, 1 blue
Game 60: 2 red, 8 green; 1 blue, 3 green, 1 red; 2 green, 1 blue, 5 red; 1 red, 13 green, 1 blue; 4 red, 6 green, 1 blue
Game 61: 2 red, 2 green; 15 red, 1 green, 3 blue; 20 red; 7 red, 2 blue; 8 red, 5 blue, 1 green
Game 62: 4 green, 12 red, 14 blue; 11 red, 3 blue, 13 green; 6 green, 16 blue, 7 red; 7 red, 10 blue, 11 green
Game 63: 2 green, 8 red, 3 blue; 1 red; 2 blue, 8 red; 5 blue, 2 red; 1 green, 5 blue, 10 red; 1 green, 3 blue, 11 red
Game 64: 12 blue, 2 red, 4 green; 4 green, 3 red, 5 blue; 9 blue, 1 red, 4 green; 7 green, 7 blue, 1 red; 1 red, 10 blue, 2 green
Game 65: 4 blue, 2 green, 1 red; 1 blue, 4 red, 3 green; 5 green, 3 red; 1 red, 2 green, 15 blue; 3 blue, 3 red
Game 66: 1 red, 7 blue, 1 green; 3 red, 1 green, 1 blue; 1 green, 9 red, 2 blue; 2 green, 2 blue; 5 red, 3 green, 3 blue; 1 blue, 5 red
Game 67: 6 green; 17 green, 5 blue; 3 blue, 3 red, 9 green; 2 green, 4 blue; 1 red, 15 green
Game 68: 1 blue, 11 red, 8 green; 17 green, 3 blue, 8 red; 5 green, 8 red; 18 green, 7 red, 2 blue; 6 green
Game 69: 12 green, 13 blue, 2 red; 4 red, 14 green, 1 blue; 11 red, 15 green, 5 blue; 15 green, 9 red; 4 blue, 1 red, 5 green; 10 red, 20 green, 13 blue
Game 70: 6 red, 8 green, 7 blue; 5 blue, 1 red, 17 green; 2 red, 3 blue, 6 green; 7 blue, 1 red, 14 green; 7 red, 6 blue, 16 green
Game 71: 3 green, 3 blue, 3 red; 1 blue, 11 red, 2 green; 1 blue, 11 red
Game 72: 9 red, 17 blue, 1 green; 20 red, 3 green, 2 blue; 14 blue, 4 green, 11 red; 2 red, 12 blue, 7 green; 18 red, 13 blue, 7 green
Game 73: 6 green, 12 blue, 1 red; 10 blue, 5 red; 6 green, 17 blue, 3 red
Game 74: 1 green, 2 blue, 13 red; 2 blue, 2 green, 1 red; 2 green, 1 blue, 7 red; 1 red, 1 green
Game 75: 10 red, 2 green; 3 blue, 4 green; 9 red, 1 green
Game 76: 1 red, 3 green, 1 blue; 3 blue, 4 green, 6 red; 9 blue, 12 green, 2 red; 5 green, 1 red, 1 blue
Game 77: 3 blue, 4 red, 11 green; 8 green, 5 red; 7 blue, 11 green; 1 green, 3 blue, 6 red
Game 78: 15 blue, 5 green; 7 green, 9 blue; 7 green, 3 red, 2 blue
Game 79: 9 green, 6 red, 4 blue; 4 blue, 2 red, 14 green; 17 green, 2 blue, 4 red; 1 red, 2 green; 3 red, 3 green, 2 blue
Game 80: 1 green; 15 green, 1 red; 1 blue, 20 green, 1 red; 3 red, 15 green, 1 blue; 4 red, 3 green; 2 red, 18 green
Game 81: 4 blue, 1 green, 13 red; 13 blue, 19 red; 4 red, 13 blue; 8 blue, 10 red; 13 blue, 5 red; 1 green, 7 blue, 12 red
Game 82: 5 red, 3 blue; 4 red, 3 green, 9 blue; 19 blue, 1 green, 5 red; 5 green, 3 red, 10 blue
Game 83: 9 red, 3 blue, 5 green; 1 blue, 1 green, 11 red; 2 blue, 6 green, 18 red
Game 84: 2 green; 6 green, 5 red; 3 green, 1 red, 1 blue
Game 85: 2 blue, 6 red; 9 green, 5 red, 15 blue; 7 green, 10 red, 2 blue; 10 red, 6 blue, 2 green; 8 green, 5 red, 12 blue; 6 green, 5 blue, 6 red
Game 86: 2 blue, 12 red, 3 green; 3 red, 2 blue; 1 green, 2 blue, 2 red; 7 blue, 3 red, 1 green; 1 green, 2 blue, 5 red; 3 green, 14 red, 4 blue
Game 87: 3 blue, 1 green; 3 red, 2 blue, 1 green; 1 red, 3 blue; 10 red, 3 green; 5 red, 2 blue
Game 88: 3 blue, 9 red, 9 green; 9 blue, 11 red; 2 green, 11 blue; 2 blue, 14 red, 1 green; 7 green, 11 blue, 8 red; 9 red, 8 green, 3 blue
Game 89: 3 red, 1 blue, 16 green; 5 blue, 4 red, 3 green; 3 blue, 5 red, 5 green; 5 green, 8 blue, 2 red; 4 green, 2 red, 1 blue; 4 red, 1 green, 6 blue
Game 90: 7 green, 8 red; 1 blue, 7 green, 5 red; 4 green, 6 red
Game 91: 3 green, 6 red, 4 blue; 2 green, 9 red, 10 blue; 3 green, 12 blue; 1 red, 4 blue
Game 92: 12 green, 8 blue, 16 red; 6 red, 14 green, 4 blue; 3 green, 3 red, 10 blue; 9 blue, 6 red, 15 green; 14 green, 9 blue, 10 red
Game 93: 4 blue, 4 red, 9 green; 2 blue, 2 green, 6 red; 1 blue, 7 red; 7 blue, 17 red; 2 blue, 13 red, 10 green
Game 94: 4 green, 10 red; 9 red; 1 green, 3 blue, 14 red
Game 95: 9 green, 5 red; 3 blue, 11 red, 6 green; 4 red, 1 green; 13 green, 3 blue, 5 red; 1 red, 6 blue, 12 green; 7 red, 7 green
Game 96: 6 blue; 5 green, 2 blue, 2 red; 14 blue, 3 green
Game 97: 1 blue, 2 green, 5 red; 2 green, 8 blue, 9 red; 1 green, 8 blue, 6 red; 1 blue, 17 red; 2 green, 10 blue, 11 red
Game 98: 3 red, 12 blue, 2 green; 3 green, 4 blue, 4 red; 1 red, 11 blue, 2 green; 1 blue, 3 red
Game 99: 2 green, 9 red; 8 red, 4 green, 9 blue; 8 blue, 13 red; 10 green, 8 blue, 6 red; 11 green, 2 red, 13 blue
Game 100: 5 blue, 2 green, 7 red; 14 red, 15 green, 1 blue; 3 blue, 3 red; 8 green, 10 red, 6 blue; 6 blue, 4 red, 8 green";
}
