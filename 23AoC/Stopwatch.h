#include <iostream>
#include <chrono>

class Stopwatch {
public:
    // Start the stopwatch
    Stopwatch() : start_time_(std::chrono::high_resolution_clock::now()) {}

    // Restart the stopwatch
    void restart() {
        start_time_ = std::chrono::high_resolution_clock::now();
    }

    // Returns elapsed time in milliseconds
    double elapsed() const {
        auto end_time = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::microseconds>(end_time - start_time_);
        return duration.count() / 1e3; // Convert microseconds to milliseconds
    }

private:
    std::chrono::time_point<std::chrono::high_resolution_clock> start_time_;
};