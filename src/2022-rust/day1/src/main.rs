use std::fs;

fn main() {
    let mut count: i32 = 0;

    let data = fs::read_to_string("assets/day1/measurements.txt")
        .unwrap()
        .lines()
        .filter(|s | !s.is_empty())
        .map(|s | {
            s.parse::<i32>().unwrap()
        }).collect::<Vec<i32>>();

    for (i, _value) in data.iter().enumerate() {
        if i > data.len() - 4 {
            break;
        }

        let pre_total: i32 = data[i..(i + 3)].iter().sum();
        let cur_total: i32 = data[(i + 1)..(i + 4)].iter().sum();
        if pre_total < cur_total {
            count = count + 1;
        }
    }

    println!("{}", count)
}
