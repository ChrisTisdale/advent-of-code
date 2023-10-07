list(APPEND day4_files "day4.cc")
list(APPEND day4_files "day4.h")
list(APPEND day4_main "main.cc")
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/measurements.txt ${CMAKE_CURRENT_BINARY_DIR}/measurements.txt COPYONLY)
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/sample.txt ${CMAKE_CURRENT_BINARY_DIR}/sample.txt COPYONLY)
add_executable(2022day4 ${day4_files} ${day4_main})
add_custom_target(
    run2022Day4Test
    COMMAND 2022day4 sample.txt
    DEPENDS 2022day4
)

add_custom_target(
    run2022Day4
    COMMAND 2022day4 measurements.txt
    DEPENDS 2022day4
)

list(APPEND TEST_FILES "day4Tests.cc")
package_add_test(
  day4_20221_tests
  ${TEST_FILES}
  ${day4_files}
)
