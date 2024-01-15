#include <signal.h>

volatile int interrupted = 0;

void handler(int signum) {
    int localInterrupted = interrupted;
    localInterrupted = 1;
    interrupted = localInterrupted;
}

int main() {
    signal(SIGINT, handler);
    while (!interrupted) {
        /* do something */
    }
    return 0;
}


