use std::collections::HashSet;
use std::iter::Rev;
use std::ops::{Range, RangeInclusive};

struct GuardMap {
    map: Vec<Vec<char>>,
    start_x: usize,
    start_y: usize,
}

#[derive(Debug, Eq, PartialEq, Hash, Copy, Clone)]
enum Direction {
    North,
    South,
    East,
    West,
    Exit,
}

#[derive(Debug, Eq, PartialEq, Hash, Copy, Clone)]
struct Index {
    x: usize,
    y: usize,
}

#[derive(Debug, Eq, PartialEq, Hash, Copy, Clone)]
struct Line {
    x: usize,
    y: usize,
    direction: Direction,
}

fn handle_direction(
    current: &Line,
    guard_map: &GuardMap,
    hit_map: &mut HashSet<Index>,
    len_y: usize,
    len_x: usize,
    add_map: bool,
) -> Line {
    match current.direction {
        Direction::North => {
            for i in (0..=current.y).rev() {
                let local = guard_map.map[i][current.x];
                if local == '#' {
                    return Line {
                        x: current.x,
                        y: i + 1,
                        direction: Direction::East,
                    };
                }

                if add_map {
                    hit_map.insert(Index { x: current.x, y: i });
                }
            }

            Line {
                x: current.x,
                y: 0,
                direction: Direction::Exit,
            }
        }
        Direction::South => {
            for i in current.y..len_y {
                let local = guard_map.map[i][current.x];
                if local == '#' {
                    return Line {
                        x: current.x,
                        y: i - 1,
                        direction: Direction::West,
                    };
                }

                if add_map {
                    hit_map.insert(Index { x: current.x, y: i });
                }
            }

            Line {
                x: current.x,
                y: len_y - 1,
                direction: Direction::Exit,
            }
        }
        Direction::East => {
            for i in current.x..len_x {
                let local = guard_map.map[current.y][i];
                if local == '#' {
                    return Line {
                        x: i - 1,
                        y: current.y,
                        direction: Direction::South,
                    };
                }

                if add_map {
                    hit_map.insert(Index { x: i, y: current.y });
                }
            }

            Line {
                x: len_x - 1,
                y: current.y,
                direction: Direction::Exit,
            }
        }
        Direction::West => {
            for i in (0..=current.x).rev() {
                let local = guard_map.map[current.y][i];
                if local == '#' {
                    return Line {
                        x: i + 1,
                        y: current.y,
                        direction: Direction::North,
                    };
                }

                if add_map {
                    hit_map.insert(Index { x: i, y: current.y });
                }
            }

            Line {
                x: 0,
                y: current.y,
                direction: Direction::Exit,
            }
        }
        Direction::Exit => *current,
    }
}

enum DirectionIterator {
    Range(Range<usize>),
    Reverse(Rev<RangeInclusive<usize>>),
}

impl From<Range<usize>> for DirectionIterator {
    fn from(value: Range<usize>) -> Self {
        Self::Range(value)
    }
}

impl From<Rev<RangeInclusive<usize>>> for DirectionIterator {
    fn from(value: Rev<RangeInclusive<usize>>) -> Self {
        Self::Reverse(value)
    }
}

impl Iterator for DirectionIterator {
    type Item = usize;

    fn next(&mut self) -> Option<Self::Item> {
        match self {
            Self::Range(r) => r.next(),
            Self::Reverse(r) => r.next(),
        }
    }
}

fn has_cycle(
    mut current: Line,
    guard_map: &GuardMap,
    hit_map: &mut HashSet<Index>,
    len_y: usize,
    len_x: usize,
) -> bool {
    let mut lines = HashSet::new();

    loop {
        if !lines.insert(current) {
            return true;
        }

        current = handle_direction(&current, guard_map, hit_map, len_y, len_x, false);

        if current.direction == Direction::Exit {
            return false;
        }
    }
}

fn get_visited(guard_map: &GuardMap) -> HashSet<Index> {
    let mut visited = HashSet::new();
    let mut current = Line {
        x: guard_map.start_x,
        y: guard_map.start_y,
        direction: Direction::North,
    };

    let len_y = guard_map.map.len();
    let len_x = guard_map.map[0].len();
    let mut line_map = HashSet::new();
    visited.insert(Index {
        x: guard_map.start_x,
        y: guard_map.start_y,
    });

    loop {
        if !line_map.insert(current) {
            break;
        }

        current = handle_direction(&current, guard_map, &mut visited, len_y, len_x, true);

        if current.direction == Direction::Exit {
            break;
        }
    }

    visited
}

#[must_use]
pub fn part1(input: &str) -> usize {
    let guard_map = read_map(input);
    let hit_map = get_visited(&guard_map);
    hit_map.len()
}

