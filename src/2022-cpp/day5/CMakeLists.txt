list(APPEND day5_files "day5.cc")
list(APPEND day5_files "day5.h")
list(APPEND day5_main "main.cc")
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/measurements.txt ${CMAKE_CURRENT_BINARY_DIR}/measurements.txt COPYONLY)
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/sample.txt ${CMAKE_CURRENT_BINARY_DIR}/sample.txt COPYONLY)
add_executable(2022day5 ${day5_files} ${day5_main})
add_custom_target(
        runDay5Test
        COMMAND 2022day5 sample.txt
        DEPENDS 2022day5
)

add_custom_target(
        runDay5
        COMMAND 2022day5 measurements.txt
        DEPENDS 2022day5
)

list(APPEND TEST_FILES "day5Tests.cc")
package_add_test(
        day5_tests
        ${TEST_FILES}
        ${day5_files}
)