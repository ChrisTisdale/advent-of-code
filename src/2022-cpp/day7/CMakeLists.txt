list(APPEND day7_files "day7.cc")
list(APPEND day7_files "day7.h")
list(APPEND day7_main "main.cc")
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/measurements.txt ${CMAKE_CURRENT_BINARY_DIR}/measurements.txt COPYONLY)
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/sample.txt ${CMAKE_CURRENT_BINARY_DIR}/sample.txt COPYONLY)
add_executable(2022day7 ${day7_files} ${day7_main})
add_custom_target(
        runDay7Test
        COMMAND 2022day7 sample.txt
        DEPENDS 2022day7
)

add_custom_target(
        runDay7
        COMMAND 2022day7 measurements.txt
        DEPENDS 2022day7
)

list(APPEND TEST_FILES "day7Tests.cc")
package_add_test(
        day7_tests
        ${TEST_FILES}
        ${day7_files}
)