#include <stdio.h>
int bendr(char lytis, int aukstis, int *var, int k, char vari[])
{
    int temp=aukstis;
    temp++;//naudojamas tam kad einant giliau i rekursija aukstis butu didesnis
    vari[aukstis]=lytis;
    if(aukstis == k)
    {   int i;
        (*var)++;
        for(i=1; i<=aukstis; printf("%c", vari[i++]));//isspausdinam masyva
        printf("\n");
        return 0;
    }
    if(lytis == 'V') bendr ('M', temp, var, k, vari);
    else
    {
        bendr('M', temp, var, k, vari);
        bendr('V', temp, var, k, vari);
    }
}

int main()
{
    int k, var=0;
    char vari[100];//masyvas i kuri deda variantà

    scanf("%d", &k);

    bendr('M', 1, &var, k, vari);
    bendr('V', 1, &var, k, vari);
    printf("%d", var);
    return 0;
}
