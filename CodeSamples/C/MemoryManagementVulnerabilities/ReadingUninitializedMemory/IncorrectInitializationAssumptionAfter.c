#include <stdio.h>
#include <stdlib.h>

void populateMemoryWithSomeData() {
    int *array = (int *)malloc(3000 * sizeof(int));

    if (array == NULL) {
        printf("Memory allocation failed.\n");
        return;
    }

    for (int i = 0; i < 3000; i++) {
        array[i] = 1000 + i;
    }

    free(array);
}

int main() {

    populateMemoryWithSomeData();

    int size = 10;

    int *array1 = (int *)calloc(size, sizeof(int));
    int *array2 = (int *)calloc(size, sizeof(int));

    if (array1 == NULL || array2 == NULL) {
        printf("Memory allocation failed.\n");
        free(array1);  
        free(array2); 
        return 1;
    }

    for (int i = 0; i < size; i++) {
        array2[i] = i * 2;
        array1[i] += array2[i];
        printf("%d ", array1[i]);
    }

    printf("%s", "\n");

    free(array1);
    free(array2);

    return 0;
}
