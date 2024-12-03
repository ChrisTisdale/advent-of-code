list(APPEND day2_files "day2.cc")
list(APPEND day2_files "day2.h")
list(APPEND day2_main "main.cc")
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/measurements.txt ${CMAKE_CURRENT_BINARY_DIR}/measurements.txt COPYONLY)
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/sample.txt ${CMAKE_CURRENT_BINARY_DIR}/sample.txt COPYONLY)
add_executable(2024day2 ${day2_files} ${day2_main})
add_custom_target(
        run2024Day2Test
        COMMAND 2024day2 sample.txt
        DEPENDS 2024day2
)

add_custom_target(
        run2024Day2
        COMMAND 2024day2 measurements.txt
        DEPENDS 2024day2
)

list(APPEND TEST_FILES "day2Tests.cc")
package_add_test(
        day2_2024_tests
        ${TEST_FILES}
        ${day2_files}
)
