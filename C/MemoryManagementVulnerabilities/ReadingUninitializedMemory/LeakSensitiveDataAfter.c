#include <stdio.h>
#include <stdlib.h>
#include <string.h>

// use secure_memset or C23 memset_explicit
// the compiler can optimize away the memset call
void *secure_memset(void *s, int c, size_t n)
{
    volatile unsigned char *p = s;
    while (n--)
        *p++ = c;
    return s;
}

void readSensitiveDataFrom(const char *filename)
{
    FILE *file = fopen(filename, "rb");
    if (file == NULL)
    {
        perror("Error opening file");
        return;
    }

    setvbuf(file, NULL, _IONBF, 0);

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
        memset(buffer, '\0', length);
        free(buffer);
        return;
    }

    memset(buffer, '\0', length);
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