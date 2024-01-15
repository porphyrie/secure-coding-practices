#include <stdio.h>
#include <string.h>

int main() {
    char buffer[16]; 
    char input[] = "This is a very long string that will cause a buffer overflow";

    strcpy(buffer, input);

    printf("Buffer: %s\n", buffer);

    return 0;
}

