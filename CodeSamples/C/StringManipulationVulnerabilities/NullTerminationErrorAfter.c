#include <stdio.h>
#include <string.h>

int main(void) {
    char a[6];
    char b[9] = " it's me";
    
    strncpy(a, "Hello", sizeof(a));
    a[sizeof(a)-1] = '\0';
    
    printf("%s\n", a);

    return 0;
}

