#include "header.h"

int main(int argc, char *argv[])
{
    char String[MAX];
    int n1, n2, n3, n;
    STRING String1, String2, String3;
    String = "ABC";
    Insert(&String1, String, n, &n1);
    String = "BCD";
    Insert(&String2, String, n, &n2);
    String = "CDE";
    Insert(&String3, String, n, &n3);
    Delete(String1, n1);
    Concat(String1, String2, String3, n1, n2, &n3);
    Copy(String1, String2, n1, &n2);
  system("PAUSE");	
  return 0;
}
