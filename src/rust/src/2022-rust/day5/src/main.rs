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

use std::{fs, str::FromStr};

#[derive(Debug)]
struct Round {
    pub from: usize,
    pub to: usize,
    pub count: usize,
}

#[derive(Debug, PartialEq, Eq)]
struct ParseRoundError;

impl FromStr for Round {
    type Err = ParseRoundError;

    fn from_str(s: &str) -> Result<Self, Self::Err> {
        let data: Vec<&str> = s.split(' ').collect();
        Ok(Self {
            from: data[3].parse::<usize>().expect("AOC won't lie") - 1,
            to: data[5].parse::<usize>().expect("AOC won't lie") - 1,
            count: data[1].parse().expect("AOC won't lie"),
        })
    }
}

fn main() {
    let mut count: usize = 0;

    let data = fs::read_to_string("assets/day5/measurements.txt").expect("aoc lies");

    let mut chars: Vec<Vec<char>> = Vec::new();
    for l in data.lines().take_while(|s| s.contains('[')) {
        let mut i: usize = 0;
        while i < l.len() {
            if chars.len() <= i / 4 {
                chars.push(Vec::new());
            }

            let c = l.chars().nth(i + 1).unwrap();
            //println!("{:?}, {}, {}", chars, i, l.len());
            if c != ' ' {
                chars[i / 4].insert(0, c);
            }
            i += 4;
        }

        count += 1;
    }

    let rounds = data
        .lines()
        .skip(count + 2)
        .map(|s| s.parse::<Round>().unwrap())
        .collect();
    println!("Round 1: {}", round1(&rounds, chars.clone()));
    println!("Round 2: {}", round2(&rounds, chars.clone()));
}

fn round1(rounds: &Vec<Round>, mut chars: Vec<Vec<char>>) -> String {
    for round in rounds {
        for _ in 0..round.count {
            let value = chars[round.from].pop().unwrap();
            chars[round.to].push(value);
        }
    }

    println!("{chars:?}");
    let res = chars.iter().map(|c| c.last().unwrap()).collect::<String>();
    res
}

fn round2(rounds: &Vec<Round>, mut chars: Vec<Vec<char>>) -> String {
    for round in rounds {
        let mut temp = Vec::new();
        for _ in 0..round.count {
            let value = chars[round.from].pop().unwrap();
            temp.push(value);
        }

        while let Some(v) = temp.pop() {
            chars[round.to].push(v);
        }
    }

    println!("{chars:?}");
    let res = chars.iter().map(|c| c.last().unwrap()).collect::<String>();
    res
}
