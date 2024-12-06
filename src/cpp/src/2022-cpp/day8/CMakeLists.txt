list(APPEND day8_files "day8.cc")
list(APPEND day8_files "day8.h")
list(APPEND day8_main "main.cc")
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/measurements.txt ${CMAKE_CURRENT_BINARY_DIR}/measurements.txt COPYONLY)
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/sample.txt ${CMAKE_CURRENT_BINARY_DIR}/sample.txt COPYONLY)
add_executable(2022day8 ${day8_files} ${day8_main})
add_custom_target(
        run2022Day8Test
        COMMAND 2022day8 sample.txt
        DEPENDS 2022day8
)

add_custom_target(
        run2022Day8
        COMMAND 2022day8 measurements.txt
        DEPENDS 2022day8
)

list(APPEND TEST_FILES "day8Tests.cc")
package_add_test(
        day8_20221_tests
        ${TEST_FILES}
        ${day8_files}
)
