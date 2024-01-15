#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int main() {

    const char s1[3] = "abc";
    const char s2[3] = "def";

    printf("Size of s1: %ld\n", sizeof(s1));
    printf("Length of s1: %ld\n", strlen(s1));
    printf("s1: %s\n", s1);

    return 0;
}


