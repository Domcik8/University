# ifndef Prio_el
# define Prio_el 
# include <stdio.h>
# include <stdlib.h>
# include <string.h>

typedef struct eile eile;
typedef int value;
struct eile
{
	int prioritetas;
	value val;
	struct eile *next;
};

int naikintiElementa(eile **front1);
int gautiMax(value *elementasMax, int *prio, eile *front1);
int rodyti(eile *front1);
int idetiElementa(value elementas, int prio, eile **front1);
int trintiSarasa(eile **front1);
void sukurtiSarasa(eile ** front1);
int sujungti(eile **front1, eile *front2);

/*

1 :sarasas yra tuscias

*/
#endif