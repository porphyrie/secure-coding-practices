#include <stdio.h>
int main() {
    char *str1 = "Hello, World!";
    char *str2 = "Hello, World!";

    printf("str1 address: %p\n", str1);
    printf("str2 address: %p\n", str2);
   
    str1[0] = 'h';

    printf("str1: %s\n", str1);
    printf("str2: %s\n", str2);

    return 0;
}

