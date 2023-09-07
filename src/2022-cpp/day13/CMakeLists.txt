list(APPEND day13_files "day13.cc")
list(APPEND day13_files "day13.h")
list(APPEND day13_main "main.cc")
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/measurements.txt ${CMAKE_CURRENT_BINARY_DIR}/measurements.txt COPYONLY)
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/sample.txt ${CMAKE_CURRENT_BINARY_DIR}/sample.txt COPYONLY)
add_executable(2022day13 ${day13_files} ${day13_main})
add_custom_target(
        runDay13Test
        COMMAND 2022day13 sample.txt
        DEPENDS 2022day13
)

add_custom_target(
        runDay13
        COMMAND 2022day13 measurements.txt
        DEPENDS 2022day13
)

list(APPEND TEST_FILES "day13Tests.cc")
package_add_test(
        day13_tests
        ${TEST_FILES}
        ${day13_files}
)
