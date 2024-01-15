#include <stdio.h>
#include <string.h>

int main() {
    char buffer[16];
    char input[] = "This is a very long string that will cause a buffer overflow";

    strncpy(buffer, input, sizeof(buffer) - 1);
    buffer[sizeof(buffer) - 1] = '\0';

    printf("Buffer: %s\n", buffer);

    return 0;
}

