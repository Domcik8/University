//Duota informacija, kokios žmonių poros yra pažįstamos. 
//Patikrinti, ar, pavyzdžiui, Jonas gali perduoti Marytei laiškelį per pažįstamus. 
//(grafo realizacija paremta kaimynystės sąrašais; naudoti paieškos į gylį metodą)
//Dominik Lisovski

#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#define MAX_M 50				//Masyvu didziai
#define MAX_LINE 20
#define input "Duomenys.txt"

struct grafas
{
	char vardas[MAX_LINE];
	struct grafas *draugas;
	int numeris;
}; 
struct stekas
{
	int data;
	struct stekas *next;
};

typedef struct grafas Grafas;
typedef struct stekas Stekas;

int Nuskaityti(Grafas **grafasm, int *MAX);
int Push(Stekas **stekas, int data);
int Pop(Stekas **stekas, int *data);

int main()
{
	char vardas[MAX_M][MAX_LINE];
	int i, duoda, ima, nagrinejamas, perduota = 0;
	int virsune[MAX_M] = { 0 };
	int MAX = 0;
	Grafas *grafas[MAX_M];
	Stekas (*stekas) = NULL;
	Nuskaityti(&grafas, &MAX);
	for (i = 0; i < MAX; i++)
		strcpy(vardas[i], grafas[i]->vardas);
	if (MAX == 0)
	{
		printf("Duomenu faile nebuvo nei vieno zmogaus\n");
		getchar();
		getchar();
		return 0;
	}
	printf("Si programa rodo ar zmogus gali perduodi laiskeli kitam zmogui per pazistamus\n\n");
	printf("Cia yra zmoniu sarasas :\n");
	for (i = 0; i < MAX; i++) printf("%d. %s \n", i, grafas[i]->vardas);
	printf("\nIveskite numeri zmogaus, kuris duoda laiskeli: ");
	scanf("%d", &duoda);
	printf("Iveskite numeri zmogaus, kuriam duodamas laiskelis: ");
	scanf("%d", &ima);
	if (duoda < 0 || duoda > (MAX - 1) || ima < 0 || ima > (MAX - 1))
	{
		printf("Blogas ivedimas");
		getchar();
		getchar();
		return 0;
	}
	if (duoda == ima)
	{
		printf("Laiskelis gali buti perduotas");
		getchar();
		getchar();
		return 0;
	}
	nagrinejamas = duoda;

	//DFS
	Push(&stekas, 0);
	Push(&stekas, duoda);
	virsune[duoda] = 1;
	while (stekas != NULL)
	{
		if (grafas[nagrinejamas]->draugas == NULL) Pop(&stekas, &nagrinejamas);
		else
		{
			grafas[nagrinejamas] = grafas[nagrinejamas]->draugas;			
			if (virsune[grafas[nagrinejamas]->numeris] == 0)
			{
				printf("Einu is %d-%s\n", nagrinejamas, vardas[nagrinejamas]);
				nagrinejamas = grafas[nagrinejamas]->numeris;
				printf("I %d-%s\n", nagrinejamas, vardas[nagrinejamas]);
				if (nagrinejamas == ima)
				{
					perduota = 1;
					break;
				}
				Push(&stekas, grafas[nagrinejamas]->numeris);
				virsune[grafas[nagrinejamas]->numeris] = 1;
			}
		}
	}

	if (perduota == 0)
	{
		printf("Perduoti laiskelio negalima");
	}
	else printf("Laiskelis gali buti perduotas");
	getchar();
	getchar();
	return 0;
}

int Nuskaityti(Grafas *grafas[], int *MAX)
{
	FILE *duomenys;
	Grafas *temp;
	Grafas *temp2;
	int temp3, numeris = 0;
	int sukurtas[MAX_M] = { 0 };
	char line[MAX_LINE];
	char vardas[MAX_LINE];
	int i, j, k; //Cia i nuskaitomo zmogaus numeris
	duomenys = fopen(input, "r");
	if (duomenys == NULL)
	{
		printf("Negalejo atidaryti %s\n", input);
		getchar();
		return 2;
	}
	fgets(line, MAX_LINE, duomenys);
	(*MAX) = strtol(line, NULL, 10);
	for (i = 0; i < (*MAX); i++)
	{
		grafas[i] = (Grafas*)malloc(sizeof(Grafas));
		fscanf(duomenys, "%s", grafas[i]->vardas);
		grafas[i]->numeris = i;
		grafas[i]->draugas = NULL;
	}
	fgets(line, MAX_LINE, duomenys); 
	fgets(line, MAX_LINE, duomenys);
	for (k = 0; k < (*MAX); k++)
	{
		fgets(line, MAX_LINE, duomenys);
		if (line[0] == '\n') break;
		i = line[0] - 48;
		temp = grafas[i];
		strcpy(vardas, grafas[i]->vardas);
		for (j = 3; j < (strlen(line)); j = j + 3)
		{
			while (grafas[i]->draugas != NULL) grafas[i] = grafas[i]->draugas;
			grafas[i]->draugas = (Grafas*)malloc(sizeof(Grafas));
			if (grafas[i]->draugas == NULL) return 1;
			grafas[i] = grafas[i]->draugas;
			grafas[i]->numeris = line[j] - 48;


			numeris = grafas[i]->numeris;
			temp2 = grafas[numeris];
			while (grafas[numeris]->draugas != NULL)
				grafas[numeris] = grafas[numeris]->draugas;
			grafas[numeris]->draugas = (Grafas*)malloc(sizeof(Grafas));
			grafas[numeris] = grafas[numeris]->draugas;
			grafas[numeris]->numeris = i;
			grafas[numeris]->draugas = NULL;
			strcpy(grafas[numeris]->vardas, vardas);
			grafas[numeris] = temp2;

			strcpy(grafas[i]->vardas, grafas[(line[j] - 48)]->vardas);
			grafas[i]->draugas = NULL;
		}
		grafas[i] = temp;
	}
	//fscanf(duomenys, "%d", &(*grafas)->rysys[i][j]);
	fclose(duomenys);
	return 0;
}
int Push(Stekas **stekas, int data)
{
	Stekas *temp;
	temp = (Stekas*)malloc(sizeof(Stekas));
	if (temp == NULL) return 1;
	temp->data = data;
	temp->next = (*stekas);
	(*stekas) = temp;
	return 0;
}
int Pop(Stekas **stekas, int *data)
{
	Stekas *temp;
	temp = (*stekas);
	if ((*stekas) == NULL) return 2;
	*data = (*stekas)->data;
	(*stekas) = (*stekas)->next;
	free(temp);
	return 0;
}