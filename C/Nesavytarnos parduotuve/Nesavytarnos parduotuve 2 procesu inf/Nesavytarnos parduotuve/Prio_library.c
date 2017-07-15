# include "Prio_el.h"

int idetiElementa(value elementas, int prio, eile **front1)
{
	eile *tmp, *q, *front;

	tmp = (eile *)malloc(sizeof(eile));
	tmp->val = elementas;
	tmp->prioritetas = prio;
	front = *front1;
	if (front == NULL || prio < front->prioritetas)
	{
		tmp->next = front;
		front = tmp;
	}
	else
	{
		q = front;
		while (q->next != NULL && q->next->prioritetas <= prio) q = q->next;
		tmp->next = q->next;
		q->next = tmp;
	}
	*front1 = front;
	return 0;
}
//--------------------------------

int sujungti(eile **front1, eile *front2)
{
	eile *front;
	if (*front1 == NULL || front2 == NULL)
	{
		return 1;
	}
	front = front2;
	while (front != NULL)
	{
		idetiElementa(front->val, front->prioritetas, front1);
		front = front->next;
	}
	trintiSarasa(&front2);
	return 0;

}
//-----------------------------------------------------

void sukurtiSarasa(eile ** front1)
{
	*front1 = NULL;
}
//-----------------------------------------
int naikintiElementa(eile **front1)
{
	eile *tmp, *front;
	if (*front1 == NULL)
	{
		return 1;
	}
	front = *front1;
	tmp = front;
	front = front->next;
	free(tmp);
	*front1 = front;
	return 0;
}
//---------------------------------

int trintiSarasa(eile **front1)
{
	eile *tmp, *front;
	if (*front1 == NULL)
	{
		return 1;
	}
	front = *front1;
	while (front != NULL)
	{
		tmp = front;
		front = front->next;
		free(tmp);
	}
	*front1 = front;
	return 0;
}

int gautiMax(value *elementasMax, int *prio, eile *front1)
{
	if (front1 == NULL)
	{
		return 1;
	}
	*prio = front1->prioritetas;
	*elementasMax = front1->val;
	return(0);
}
//------------------------------------

int rodyti(eile *front1)
{
	if (front1 == NULL)
	{
		return 1;
	}
	return 0;
}
//-------------------------------------------------------