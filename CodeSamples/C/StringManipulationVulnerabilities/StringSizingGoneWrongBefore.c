#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include <uchar.h>

int main()
{
    char16_t str1[] = u"Hello, 🌍!";

    size_t length = strlen((char *)str1);

    printf("The length of the string is %ld\n", length);

    char16_t *str2 = (char16_t *)malloc((length + 1) * sizeof(char16_t));
    if (str2 == NULL)
    {
        perror("Failed to allocate memory");
        return 1;
    }

    strncpy((char *)str2, (char *)str1, length);
    str2[length] = u'\0';

    // ... Use str2 ...

    free(str2);
    str2 = NULL;
    return 0;
}
