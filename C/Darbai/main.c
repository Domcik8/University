/*Dominik Lisovski:
Turime N darbų, kurių atlikimo trukmė t1, t2,..tN¬, kurių baigimo terminai
d1, d2,..dN. Jei darbai neatliekami laiku, bauda atitinkamai b1, b2,..bN.
Kokia eilės tvarka atlikti darbus, kad bauda būtų minimali.
*/
#include "main.h"

int main()
{
    const char *IsvedimoF = "Rezultatas.txt";
    FILE *Rez;
    PLAN *plan1 = NULL, *plan2 = NULL;
    int i, j = 0, n1 = 0, n2 = 0, n3 = 0, workOrder[LN], currOrder[LN], minPenalty = -1, currPenalty = 0, hour = 0, availability[LN], l = 0;
    ReadData(&plan1, &n1);///Gavau is kitos puses ( eiti su prev)
    SortData(plan1, &plan2, &n2, n1);
    for (i = 0; i < n2; i++) availability[i] = 1;
    ChooseBestPlan(plan2, n2, workOrder, currOrder, &minPenalty, currPenalty, hour, j, availability, l, &n3);
    WriteData(plan2, n2, n3, workOrder, plan1, n1);
    Delete(&plan1), Delete(&plan2);
    return 0;
}
