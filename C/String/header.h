#include <stdio.h>
#include <stdlib.h>
#define MAX 555
struct Strings
{
    char String[MAX];
}
typedef struct Strings STRING;

short Delete(STRING *String1, int n);
short Concat(STRING String1, STRING String2, STRING String3, int n1, int n2, int *n3);
short Copy(STRING String1, STRING String2, int n1, int *n2);
short Insert(STRING String1, char String[], int n, int *n1);
