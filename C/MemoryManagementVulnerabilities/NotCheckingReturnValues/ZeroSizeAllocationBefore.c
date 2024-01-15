#include <stdio.h>
#include <stdlib.h>
#include <time.h>

char *generateRandomCharArray(int size)
{
    char *array = (char *)malloc((size + 1) * sizeof(char));
    if (array == NULL)
    {
        perror("Memory allocation failed");
        return NULL;
    }

    srand(time(NULL));

    for (int i = 0; i < size; i++)
    {
        array[i] = 'a' + (rand() % 26);
    }

    array[size]='\0';

    return array;
}

int main()
{
    int size;
    printf("Enter the size of the random string: ");
    scanf("%d", &size);

    char *randomArray = generateRandomCharArray(size);
    if (randomArray != NULL)
    {
        printf("%s\n", randomArray);
        free(randomArray);
    }
    else
    {
        printf("Failed to allocate memory or invalid size.\n");
    }

    return 0;
}
