#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int main(int argc, char **argv)
{
    if (argc < 2)
    {
        printf("Usage: %s <string>\n", argv[0]);
        return 1;
    }

    int size = strlen(argv[1]) / 3;
    char *buf1R1 = (char *)malloc(size + 1);
    if (buf1R1 == NULL)
    {
        perror("Failed to allocate memory for buf1R1");
        return 1;
    }
    strncpy(buf1R1, argv[1], size);
    buf1R1[size] = '\0';
    printf("%s\n", buf1R1);
    free(buf1R1);
    buf1R1 = NULL;

    char *buf2R1 = (char *)malloc(size + 1);
    if (buf2R1 == NULL)
    {
        perror("Failed to allocate memory for buf2R1");
        return 1;
    }
    strncpy(buf2R1, argv[1] + size, size);
    buf2R1[size] = '\0';
    printf("%s\n", buf2R1);
    free(buf2R1);
    buf2R1 = NULL;

    char *buf2R2 = (char *)malloc(size + 1);
    if (buf2R2 == NULL)
    {
        perror("Failed to allocate memory for buf2R2");
        return 1;
    }
    strncpy(buf2R1, argv[1] + 2 * size, size);
    buf2R2[size] = '\0';
    printf("%s\n", buf2R2);
    free(buf2R2);
    buf2R2 = NULL;

    return 0;
}
