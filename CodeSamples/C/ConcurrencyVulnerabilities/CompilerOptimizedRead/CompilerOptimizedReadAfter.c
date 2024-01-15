#include <signal.h>

volatile int interrupted = 0;

void handler(int signum) {
    interrupted = 1;
}

int main() {
    signal(SIGINT, handler);
    while (!interrupted) {
        /* do something */
    }
    return 0;
}


