#include <stdio.h>
#include <string.h>

int IsPasswordOK() {
    char password[30];
    scanf("%s", password);
    return strcmp(password, "SCOOBYDOO") == 0;
}

int main() {
    printf("Enter password: ");
    int PwStatus = IsPasswordOK();
    if (!PwStatus) {
        printf("Access denied\n");  
    } else {
        printf("Access granted\n");
    }
    return 0;
}
