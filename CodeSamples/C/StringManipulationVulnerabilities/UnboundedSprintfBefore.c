#include <stdio.h>

int main() {
    char buffer[20];
    char input[] = "This input is too long for the buffer";

    sprintf(buffer, "%s", input);

    printf("Buffer: %s\n", buffer);

    return 0;
}
