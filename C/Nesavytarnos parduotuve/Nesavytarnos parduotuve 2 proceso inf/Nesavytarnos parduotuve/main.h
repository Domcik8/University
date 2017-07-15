#ifndef main_h
#define main_h
#define _CRT_SECURE_NO_WARNINGS

#include "functions.h"
#include "Prio_el.h"
#include <stdlib.h>
#include <stdio.h>
#include <time.h>
#include <math.h>

#define input "Duomenys.txt"
#define MAX 255
#define MAX_EINA 255

struct Aktivus
{
	int atiejo;		//Kada klientas atiejo
	int pr1;		//Kiek laiko vykdo pirma procesa
	int pr21;		//Kiek laiko vykdo antrojo proceso 1 dali
	int k1;			//Kiek laiko uzims kelias prie kasos
	int pr22;		//Kiek laiko vykdo antrojo proceso 2 dali
	int k2;			//Kiek laiko uzims kelias is kasos
	int pr23;		//Kiek laiko vykdo antrojo proceso 3 dali
	int lauke;		//Kiek laiko klientas laukie savo eiles
	int id;			//Kliento id;
	int kasa;		//Prie kurios pardavejos zmogus buvo
	int cekis;		//Ar zmogus jau buvo prie kasos
};
typedef struct Aktivus Aktivus;

int Nuskaityti(int *time, int *tikimybe, int *darbuotojai, int *kaina, int *cekis, int *pPaimimas, int *pPadavimas, int *prSurinkimas, int *prSurasimas, int *kelias, int *prGavimas);

#endif
