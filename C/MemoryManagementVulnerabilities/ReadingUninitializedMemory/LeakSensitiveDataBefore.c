#include <stdio.h>
#include <stdlib.h>
#include <string.h>

void readSensitiveDataFrom(const char *filename)
{
    FILE *file = fopen(filename, "rb");
    if (file == NULL)
    {
        perror("Error opening file");
        return;
    }

    fseek(file, 0, SEEK_END);
    long length = ftell(file);
    rewind(file);

    char *buffer = malloc(length);
    if (buffer == NULL)
    {
        perror("Memory allocation failed");
        fclose(file);
        return;
    }

    if (fread(buffer, 1, length, file) != length)
    {
        perror("Error reading file");
        fclose(file);
        free(buffer);
        return;
    }

    free(buffer);
    fclose(file);
}

void exposeHeapDataTo(const char *filename)
{
    size_t bufferSize = 1024 * sizeof(char);

    char *buffer = (char *)malloc(bufferSize);
    if (buffer == NULL)
    {
        perror("Failed to allocate memory");
        return;
    }

    strcpy(buffer, "Pretend to read some data into the buffer");

    FILE *file = fopen(filename, "wb");
    if (file == NULL)
    {
        perror("Error opening file");
        free(buffer);
        return;
    }

    if (fwrite(buffer, 1, bufferSize, file) != bufferSize)
    {
        perror("Error writing to file");
    }

    free(buffer);
    fclose(file);
}

int main()
{
    readSensitiveDataFrom("/etc/passwd");
    printf("%s", "The memory was deallocated but the sesitive data still resides in the heap mwahahaha\n");
    exposeHeapDataTo("output.bin");
    printf("%s", "Whoops! Some data got leaked...\n");
    return 0;
}