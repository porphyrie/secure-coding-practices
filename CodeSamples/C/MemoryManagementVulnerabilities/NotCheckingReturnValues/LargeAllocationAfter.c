#include <stdlib.h>
#include <stdio.h>

int main() {
    size_t largeSize = (size_t)-1;
    int *largeArray = malloc(largeSize * sizeof(int));
    
    if (largeArray == NULL) {
        fprintf(stderr, "Memory allocation failed\n");
        return 1;
    }

    largeArray[0] = 10;
    printf("First element: %d\n", largeArray[0]);

    free(largeArray);
    return 0;
}


