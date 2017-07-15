#include "main.h"
double CodeStr = 0;
short ReadData(PLAN **plan, int *n){
    FILE *Duom;
    PLAN *curr;
    char buffer[LN] = {0};
    int i;
    Duom = fopen(input, "r");
    if (Duom == NULL)
    {
        printf("Could not open file %s.\n", input);
        return 1;
    }
    while (fgets(buffer, (MAX - 1), Duom) != NULL)
    {
        (*n)++;
        curr = (PLAN*) malloc(sizeof(PLAN));
        if (curr == NULL)
        {
            printf("There is not enough memory");
            return 2;
        }
        curr->prev = (*plan);
        for (i = 0; i < 20; i++) curr->work[i] = buffer[i];
        fscanf (Duom, "%d", &curr->time);
        fscanf (Duom, "%d", &curr->finish);
        fscanf (Duom, "%d", &curr->penalty);
        curr->availability = 1;
        if ((*plan) != NULL)
        {
            curr->prev = (*plan);
            (*plan)->next = curr;
            (*plan) = (*plan)->next;
            (*plan)->next = NULL;
        }
        else
        {
            curr->prev = NULL;
            (*plan) = curr;
            curr->next = NULL;
        }
        fgets(buffer, LN, Duom);
    }
    fclose(Duom);
    return 0;
}
short SortData(PLAN *plan1, PLAN **plan2, int *n2, int n1)
{
    PLAN *temp = plan1, *curr = NULL;
    int cicle = n1, i, j;
    int max;
    while (cicle != 0)
    {
        max = 0;
        plan1 = temp;
        for(i = 0; i < n1; i++)
        {
            if((plan1->availability == 1) && (plan1->finish > max)) max = plan1->finish;
            plan1 = plan1->prev;
        }
        plan1 = temp;
        for(i = 0; i < n1; i++)
        {
            if((plan1->availability == 1) && (plan1->time > plan1->finish))
            {
                plan1->availability = 2;
                cicle--;
            }
            if((plan1->availability == 1) && (plan1->finish == max))
                {
                    curr = (PLAN*)malloc(sizeof(PLAN));
                    if (curr == NULL)
                    {
                        printf("There is not enough memory");
                        return 2;
                    }
                    if ((*plan2) != NULL)
                    {
                        curr->next = (*plan2);
                        (*plan2)->prev = curr;
                        (*plan2) = (*plan2)->prev;
                        (*plan2)->prev = NULL;
                    }
                    else
                    {
                        curr->next = NULL;
                        (*plan2) = curr;
                        curr->prev = NULL;
                    }
                    for (j = 0; j < 20; j++)
                    {
                        (*plan2)->work[j] = plan1->work[j];
                    }
                    (*plan2)->time = plan1->time;
                    (*plan2)->finish = plan1->finish;
                    (*plan2)->penalty = plan1->penalty;
                    (*plan2)->availability = 1;
                    (*n2)++;
                    plan1->availability = 0;
                    cicle--;
                }
            plan1 = plan1->prev;
        }
    }
    return 0;
}
short ChooseBestPlan(PLAN *plan, int n, int order[], int curro[], int *minp, int currp, int hour, int j, int availability[], int l, int *n2)
{
    int i, avail[LN], k, cicle = j;
    if (j == n)
    {
        if ((*minp) == -1) (*minp) = currp;
        if (currp <= (*minp))
        {
            (*minp) = currp;
            for (i = 0; i <= l; i++) order[i] = curro[i];
            (*n2) = l;
        }
        return 0;
    }
    while (plan->prev != NULL) {plan = plan->prev; CodeStr++;}
    for(i = 0; i < n; i++) {avail[i] = availability[i]; CodeStr++;}
    for(i = 0; i < n; i++)
    {
        CodeStr++;
        if (((*minp) != -1) && ((*minp) <= currp)) return 0;
        if (avail[i] == 1)
        {
            if (((plan->time + hour) > plan->finish)&&(avail[i] == 1))
            {
                avail[i] = 0;
                ChooseBestPlan(plan, n, order, curro, minp, currp + plan->penalty, hour, j + 1, avail, l, n2);
                if ((j + 1) == n) return 0;
                cicle++;
                if (cicle == n) return 0;
                for(k = 0; k < n; k++) avail[k] = availability[k];
            }
            else
            {
                avail[i] = 0;
                curro[l] = i + 1;
                l++;
                ChooseBestPlan(plan, n, order, curro, minp, currp, hour + plan->time, j + 1, avail, l, n2);
                l--;
                if ((j + 1) == n) return 0;
                cicle++;
                if (cicle == n) return 0;
                for(k = 0; k < n; k++) avail[k] = availability[k];
            }
        }
        if (plan->next != NULL) plan = plan->next;
    }
    ChooseBestPlan(plan, n, order, curro, minp, currp, hour, j, avail, l, n2);
    return 0;
}
short WriteData(PLAN *plan, int n2, int n, int order[], PLAN *plan1, int n1)
{
    int i = 1, j, l = 1;
    PLAN *temp;
    FILE *Rez;
    int ord[LN], penalty = 0, timer = 0;
    Rez = fopen(output, "w");
    temp = plan;
    if (Rez == NULL)
    {
        printf("Could not create file %s.\n", output);
        return 1;
    }
    fprintf(Rez, "Darbas              Trukme, Terminas, Bauda, Laikas\n\n");
    for(i = 0; i < n; i++)
    {
        for(j = 1; j < order[i]; j++) plan = plan->next;
        timer = timer + plan->time;
        fprintf(Rez, "%s %d %d %d %d\n", plan->work, plan->time, plan->finish, plan->penalty, timer);
        plan->availability = 0;
        plan = temp;
    }
    for(i = 0; i < n2; i++)
    {
        if (plan->availability == 1)
        {
            timer = timer + plan->time;
            fprintf(Rez, "%s %d %d %d %d\n", plan->work, plan->time, plan->finish, plan->penalty, timer);
            penalty = penalty + plan->penalty;
        }
        plan = plan->next;
    }
    for(i = 0; i < n1; i++)
    {
        if (plan1->availability == 2)
        {
            penalty = penalty + plan1->penalty;
            timer = timer + plan1->time;
            fprintf(Rez, "%s %d %d %d %d\n", plan1->work, plan1->time, plan1->finish, plan1->penalty, timer);
        }
        plan1 = plan1->prev;
    }

    fprintf(Rez, "\nBauda : %d, Code Hardness : %f", penalty, CodeStr);
    fclose(Rez);
}
short Delete(PLAN **plan)
{
    PLAN *temp;
    if ((*plan) == NULL) return 1;
    if ((*plan)->next != NULL)
    {
        while ((*plan)->next != NULL)
        {
            temp = (*plan);
            (*plan) = (*plan)->next;
            free(temp);
        }
    }
    else
    {
        while ((*plan)->prev != NULL)
        {
            temp = (*plan);
            (*plan) = (*plan)->prev;
            free(temp);
        }
    }
    free(*plan);
    (*plan) = NULL;
}
