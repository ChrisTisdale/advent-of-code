use std::collections::HashMap;
use std::fs;

#[allow(dead_code)]
struct File {
    name: String,
    size: usize,
}

struct SizeResult {
    name: String,
    size: usize,
}

struct Folder {
    directories: Vec<String>,
    files: Vec<File>,
}

fn main() {
    let mut map: HashMap<String, Folder> = HashMap::new();
    let mut current_dir = String::new();
    for d in fs::read_to_string("assets/day7/measurements.txt")
        .unwrap()
        .lines()
        .filter(|s| !s.is_empty())
        .map(|s| s.split(' ').collect::<Vec<&str>>())
    {
        if d[0] == "$" {
            if d[1] == "cd" {
                if d[2] == ".." {
                    let par = find_parent(&current_dir.to_string(), &map);
                    current_dir = par;
                    continue;
                }

                current_dir.push_str(d[2]);
                assert!(!map.contains_key(current_dir.as_str()), "We have a duplicate: {current_dir}");

                map.insert(
                    current_dir.to_string(),
                    Folder {
                        files: Vec::new(),
                        directories: Vec::new(),
                    },
                );
            }

            continue;
        }

        if d[0] == "dir" {
            let mut new_dir = current_dir.clone();
            new_dir.push_str(d[1]);
            let value = map
                .get_mut(current_dir.as_str())
                .expect("aoc wont lie to me");
            value.directories.push(new_dir);
        } else {
            let value = map
                .get_mut(current_dir.as_str())
                .expect("aoc wont lie to me");
            value.files.push(File {
                name: d[1].to_string(),
                size: d[0].parse::<usize>().unwrap(),
            });
        }
    }

    let root = map.get("/").expect("aoc is not a liar");
    let mut min_size = cal_folder_size(root, &map);
    let free_space = 70_000_000 - min_size;
    let need_free = 30_000_000 - free_space;

    println!(
        "Root size: {min_size}, need: {need_free}, free space: {free_space}"
    );
    for d in &root.directories {
        let f = map.get(d).expect("aoc isn't a liar");
        let size = find_min_size(&mut min_size, f, &map, &need_free, d);
        match size {
            None => {}
            Some(d) => {
                min_size = {
                    if d.size < min_size {
                        d.size
                    } else {
                        min_size
                    }
                }
            }
        }
    }

    println!("Sizes to remove: {min_size}");
}

fn find_min_size(
    cur: &mut usize,
    folder: &Folder,
    map: &HashMap<String, Folder>,
    needed: &usize,
    name: &str,
) -> Option<SizeResult> {
    let cal = cal_folder_size(folder, map);
    if folder.directories.is_empty() && cal < *cur && cal >= *needed {
        *cur = cal;
        return Some(SizeResult {
            name: name.to_owned(),
            size: cal,
        });
    }

    let mut result = None;

    for d in &folder.directories {
        let f = map.get(d.as_str()).expect("aoc isn't a lair");
        let res = find_min_size(cur, f, map, needed, d);
        if res.is_none() {
            continue;
        }

        result = Option::from(match result {
            None => res.unwrap(),
            Some(d) => get_change(&d, &res.unwrap()),
        });
    }

    match result {
        None => {
            if cal < *cur && cal >= *needed {
                *cur = cal;
                Some(SizeResult {
                    name: name.to_owned(),
                    size: cal,
                })
            } else {
                None
            }
        }
        Some(_) => result,
    }
}

fn get_change(cur: &SizeResult, new: &SizeResult) -> SizeResult {
    if cur.size < new.size {
        return SizeResult {
            name: new.name.clone(),
            size: new.size,
        };
    }

    SizeResult {
        name: cur.name.clone(),
        size: cur.size,
    }
}

fn find_parent(name: &String, map: &HashMap<String, Folder>) -> String {
    for (s, f) in map {
        if !f.directories.contains(name) {
            continue;
        }

        return s.clone();
    }

    String::new()
}

fn cal_folder_size(folder: &Folder, map: &HashMap<String, Folder>) -> usize {
    let mut size: usize = 0;
    for f in &folder.directories {
        size += cal_folder_size(map.get(f.as_str()).expect("aoc won't lie"), map);
    }

    for f in &folder.files {
        size += f.size;
    }

    size
}
