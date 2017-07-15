#include <stdlib.h>
#include <stdio.h>
#define MAX 21
#define LN 255
#define input "Duomenys.txt"
#define output "Rezultatas.txt"

struct PlanOfWork
{
    char work[MAX];
    int time;
    int finish;
    int penalty;
    short availability;
    struct PlanOfWork *next;
    struct PlanOfWork *prev;
};
typedef struct PlanOfWork PLAN;

short ReadData(PLAN **plan, int *n);
short SortData(PLAN *plan1, PLAN **plan2, int *n2, int n1);
short ChooseBestPlan(PLAN *plan, int n, int order[], int curro[], int *minp, int currp, int hour, int j, int availability[], int l, int *n2);
short WriteData(PLAN *plan, int n2, int n, int order[], PLAN *plan1, int n1);
