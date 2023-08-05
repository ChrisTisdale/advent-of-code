use std::{
    env, fs,
    path::{Path, PathBuf},
};

const COPY_DIR: &'static str = "assets";

/// A helper function for recursively copying a directory.
fn copy_dir<P, Q>(from: P, to: Q)
where
    P: AsRef<Path>,
    Q: AsRef<Path>,
{
    let to = to.as_ref().to_path_buf();

    for path in fs::read_dir(from).unwrap() {
        let path = path.unwrap().path();
        let to = to.clone().join(path.file_name().unwrap());

        if path.is_file() {
            fs::copy(&path, to).unwrap();
        } else if path.is_dir() {
            if !to.exists() {
                fs::create_dir(&to).unwrap();
            }

            copy_dir(&path, to);
        } else { /* Skip other content */
        }
    }
}

fn get_output_path() -> PathBuf {
    //<root or manifest path>/target/<profile>/
    let manifest_dir_string = env::var("OUT_DIR").unwrap();
    let path = Path::new(&manifest_dir_string)
        .parent()
        .unwrap()
        .parent()
        .unwrap()
        .parent()
        .unwrap()
        .join(COPY_DIR);
    return PathBuf::from(path);
}

fn get_source_path() -> PathBuf {
    let manifest_dir_string = env::var("CARGO_MANIFEST_DIR").unwrap();
    let path = Path::new(&manifest_dir_string).join(COPY_DIR);
    return PathBuf::from(path);
}

fn main() {
    let target_dir = get_output_path();
    let source = get_source_path();
    if !target_dir.exists() {
        fs::create_dir(&target_dir).unwrap();
    }
    copy_dir(source, target_dir)
}
