#include <stdio.h>
#include <string.h>

int main(void) {
    char a[5];
    char b[9] = " it's me";
    
    strncpy(a, "Hello", sizeof(a));
    
    printf("%s\n", a);

    return 0;
}

