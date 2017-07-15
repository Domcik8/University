///Long digit operations by Dominik Lisovski and Paulius Leleika
#ifndef longdigit_h
#define longdigit_h
#include <stdio.h>
#include <stdlib.h>
#define size 255

typedef struct number numb;
struct number
{
	short val;
	numb *next;
	numb *prev;
};

short Addition(numb *pirmas, numb *antras, numb **answ);
short Subtraction(numb *pirmas, numb *antras, numb **answ);     //Changes "pirmas"
short Multiplication(numb *pirmas, numb *antras, numb **answ);
short Division(numb *pirmas, numb *antras, numb **answ);        //Changes "pirmas"
short Modulus(numb *pirmas, numb *antras, numb **answ);         //Changes "pirmas"
short DelAll(numb **tail);
short CreateNumber(numb **tail, char *num);
short Factorial(numb **answ, numb *skaicius);
short PrintAll(numb *curr);
short IsGreater(numb *tail1, numb* tail2);                      //Unsigned! 0 - tail1 < tail2 , 1 - tail1 >= tail2

																/**
																ERROR_1 - Neuztenka atminties
																ERROR_2 - Klaidingas skaicius
																ERROR_3 - Tuscias skaicius
																ERROR_4 - Dalyba is nulio, negalima!
																ERROR_5 - Buvo bandoma isvalyti tuscia sarasa
																*/
#endif