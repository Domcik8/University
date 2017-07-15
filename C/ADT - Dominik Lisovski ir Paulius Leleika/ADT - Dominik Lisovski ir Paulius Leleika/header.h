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
short IsGreater(numb *tail1, numb* tail2);
short Sub(numb **head1, numb *tail1, numb *tail2);
short Mod(numb *tail1, numb *tail2, numb **head1);
short Div(numb *tail1, numb *tail2, numb **head1);
void DeleteNulls(numb **answ);
void Twister(numb **mult);
void TwisterMul(numb **mult);
short AddNumbToStart(numb **tail, short i);
short AddNumbToEnd(numb **tail, short i);
void DelAll(numb *tail);
void FreeOne(numb *head);
int Add(numb **sum, numb *tail1, numb *tail2);
short Multiplication(numb **total, numb *tail1, numb *tail2);
short SignArithmetics(numb **answ, numb *tail1, numb *tail2, short sign1, short sign2, char action, short *total_sign);
void GetHead(numb **head, numb *tail);
short GetSign(short *sign, char num[]);
short GetNumber(numb **tail, char *num);
short IsGreater(numb *tail1, numb* tail2);
short Sub(numb **head1, numb *tail1, numb *tail2);
short Mod(numb *tail1, numb *tail2, numb **head1);
void AddSignToAnsw(numb *answ, short total_sign);
short LongNumber(char *num1, char *num2, short action, numb **answ);
void PrintAll(numb * curr);
