#include <iostream>
#include <atomic>
#include <thread>

static std::atomic<int> UnitsSold = 0;

void IncrementUnitsSold()
{
    int expected;
    do
    {
        expected = UnitsSold.load(std::memory_order_relaxed);
    } while (!UnitsSold.compare_exchange_strong(expected, expected + 1, std::memory_order_relaxed));
}

int main()
{
    const int NUM_THREADS = 100;
    std::thread threads[NUM_THREADS];

    for (int i = 0; i < NUM_THREADS; ++i)
    {
        threads[i] = std::thread(IncrementUnitsSold);
    }

    for (int i = 0; i < NUM_THREADS; ++i)
    {
        threads[i].join();
    }

    if (UnitsSold < NUM_THREADS)
        std::cout << "UnitsSold: " << UnitsSold << " !!!!!!!" << std::endl;
    else
        std::cout << "UnitsSold: " << UnitsSold << std::endl;

    return 0;
}

