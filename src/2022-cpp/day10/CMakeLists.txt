list(APPEND day10_files "day10.cc")
list(APPEND day10_files "day10.h")
list(APPEND day10_main "main.cc")
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/measurements.txt ${CMAKE_CURRENT_BINARY_DIR}/measurements.txt COPYONLY)
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/sample.txt ${CMAKE_CURRENT_BINARY_DIR}/sample.txt COPYONLY)
add_executable(2022day10 ${day10_files} ${day10_main})
add_custom_target(
        run2022Day10Test
        COMMAND 2022day10 sample.txt
        DEPENDS 2022day10
)

add_custom_target(
        run2022Day10
        COMMAND 2022day10 measurements.txt
        DEPENDS 2022day10
)

list(APPEND TEST_FILES "day10Tests.cc")
package_add_test(
        day10_20221_tests
        ${TEST_FILES}
        ${day10_files}
)