#[must_use]
#[allow(clippy::missing_panics_doc)]
pub fn part2(input: &str) -> usize {
    let mut guard_map = read_map(input);

    let len_y = guard_map.map.len();
    let len_x = guard_map.map[0].len();
    let mut hit_map = HashSet::new();
    let mut found_points = HashSet::new();
    let start = Line {
        x: guard_map.start_x,
        y: guard_map.start_y,
        direction: Direction::North,
    };

    let start_point = Index {
        x: start.x,
        y: start.y,
    };

    let visited = get_visited(&guard_map);
    for visitor in visited {
        if visitor == start_point {
            continue;
        }

        let c = guard_map.map[visitor.y][visitor.x];
        guard_map.map[visitor.y][visitor.x] = '#';
        let cycle = has_cycle(start, &guard_map, &mut hit_map, len_y, len_x);
        if cycle {
            found_points.insert(visitor);
        }

        guard_map.map[visitor.y][visitor.x] = c;
    }

    /*

    loop {
        if !line_map.insert(current) {
            break;
        }

        let iter = get_iterator(&current, len_y, len_x);
        for i in iter.skip(2) {
            let c = match current.direction {
                Direction::North | Direction::South => guard_map.map[i][current.x],
                Direction::East | Direction::West => guard_map.map[current.y][i],
                Direction::Exit => panic!(),
            };

            if c == '#' {
                break;
            }

            match current.direction {
                Direction::North | Direction::South => {
                    guard_map.map[i][current.x] = '#';
                    if do_loop(start, &guard_map, &mut hit_map, len_y, len_x) {
                        found_points.insert(Index { x: current.x, y: i });
                    }

                    guard_map.map[i][current.x] = c;
                }
                Direction::West | Direction::East => {
                    guard_map.map[current.y][i] = '#';
                    if do_loop(start, &guard_map, &mut hit_map, len_y, len_x) {
                        found_points.insert(Index { x: i, y: current.y });
                    }

                    guard_map.map[current.y][i] = c;
                }
                Direction::Exit => panic!(),
            };
        }

        current = handle_direction(&current, &guard_map, &mut hit_map, len_y, len_x, true);
        if current.direction == Direction::Exit {
            break;
        }
    }
     */

    found_points.len()
}

