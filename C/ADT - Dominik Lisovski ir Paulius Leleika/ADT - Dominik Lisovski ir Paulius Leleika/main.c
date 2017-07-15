///Long digit operations by Dominik Lisovski and Paulius Leleika
#include "longdigit.h"
void PrintError(short err)
{
	switch (err)
	{
	case 1:
		printf("Neuztenka atminties\n");
		break;
	case 2:
		printf("Klaidingas skaicius\n");
		break;
	case 3:
		printf("Tuscias skaicius\n");
		break;
	case 4:
		printf("Dalyba is nulio, negalima!\n");
		break;
	case 5:
		printf("Buvo bandoma isvalyti tuscia sarasa\n");
		break;
	}
}
int main()
{
	int i = 0, val, n = 1;
	short error;
	char num1[size] = { 0 }, num2[size] = { 0 };
	numb *answ = NULL, *tail1 = NULL, *tail2 = NULL;
	while (n)
	{
		printf("\nPasirinkite ka noretumete daryti:\n");
		printf("  1. Ivesti pirmaji sveikaji skaiciu\n");
		printf("  2. Ivesti antraji sveikaji skaiciu\n");
		printf("  3. Sudetis\n");
		printf("  4. Atimtis\n");
		printf("  5. Daugyba\n");
		printf("  6. Dalyba div\n");
		printf("  7. Dalyba mod\n");
		printf("  8. Faktorialas\n");
		printf("  0. Baigti programos darba.\n");
		printf("\n\nPasirinkite veiksma: ");
		scanf_s("%d", &n);
		system("cls");
		switch (n)
		{
		case 1:
			printf("Iveskite pirmaji sveikaji skaiciu: ");
			getchar();
			i = 0;
			while ((val = getchar()) != '\n')
			{
				num1[i] = val;
				i++;
			}
			num1[i] = '\0';
			DelAll(&tail1);
			if ((error = CreateNumber(&tail1, num1))>0) PrintError(error);
			break;
		case 2:
			printf("Iveskite antraji sveikaji skaiciu: ");
			getchar();
			i = 0;
			while ((val = getchar()) != '\n')
			{
				num2[i] = val;
				i++;
			}
			num2[i] = '\0';
			DelAll(&tail2);
			if ((error = CreateNumber(&tail2, num2))>0) PrintError(error);
			break;
		case 3:
			if ((error = Addition(tail1, tail2, &answ)) == 0)
			{
				PrintAll(answ);
				DelAll(&answ);
			}
			else PrintError(error);
			break;
		case 4:
			if ((error = Subtraction(tail1, tail2, &answ)) == 0)
			{
				PrintAll(answ);
				DelAll(&answ);
			}
			else PrintError(error);
			break;
		case 5:
			if ((error = Multiplication(tail1, tail2, &answ)) == 0)
			{
				PrintAll(answ);
				DelAll(&answ);
			}
			else PrintError(error);
			break;
		case 6:
			if ((error = Division(tail1, tail2, &answ)) == 0)
			{
				PrintAll(answ);
				DelAll(&answ);
			}
			else PrintError(error);
			break;
		case 7:
			if ((error = Modulus(tail1, tail2, &answ)) == 0)
			{
				PrintAll(answ);
				DelAll(&answ);
			}
			else PrintError(error);
			break;
		case 8:

			Factorial(&answ, tail1);
			PrintAll(answ);
			DelAll(&answ);
			break;
		case 0:
			DelAll(&tail1);
			DelAll(&tail2);
			break;
		default:
			printf("\nError! Negalimas veiksmas!\n\n");
		}
	}
	return 0;
}
