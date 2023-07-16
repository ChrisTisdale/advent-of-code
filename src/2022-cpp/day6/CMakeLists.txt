list(APPEND day6_files "day6.cc")
list(APPEND day6_files "day6.h")
list(APPEND day6_main "main.cc")
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/measurements.txt ${CMAKE_CURRENT_BINARY_DIR}/measurements.txt COPYONLY)
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/sample.txt ${CMAKE_CURRENT_BINARY_DIR}/sample.txt COPYONLY)
add_executable(2022day6 ${day6_files} ${day6_main})
add_custom_target(
        runDay6Test
        COMMAND 2022day6 sample.txt
        DEPENDS 2022day6
)

add_custom_target(
        runDay6
        COMMAND 2022day6 measurements.txt
        DEPENDS 2022day6
)

list(APPEND TEST_FILES "day6Tests.cc")
package_add_test(
        day6_tests
        ${TEST_FILES}
        ${day6_files}
)