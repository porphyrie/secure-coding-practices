#include <stdio.h>
#include <string.h>

int main()
{
    char buffer[16] = "Hello, ";
    char input[32];

    printf("Enter a string to concatenate: ");
    fgets(input, sizeof(input), stdin);

    strncat(buffer, input, sizeof(buffer) - strlen(buffer) - 1);

    printf("Result: %s\n", buffer);

    return 0;
}

