# CMakeList.txt : Top-level CMake project file, do global configuration
# and include sub-projects here.
#
cmake_minimum_required(VERSION 3.20)
set(CMAKE_EXPORT_COMPILE_COMMANDS 1)

# Enable Hot Reload for MSVC compilers if supported.
if (POLICY CMP0141)
    cmake_policy(SET CMP0141 NEW)
    set(CMAKE_MSVC_DEBUG_INFORMATION_FORMAT "$<IF:$<AND:$<C_COMPILER_ID:MSVC>,$<CXX_COMPILER_ID:MSVC>>,$<$<CONFIG:Debug,RelWithDebInfo>:EditAndContinue>,$<$<CONFIG:Debug,RelWithDebInfo>:ProgramDatabase>>")
endif ()

project("advent-of-code")

set(CMAKE_CXX_STANDARD 23)
set(gtest_force_shared_crt on)
set(CMAKE_CXX_STANDARD_REQUIRED ON CACHE BOOL "" FORCE)

option(PACKAGE_TESTS "Build the tests" ON)
if (PACKAGE_TESTS)
    include(GoogleTest)
    find_package(GTest CONFIG REQUIRED)
    mark_as_advanced(
            BUILD_GMOCK BUILD_GTEST BUILD_SHARED_LIBS
            GTest::gmock_build_tests GTest::gtest_build_samples GTest::gtest_build_tests
            GTest::gtest_disable_pthreads GTest::gtest_force_shared_crt GTest::gtest_hide_internal_symbols
    )
endif ()

macro(package_add_test TEST_NAME)
    if (PACKAGE_TESTS)
        enable_testing()
        # create an executable in which the tests will be stored
        add_executable(${TEST_NAME} ${ARGN})
        # link the Google test infrastructure, mocking library, and a default main function to
        # the test executable.  Remove g_test_main if writing your own main function.
        target_link_libraries(${TEST_NAME} PRIVATE GTest::gtest GTest::gmock GTest::gtest_main)
        # gtest_discover_tests replaces gtest_add_tests,
        # see https://cmake.org/cmake/help/v3.10/module/GoogleTest.html for more options to pass to it
        gtest_discover_tests(${TEST_NAME})
        set_target_properties(${TEST_NAME} PROPERTIES FOLDER tests)
    endif ()
endmacro()

if (PACKAGE_TESTS)
    enable_testing()
endif ()

add_subdirectory("src/2024-cpp")
add_subdirectory("src/2022-cpp")
add_subdirectory("src/2021-cpp")
