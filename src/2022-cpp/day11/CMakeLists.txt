list(APPEND day11_files "day11.cc")
list(APPEND day11_files "day11.h")
list(APPEND day11_main "main.cc")
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/measurements.txt ${CMAKE_CURRENT_BINARY_DIR}/measurements.txt COPYONLY)
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/sample.txt ${CMAKE_CURRENT_BINARY_DIR}/sample.txt COPYONLY)
add_executable(2022day11 ${day11_files} ${day11_main})
add_custom_target(
        runDay11Test
        COMMAND 2022day11 sample.txt
        DEPENDS 2022day11
)

add_custom_target(
        runDay11
        COMMAND 2022day11 measurements.txt
        DEPENDS 2022day11
)

list(APPEND TEST_FILES "day11Tests.cc")
package_add_test(
        day11_tests
        ${TEST_FILES}
        ${day11_files}
)
