/*#include "header.h"
short SignArithmetics(numb **answ, numb *tail1, numb *tail2, short sign1, short sign2, char action, short *total_sign)
{
	short err;
	switch (action)
	{
	case '+':
		if (sign1 == -1 && sign2 == 1)
		{

			if (IsGreater(tail1, tail2))
			{
				GetHead(answ, tail1);
				*total_sign = -1;
				if ((err = Sub(answ, tail1, tail2))>0) return err;
			}
			else
			{
				GetHead(answ, tail2);
				*total_sign = 1;
				if ((err = Sub(answ, tail2, tail1))>0) return err;
			}
		}
		else if (sign1 == 1 && sign2 == 1)
		{
			*total_sign = 1;
			if ((err = Add(answ, tail1, tail2)>0)) return err;
			Twister(answ);
		}
		else if (sign1 == -1 && sign2 == -1)
		{
			*total_sign = -1;
			if ((err = Add(answ, tail1, tail2)>0)) return err;
			Twister(answ);
		}
		else if (sign1 == 1 && sign2 == -1)
		{
			if (IsGreater(tail1, tail2))
			{
				GetHead(answ, tail1);
				*total_sign = 1;
				if ((err = Sub(answ, tail1, tail2))>0) return err;
			}
			else
			{
				GetHead(answ, tail2);
				*total_sign = -1;
				if ((err = Sub(answ, tail2, tail1))>0) return err;
			}
		}
		break;
	case '-':
		if (sign1 == -1 && sign2 == -1)
		{
			if (IsGreater(tail1, tail2))
			{
				GetHead(answ, tail1);
				*total_sign = -1;
				if ((err = Sub(answ, tail1, tail2))>0) return err;
			}
			else
			{
				GetHead(answ, tail2);
				*total_sign = 1;
				if ((err = Sub(answ, tail2, tail1))>0) return err;
			}
		}
		else if (sign1 == -1 && sign2 == 1)
		{
			*total_sign = -1;
			if ((err = Add(answ, tail1, tail2)>0)) return err;
			Twister(answ);
		}
		else if (sign1 == 1 && sign2 == -1)
		{
			*total_sign = 1;
			if ((err = Add(answ, tail1, tail2)>0)) return err;
			Twister(answ);
		}
		else if (sign1 == 1 && sign2 == 1)
		{
			if (IsGreater(tail1, tail2))
			{
				GetHead(answ, tail1);
				*total_sign = 1;
				if ((err = Sub(answ, tail1, tail2))>0) return err;
			}
			else
			{
				GetHead(answ, tail2);
				*total_sign = -1;
				if ((err = Sub(answ, tail2, tail1))>0) return err;
			}
		}
		break;
	case '*':
		*total_sign = sign1*sign2;
		if (IsGreater(tail1, tail2))
		{
			if ((err = Multiplication(answ, tail2, tail1)) >0) return err;
			Twister(answ);
			DeleteNulls(answ);
		}
		else
		{
			if ((err = Multiplication(answ, tail1, tail2)) >0) return err;
			Twister(answ);
			DeleteNulls(answ);
		}
		break;
	case '/':
		GetHead(answ, tail1);
		*total_sign = sign1*sign2;
		if (!IsGreater(tail1, tail2)) *answ = tail1;
		else Div(tail1, tail2, answ);
		break;
	case '%':
		GetHead(answ, tail1);
		*total_sign = sign1*sign2;
		if (!IsGreater(tail1, tail2));
		else Mod(tail1, tail2, answ);
		break;
	default: return 4;
	}
	return 0;
}
short LongNumber(char *num1, char *num2, short action, numb **answ)
{
	numb *tail1 = NULL, *tail2 = NULL;
	short sign1 = 1, sign2 = 1, err, total_sign;

	GetSign(&sign1, num1);
	GetSign(&sign2, num2);
	if ((err = GetNumber(&tail1, num1))>0) return err;
	if ((err = GetNumber(&tail2, num2))>0) return err;
	SignArithmetics(answ, tail1, tail2, sign1, sign2, action, &total_sign);
	DelAll(tail1);
	DelAll(tail2);
	AddSignToAnsw(*answ, total_sign);
	return 0;
}
void PrintAll(numb * curr)
{
	printf("\nJusu atsakymas: ");
	while (curr->prev)
	{
		printf("%d", curr->val);
		curr = curr->prev;
	}
	printf("%d", curr->val);
}
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
	}
}
int main()
{
	int i = 0, val, n;
	int error;
	numb *temp;
	char num1[size] = { '\0' }, num2[size] = { '\0' }, action;
	numb *answ = NULL, *pirm, *antr;

	while (n)
	{
		printf("\nPasirinkite ka noretumete daryti:\n");
		printf("  1. Ivesti pirmaji sveikaji skaiciu\n");
		printf("  2. Ivesti antraji sveikaji skaiciu\n");
		printf("  3. Aritmetinies operacijos pasirinkimas\n");
		printf("  0. Baigti programos darba.\n");
		printf("\n\nPasirinkite veiksma: ");
		scanf("%d", &n);
		system("cls");
		switch (n)
		{
		case 1:
			printf("Iveskite pirmaji sveikaji skaiciu: ");
			getchar();
			i = 0;                                          //<------------------------------------Jei 2 kartus is eiles noresi ivesti 1 sk
			while ((val = getchar()) != '\n')
			{
				num1[i] = val;
				i++;
			}
			num1[i] = '\n';
			number a = setnumber(num1);                                 //<------------------------------------Kad nustatytum pabaiga, jei uzrasytas vienas ant kito, arba galima vel nunulinti, bet daugiau kainuoja
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
			num2[i] = '\n';                                   //
			number b = setnumber(num2);                                 //
			<------------------------------------Kad nustatytum pabaiga, jei uzrasytas vienas ant kito, arba galima vel nunulinti, bet daugiau kainuoja
				break;
		case 3:
			getchar();
			printf("Pasirinkite norima veiksma + ,- , *, /, % \n");
			scanf("%c", &action);
			if (action == '+') Add(num1, num2, num3);

			if ((error = LongNumber(num1, num2, action, &answ)) == 0)
			{
				PrintAll(answ);
				DelAll(answ);
				answ = NULL;
				Add(answer, num1, number2);
			}
			else PrintError(error);
			break;

		case 0:
			break;
		default:
			printf("\nError! Negalimas veiksmas!\n\n");    //<------------------------------------Informacija
			break;
			system("cls");
		}
	}
	return 0;
}*/