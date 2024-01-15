#include <iostream>
#include <mutex>
#include <thread>

static int UnitsSold = 0;
static std::mutex unitsSoldMutex;

void IncrementUnitsSold()
{
    std::lock_guard<std::mutex> lock(unitsSoldMutex);
    UnitsSold = UnitsSold + 1;
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

