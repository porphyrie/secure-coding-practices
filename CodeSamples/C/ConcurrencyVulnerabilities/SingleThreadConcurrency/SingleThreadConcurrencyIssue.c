#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <signal.h>
#include <unistd.h>

#define MAX_MSG_SIZE 24
char *err_msg;

void handler(int signum) {
    strcpy(err_msg, "SIGINT encountered.");
    printf("%s\n", err_msg);
}

int main(void) {

    signal(SIGINT, handler);

    printf("sleep (2)\n");
    sleep(2);

    err_msg = (char *)malloc(MAX_MSG_SIZE);
    if (err_msg == NULL) {
        return 1;
    }

    printf("sleep (2)\n");
    sleep(2);

    strcpy(err_msg, "No errors yet.");
    printf("%s\n", err_msg);

    free(err_msg);
    return 0;
}
