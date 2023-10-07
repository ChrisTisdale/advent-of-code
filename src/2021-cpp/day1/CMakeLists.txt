list(APPEND day1_files "day1.cc")
list(APPEND day1_files "day1.h")
list(APPEND day1_main "main.cc")
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/measurements.txt ${CMAKE_CURRENT_BINARY_DIR}/measurements.txt COPYONLY)
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/sample.txt ${CMAKE_CURRENT_BINARY_DIR}/sample.txt COPYONLY)
add_executable(2021day1 ${day1_files} ${day1_main})
add_custom_target(
    run2021Day1Test
    COMMAND 2021day1 sample.txt
    DEPENDS 2021day1
)

add_custom_target(
    run2021Day1
    COMMAND 2021day1 measurements.txt
    DEPENDS 2021day1
)

list(APPEND TEST_FILES "day1Tests.cc")
package_add_test(
  day1_2021_tests
  ${TEST_FILES}
  ${day1_files}
)
