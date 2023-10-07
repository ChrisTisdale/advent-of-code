list(APPEND day9_files "day9.cc")
list(APPEND day9_files "day9.h")
list(APPEND day9_main "main.cc")
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/measurements.txt ${CMAKE_CURRENT_BINARY_DIR}/measurements.txt COPYONLY)
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/samplePart1.txt ${CMAKE_CURRENT_BINARY_DIR}/samplePart1.txt COPYONLY)
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/samplePart2.txt ${CMAKE_CURRENT_BINARY_DIR}/samplePart2.txt COPYONLY)
add_executable(2022day9 ${day9_files} ${day9_main})

add_custom_target(
        run2022Day9
        COMMAND 2022day9 measurements.txt
        DEPENDS 2022day9
)

list(APPEND TEST_FILES "day9Tests.cc")
package_add_test(
        day9_20221_tests
        ${TEST_FILES}
        ${day9_files}
)