fn read_map(input: &str) -> GuardMap {
    let mut map = Vec::new();
    let mut start_x = 0;
    let mut start_y = 0;
    for (y, line) in input.lines().enumerate() {
        if line.is_empty() {
            continue;
        }

        let mut found = false;
        map.push(
            line.chars()
                .enumerate()
                .map(|(i, c)| {
                    if c == '^' {
                        found = true;
                        start_x = i;
                        return ' ';
                    }

                    c
                })
                .collect(),
        );

        if found {
            start_y = y;
        }
    }

    GuardMap {
        map,
        start_x,
        start_y,
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn part_1_sample() {
        let result = part1(SAMPLE);
        assert_eq!(result, 41);
    }

    #[test]
    fn part_1_measure() {
        let result = part1(MEASURE);
        assert_eq!(result, 4988);
    }

    #[test]
    fn part_2_sample() {
        let result = part2(SAMPLE);
        assert_eq!(result, 6);
    }

    #[test]
    fn part_2_measure() {
        let result = part2(MEASURE);
        assert_eq!(result, 1697);
    }

    const SAMPLE: &str = "....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...";

    const MEASURE: &str = "............#.............#......................#....#....................................................#..............#.......
..................................#...#......#.......#........#..................#....#...........................##....#.........
...........#...........#...........#...#...#...............................#.......................##....................#........
..................#.............#........#.....................#...........................................................#......
....#............................................................#..#...#......#.#......#...#..........#..#.....#....#...#........
.................................#......##.............................#..........................#...#.........#.................
.#.............#.........#..#..............................................................................##...........#..#......
....#..............#..........................##......#........#...#........#..........#........#.............#...................
.....#.......#..#......#..#..................................................................#.........................#..........
......................#...............................#......#................................#......#.....#......................
.......#.........#.....................................................................#....#.#.....................#.........#...
........................#......##..........#....#..........................#....................#...................#.........#...
................#..#............#...#.................................................................#...........#...............
.#.....#.....................#..........................................#................................#........................
.............#.##........#......#......#......................#..................................................#...#..........#.
.......#.........#.#..........#.............#.......#............#.....................#..........................................
.............................#.........................................................#.........#.........#.............#........
....#..#.#..................................................#...........#..................#....#.................................
...............#..........#.....#..............................................................#..................................
.......##.............................................................................#...............#.........#.................
....#.....#..........................#..........#................................#....................#...#.......................
.#.................................................#..#.....#.....................................................................
...........................#..............#.#...#..........................................#......#..........................#....
.#....#........#....................................................#...........................................#.................
...#..................#................##...#.............#..#....................#...............#...............................
..................................................#......#..........................................................#...#.........
#.#....................................#........#.............................................................#...................
...............................................#.......##...............................................#.........................
...#.............#................................................................................................................
.......................................#....#..#........#............#..........#.........#...#..................#................
........#.................................................#.........................................#...........#.................
...................#...........#...............................#..........................#................#..#..........#.....#..
...............#......#...............................................................#............................##..#.....##...
.........................................##.#..#..................#.......#.....................#.................................
...........#...................................................................#...#..................................#.....#.....
......#...............................#.......###..........................#....#.....................................#...........
.....##...............................#...................................................#................................#...#..
..................#....#.......#.......................................................................##.........................
.....#..#.................................................................................................#.......................
.#.................................#......................##....................................#..........................##.....
#.#................#......................................#...........................#.......#...................................
.................................#.......#.....#..............................................##..#....................#..........
............#...............................................................................#....#...........#..........#.........
................................................##...............................................#..#..........................#..
.................#.......#........................................................................#...............................
..#.........................#......#.............#.......................................#........................................
...............#.............#...........................................................#.....#..................................
.........#...............................................................................#...............................#........
.........................................#...................#.................#.........##........#.........#....................
............#..........#.....#................................................................#...##............................#.
........#...#....#..............................................#..............#........................#.........................
..#.....................................................................................................................#.........
.#....#.............................^................#..#...................#.........................#...........................
..........................................................................#.........#....#......#.................................
.........#................................#.#........................#.................#...................................#......
.............................................#.##............................................................#......#..#.........#
..........#............#...........#..#......#..........#.........................................................................
......................#....................................#....#.............##....#.............................................
......................#...#............#.......................................................................................#..
.##.................#.......................#...............#.............#......#..................................#.....#.#.....
....................#........#.......................#....................................#.......#...#.......#.......#......#....
.............................................................#.......................#..........................#.....#...........
...#.................#.......................................................#.......##...........#.......#.......................
...............##.........................#.........................................................#.....#.......#...............
.......................#.......................#.........#..............#....................#..........#........................#
...................................##..........#..........................................................#.........#.............
..................#...............................#...................................#.......#.......................#...........
.......................#..#...................................#........................#........#............#....................
...................................#....#............#..................#........................................#..........#.....
.............#...#...#......#.............##...............................................#..........................#....#......
....................#..........................................#....#......................................................#......
............#.........#...............#..#.....................................................................#.#................
.......................#..#..............................................................#.....#........#..#..................#...
.............#..#..............................#.....#.................#.............#.......................#................#...
...............#......#...........#............#.........#......................................#.................................
............................#.....#.........#.....#..#...........#................................................................
....#.......#.................#..............................................#.......#.#.............................#...#..#.....
..................................................#.#......#............................................................#.........
..................................#............#........................#.......#.........#...#...............#...................
............##....#.#......#..............................................................................#..............#........
.....#..........................................#.........................................................#........##.............
............#.....................#..#.#..............#........#......#.............................#....#...#....................
.......................#...........................................................................#..............................
......#.........................................#........#............#..#..............................#.#.......................
#.#......#..#...............#...........#....#.................................................#..................................
.........#...........#............................#..............#......................#.......................................#.
...#............#..................................#..............#............................#.........................#........
...##..#.....................................................................................#...........................#........
...............#......#.........#................................#...#............#....................#..........................
..........................#...........................#..............................................#................#....#......
.........#........#.#...........................................................................#..#..............................
.................#..............#........................................................................#.................#......
...#.........#.#.........#.#...........................................#........#........#.........#..............................
..........#............................................................#..........#.....#.........................................
..................................#.....#...........##........#......#..........#..........................#......................
...........................#....#.............#............................................................#......................
.............#.......#...................................#.....#............................................................##....
............#..............##.............#.....#...............#..........#........................#.........................#...
..##.....................................................................................................#........................
.............#.#...........#...................#....#...#......#................##................................................
.................#.#............#........................#.......##..#.........##.................................................
......................#.#...#................................................................#...............#.....#...........#..
#.................#..........................#.............................##.....#...........................................#...
..............#.........#.................#................................................................................#......
....#.#............#.......#.......................#.....#.......................#....................#.....#.............#.......
......................#..#....#.........................................#.#............##............#............................
.........#.#...#...#........................................#...............................................#....................#
.......................#...................#.............................................................#.......................#
................#............................#........#..........#.......................##....#..................................
.#...................#......................#.......................................#............................#................
..#..##.......#......#..........#..#..................................................#...........................................
..............#..........#...........................................#.........##.................................................
....#..............................#.........#...#.................#......#.#.......#.............................................
#.................................#........#.....................................#...##...................#..#...........#........
.......#.....#.............................................#..................#...................#...........#..#........#.......
.......................#................#.............#.#.........#..............................................................#
......................................................#....................#..........#................................#..........
#...............#...#.............#.................#.................................................#...........................
..................................................................#.........................#......#.........#............##......
#.................#........#.......#...#....................................#.#...................................................
.....................................#...#....#.........#................#.........................................#...#..........
............#............................#.............#.#........................................................................
......................................#.................................................#...............#.......#.................
.#.#..........#........#.......................#..................................................#....................#..........
....#...............................................................#...........#............#.......................#............
....#.......#.#..#...................#.........#..................................#....................................#..........
..............................................#...................................##.......#......................................
#...#.#............#...............................#.........#...........#..........#............................................#
..#...........#..#..........#.......................#..........##.................................................................
...#.....#..................................#................................................#....................................";
}
