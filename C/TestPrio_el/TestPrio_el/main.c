# include "Prio_el.h"

int main()
{
	int choice = 0;
	int prio, elementas;
	int prioMax, elementasMax;

	eile *front1, *front2, *ptr;
	printf("\n\n Prioritetu eile \n\n");
	sukurtiSarasa(&front1);
	sukurtiSarasa(&front2);
	while (1)
	{
		printf("\n\n--------------------------- MENIU -------------------------------------\n\n");
		printf("1.Sukurti sarasa.\n\n");
		printf("2.Ideti elementa.\n\n");
		printf("3.Gauti didziausia elementa.\n\n");
		printf("4.Panaikinti elementa.\n\n");
		printf("5.Parodyti.\n\n");
		printf("6.Trinti sarasa.\n\n");
		printf("7.Sujungti du sarasus.\n\n");
		printf("8.Iseiti.\n\n");
		printf("Komandos pasirinkimas : ");
		scanf_s("%d", &choice);
		switch (choice)
		{
		case 1:
			system("cls");
			sukurtiSarasa(&front1);
			break;
		case 2:
			system("cls");
			printf("\n------------------------------------------------------------------------------\n");
			printf("\n Elemento reiksme : ");
			scanf_s("%d", &elementas);
			getchar();
			printf("\n\n Elemento prioritetas(sveikas skaicius) : ");
			scanf_s("%d", &prio);
			getchar();
			printf("\n\n------------------------------------------------------------------------------\n");

			idetiElementa(elementas, prio, &front1);
			system("cls");
			break;
		case 3:
			system("cls");
			if (gautiMax(&elementasMax, &prioMax, front1) == 1){
				printf("Prioritetu eile tuscia");
			}
			else
			{
				printf("prioritetas ir El.reiksme \n\n");
				printf("%d %d", prioMax, elementasMax);
			}
			break;
		case 4:
			system("cls");
			if (naikintiElementa(&front1) == 1) printf("Prioritetu eile tuscia");;
			break;
		case 5:
			system("cls");
			if (rodyti(front1) == 1)
			{
				printf("Prioritetu eile tuscia");
			}
			else
			{
				ptr = front1;
				printf("\n\n---------------------- Eile --------------------------------\n\n");
				printf("prioritetas ir El.reiksme \n\n");
				while (ptr != NULL)
				{
					printf("%d  ", ptr->prioritetas);
					printf("%d  ", ptr->val);
					printf("\n");
					ptr = ptr->next;
				}
			}
			system("break");
			break;
		case 6:
			system("cls");
			trintiSarasa(&front1);
			break;
		case 7:
			system("cls");
			sukurtiSarasa(&front2);
			idetiElementa(5, 1, &front2);
			idetiElementa(5, 101, &front2);
			idetiElementa(5, 51, &front2);


			sujungti(&front1, front2);
			break;
		case 8:
			return(0);
		default:
			system("cls");
			printf("Neteisingas pasirinkimas\n");
		}
	}
}
//-------------------------------------------------------
