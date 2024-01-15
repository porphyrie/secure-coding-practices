#include <stdio.h>
#include <string.h>
#include <stdlib.h>

int main() {
    char input[] = "This is a very long string that won't cause a buffer overflow";
    
    char* buffer = (char*)malloc(strlen(input) + 1);
    if (buffer == NULL) {
        fprintf(stderr, "Memory allocation failed.\n");
        return 1;
    }

    strcpy(buffer, input);

    printf("Buffer: %s\n", buffer);

    free(buffer);

    return 0;
}

