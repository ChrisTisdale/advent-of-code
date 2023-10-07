list(APPEND day3_files "day3.cc")
list(APPEND day3_files "day3.h")
list(APPEND day3_main "main.cc")
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/measurements.txt ${CMAKE_CURRENT_BINARY_DIR}/measurements.txt COPYONLY)
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/sample.txt ${CMAKE_CURRENT_BINARY_DIR}/sample.txt COPYONLY)
add_executable(2022day3 ${day3_files} ${day3_main})
add_custom_target(
    run2022Day3Test
    COMMAND 2022day3 sample.txt
    DEPENDS 2022day3
)

add_custom_target(
    run2022Day3
    COMMAND 2022day3 measurements.txt
    DEPENDS 2022day3
)

list(APPEND TEST_FILES "day3Tests.cc")
package_add_test(
  day3_20221_tests
  ${TEST_FILES}
  ${day3_files}
)
