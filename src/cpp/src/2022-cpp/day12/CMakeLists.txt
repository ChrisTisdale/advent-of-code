list(APPEND day12_files "day12.cc")
list(APPEND day12_files "day12.h")
list(APPEND day12_main "main.cc")
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/measurements.txt ${CMAKE_CURRENT_BINARY_DIR}/measurements.txt COPYONLY)
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/sample.txt ${CMAKE_CURRENT_BINARY_DIR}/sample.txt COPYONLY)
add_executable(2022day12 ${day12_files} ${day12_main})
add_custom_target(
        run2022Day12Test
        COMMAND 2022day12 sample.txt
        DEPENDS 2022day12
)

add_custom_target(
        run2022Day12
        COMMAND 2022day12 measurements.txt
        DEPENDS 2022day12
)

list(APPEND TEST_FILES "day12Tests.cc")
package_add_test(
        day12_20221_tests
        ${TEST_FILES}
        ${day12_files}
)
