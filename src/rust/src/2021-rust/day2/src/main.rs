use std::fs;

fn main() {
    let mut count: i32 = 0;

    let data = fs::read_to_string("assets/measurements.txt")
        .unwrap()
        .lines()
        .filter(|s| !s.is_empty())
        .map(|s| s.parse::<i32>().unwrap())
        .collect::<Vec<i32>>();

    println!("{}", count)
}
