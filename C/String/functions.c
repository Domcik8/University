#include "header.h"
short Delete(STRING *String1, int n)
{
      int i;
      for (i = 0; i < n; String->String[i++] = 0);
      return 0;     
}
short Concat(STRING String1, STRING String2, STRING String3, int n1, int n2, int *n3)
{
      int i;
      for (i = 0; i < n1; i++)
      {
          (*n3)++;
          String3->String[i] = String1->String[i];
      } 
      for (i = 0; i < n2; i++)
      {
          (*n3)++;
          String3->String[i + n2] = String2->String[i];
      }
      return 0;
}
short Copy(STRING String1, STRING String2, int n1, int *n2)
{
      int i;
      for (i = 0; i < n1; String2->String[i] = String1->String[i++]);
      (*n2)++ = n1;
      return 0;
}
short Insert(STRING String1, char String[], int n, *n1);
{
      int i, ;
      for (i = 0; i < n; i++) String1->String[i] = String[i];
      (*n1) = n;
      return 0;
}
      
