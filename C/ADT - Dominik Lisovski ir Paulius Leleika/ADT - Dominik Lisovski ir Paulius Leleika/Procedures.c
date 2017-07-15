///Long digit operations by Dominik Lisovski and Paulius Leleika
#include "longdigit.h"

void DeleteNulls(numb **answ);
void Twister(numb **mult);
void TwisterMul(numb **mult);
short AddNumbToStart(numb **tail, short i);
short AddNumbToEnd(numb **tail, short i);
void FreeOne(numb *head);
short SignArithmetics(numb **answ, numb *tail1, numb *tail2, short sign1, short sign2, char action, short *total_sign);
void GetHead(numb **head, numb *tail);
short GetSign(short *sign, numb *tail);
void AddSignToAnsw(numb *answ, short total_sign);
short LongNumber(numb *num1, numb *num2, char action, numb **answ);
short Sub(numb **head1, numb *tail1, numb *tail2);
short Mod(numb *tail1, numb *tail2, numb **head1);
short Div(numb *tail1, numb *tail2, numb **head1);
short Add(numb **sum, numb *tail1, numb *tail2);
short Mul(numb **total, numb *tail1, numb *tail2);
short PrevToNext(numb **totalholder);

short Addition(numb *pirmas, numb *antras, numb **answ)
{
	int err;
	if ((err = LongNumber(pirmas, antras, '+', answ))>0) return err;
	return 0;
}
short Subtraction(numb *pirmas, numb *antras, numb **answ)
{
	int err;
	if ((err = LongNumber(pirmas, antras, '-', answ))>0) return err;
	return 0;
}
short Multiplication(numb *pirmas, numb *antras, numb **answ)
{
	int err;
	if ((err = LongNumber(pirmas, antras, '*', answ))>0) return err;
	return 0;
}
short Division(numb *pirmas, numb *antras, numb **answ)
{
	int err;
	if ((err = LongNumber(pirmas, antras, '/', answ))>0) return err;
	return 0;
}
short Modulus(numb *pirmas, numb *antras, numb **answ)
{
	int err;
	if ((err = LongNumber(pirmas, antras, '%', answ))>0) return err;
	return 0;
}

void DeleteNulls(numb **answ)
{
	numb *temp = NULL;
	if (*answ)
		while (((*answ)->prev) && (((*answ)->val) == 0))
		{
			if (((*answ)->val) == 0)
			{
				temp = (*answ);
				(*answ) = (*answ)->prev;
				free(temp);
				(*answ)->next = NULL;
			}
		}
}
void Twister(numb **mult)
{
	numb *next, *prev;
	while ((*mult)->prev)
	{
		next = (*mult)->next;
		prev = (*mult)->prev;
		(*mult)->next = prev;
		(*mult)->prev = next;
		(*mult) = (*mult)->next;
	}
	next = (*mult)->next;
	prev = (*mult)->prev;
	(*mult)->next = prev;
	(*mult)->prev = next;
}
void TwisterMul(numb **mult)
{
	numb *next, *prev;
	while ((*mult)->next)
	{
		next = (*mult)->next;
		prev = (*mult)->prev;
		(*mult)->next = prev;
		(*mult)->prev = next;
		(*mult) = (*mult)->prev;
	}
	next = (*mult)->next;
	prev = (*mult)->prev;
	(*mult)->next = prev;
	(*mult)->prev = next;
}
short AddNumbToStart(numb **tail, short i)
{
	numb *curr;
	curr = (numb *)malloc(sizeof(numb));
	if (curr == NULL) return 1;
	curr->val = i;
	if ((*tail) != NULL) (*tail)->prev = curr;
	curr->next = *tail;
	curr->prev = NULL;
	*tail = curr;
	return 0;
}
short AddNumbToEnd(numb **tail, short i)
{
	numb *curr;
	curr = (numb *)malloc(sizeof(numb));
	if (curr == NULL) return 1;
	curr->val = i;
	if ((*tail) != NULL) (*tail)->next = curr;
	curr->prev = *tail;
	curr->next = NULL;
	*tail = curr;
	return 0;
}
short DelAll(numb **tail)
{
	numb *temp;
	if (*tail == NULL) return 5;
	if ((*tail)->next != NULL)
		while (*tail)
		{
			temp = (*tail)->next;
			free(*tail);
			(*tail) = temp;
		}
	if (*tail != NULL)
		if ((*tail)->prev != NULL)
			while (*tail)
			{
				temp = (*tail)->prev;
				free(*tail);
				(*tail) = temp;
			}
	(*tail) = NULL;

	return 0;
}
void FreeOne(numb *head)
{
	head->prev->next = NULL;
	free(head);
}
short Add(numb **sum, numb *tail1, numb *tail2)
{
	short over = 0, addition, err;
	while ((tail1 != NULL) && (tail2 != NULL))
	{
		addition = tail1->val + tail2->val + over;
		over = addition / 10;
		if ((err = AddNumbToEnd(sum, addition % 10)>0)) return err;
		tail1 = tail1->next;
		tail2 = tail2->next;
	}
	while (tail1 != NULL)      //Jei pirmas didesnis
	{
		addition = tail1->val + over;
		over = addition / 10;
		if ((err = AddNumbToEnd(sum, addition % 10)>0)) return err;
		tail1 = tail1->next;
	}
	while (tail2 != NULL)      //Jei antras didesnis
	{
		addition = tail2->val + over;
		over = addition / 10;
		if ((err = AddNumbToEnd(sum, addition % 10)>0)) return err;
		tail2 = tail2->next;
	}
	if ((tail1 == NULL) && (over > 0) && (tail2 == NULL))
		if ((err = AddNumbToEnd(sum, over)>0)) return err;
	Twister(sum);
	return 0;
}

short Mul(numb **total, numb *tail1, numb *tail2)
{
	numb *mult = NULL, *tail2holder, *totalholder = NULL;
	short over = 0, mul, err = 0;
	long cikliukas = 0, i;
	tail2holder = tail2;
	while (tail1 != NULL)
	{
		over = 0;
		tail2 = tail2holder;
		while (tail2 != NULL)
		{
			mul = tail1->val * tail2->val + over;
			over = mul / 10;
			if ((err = AddNumbToStart(&mult, mul % 10)>0)) return err;
			tail2 = tail2->next;
		}
		if ((tail2 == NULL) && (over > 0))
			if ((err = AddNumbToStart(&mult, over)>0)) return err;
		TwisterMul(&mult);
		if (cikliukas > 0)
		{
			err = DelAll(&totalholder);
		}
		totalholder = (*total);
		(*total) = NULL;
		if ((err = PrevToNext(&totalholder))>0) return err;
		for (i = 0; i<cikliukas; i++)
			if ((err = AddNumbToStart(&mult, 0)>0)) return err;
		if ((err = Add(total, mult, totalholder)>0)) return err;
		err = DelAll(&mult);
		mult = NULL;
		tail1 = tail1->next;
		cikliukas++;
	}
	return 0;
}
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
			if ((err = Mul(answ, tail2, tail1)) >0) return err;
			Twister(answ);
			DeleteNulls(answ);
		}
		else
		{
			if ((err = Mul(answ, tail1, tail2)) >0) return err;
			Twister(answ);
			DeleteNulls(answ);
		}
		break;
	case '/':
		*total_sign = sign1*sign2;
		if (!IsGreater(tail1, tail2)) CreateNumber(answ, "0");
		else if ((err = Div(tail1, tail2, answ))>0) return err;
		break;
	case '%':
		GetHead(answ, tail1);
		*total_sign = sign1*sign2;
		if (!IsGreater(tail1, tail2));
		else if ((err = Mod(tail1, tail2, answ))>0) return err;
		break;
	}
	return 0;
}
void GetHead(numb **head, numb *tail)
{
	while (tail)
	{
		*head = tail;
		tail = tail->next;
	}
}
short GetSign(short *sign, numb *tail)
{
	numb *head = NULL;
	GetHead(&head, tail);
	if (head->val<0)
	{
		*sign = -1;
		head->val *= -1;
	}
	return 0;
}
short CreateNumber(numb **tail, char *num)
{
	char val;
	int i = 0;
	short err;
	short neg = 0;
	if ((*tail) != NULL) DelAll(tail);
	if (num[i] == '-')
	{
		i++;
		neg = 1;
	}
	else if (num[i] == '\0') return 3;
	while (num[i] == '0') i++;
	if (num[i] == '\0')
	{
		err = AddNumbToStart(tail, 0);
		return err;
	}
	while ((val = num[i++]) != '\0')
	{
		if (val >= '0'&&val <= '9')
		{
			val -= '0';
			if (neg)
			{
				val *= -1;
				neg = 0;
			}
			if ((err = AddNumbToStart(tail, val)>0)) return err;
		}
		else return 2; //klaidingas skaicius
	}
	return 0;
}
short IsGreater(numb *tail1, numb* tail2)
{
	if (tail1&&tail2)
	{
		while (tail1->next&&tail2->next)
		{
			tail1 = tail1->next;
			tail2 = tail2->next;
		}
		if (tail1->next != NULL&&tail2->next == NULL) return 1;
		if (tail1->next == NULL&&tail2->next != NULL) return 0;
		if (tail1->next == NULL&&tail2->next == NULL)
		{
			while ((tail1->val == tail2->val) && (tail1->prev != NULL))
			{
				tail1 = tail1->prev;
				tail2 = tail2->prev;
			}
			if (tail1->val >= tail2->val) return 1;
			else
				return 0;
		}
	}
	return 0;
}
short Sub(numb **head1, numb *tail1, numb *tail2)
{
	short diff, aux = 0, val;
	numb *temp;
	while (tail2 != NULL || tail1 != NULL || aux)
	{
		if (tail1&&tail2)
		{
			tail1->val -= tail2->val;
		}
		else if (tail2 == NULL&&tail1 != NULL)val = tail1->val;
		tail1->val += aux;
		diff = 0, aux = 0;
		if (tail1->val<0)
		{
			diff = 10;
			aux = -1;
		}
		tail1->val += diff;
		if (tail1 != NULL) tail1 = tail1->next;
		if (tail2 != NULL) tail2 = tail2->next;
		while ((*head1)->val == 0 && ((*head1)->prev != NULL))
		{
			temp = *head1;
			*head1 = (*head1)->prev;
			FreeOne(temp);
		}
	}
	return 0;
}
void FreeOneLastDiv(numb **tail)
{
	if ((*tail)->val == 0)
	{
		numb *temp;
		temp = *tail;
		(*tail)->next->prev = NULL;
		*tail = (*tail)->next;
		free(temp);
	}

}

void CrtTmpList(numb **tail, int ilgis)
{
	int i;
	short temp = 0;
	for (i = 0; i<ilgis; i++)
	{
		AddNumbToStart(tail, temp);
	}
}
short Div(numb *tail1, numb *tail2, numb **head1)
{
	short err, temp = 0, temp1 = 1;
	int ilgis = 0;
	numb *div = NULL;
	numb *tmplist1 = NULL, *tmplist2 = NULL;
	AddNumbToStart(&div, temp);
	AddNumbToStart(&tmplist1, temp1);
	if (tail2->val == 0 && tail2->next == NULL) return 4;
	GetHead(head1, tail1);
	if (IsGreater(tail1, tail2))
	{
		while (IsGreater(tail1, tail2))
		{
			AddNumbToStart(&tail2, temp);
			ilgis++;
		}
		FreeOneLastDiv(&tail2);
		ilgis--;
		CrtTmpList(&tmplist1, ilgis);
		while (ilgis >= 0)
		{
			while (IsGreater(tail1, tail2))
			{
				if ((err = Sub(head1, tail1, tail2))>0) return err;
				Add(&tmplist2, tmplist1, div);
				DelAll(&div);
				div = tmplist2;
				tmplist2 = NULL;
				if ((err = PrevToNext(&div))>0) return err;
			}
			while (!IsGreater(tail1, tail2) && ilgis >= 0)
			{
				FreeOneLastDiv(&tail2);
				FreeOneLastDiv(&tmplist1);
				ilgis--;
			}
		}
		*head1 = div;
		GetHead(head1, div);
	}
	else
	{
		*head1 = tail1;
		GetHead(head1, tail1);
	}
	return 0;
}

short Mod(numb *tail1, numb *tail2, numb **head1)
{
	short err, temp = 0;
	int ilgis = 0;
	numb *tmplist1 = NULL;
	AddNumbToStart(&tmplist1, 1);
	if (tail2->val == 0 && tail2->next == NULL) return 4;
	GetHead(head1, tail1);
	if (IsGreater(tail1, tail2))
	{
		while (IsGreater(tail1, tail2))
		{
			AddNumbToStart(&tail2, temp);
			ilgis++;
		}
		FreeOneLastDiv(&tail2);
		ilgis--;
		CrtTmpList(&tmplist1, ilgis);
		while (ilgis >= 0)
		{
			while (IsGreater(tail1, tail2))
			{
				if ((err = Sub(head1, tail1, tail2))>0) return err;
			}
			while (!IsGreater(tail1, tail2) && ilgis >= 0)
			{
				FreeOneLastDiv(&tail2);
				FreeOneLastDiv(&tmplist1);
				ilgis--;
			}
		}
	}
	GetHead(head1, tail1);
	return 0;
}
void AddSignToAnsw(numb *answ, short total_sign)
{
	answ->val *= total_sign;
}

short PrevToNext(numb **totalholder)
{
	short val = 0, err = 0;
	numb *temp = NULL;
	if (*totalholder)
	{
		while ((*totalholder)->prev)
			(*totalholder) = (*totalholder)->prev;
		while (*totalholder)
		{
			val = (*totalholder)->val;
			if ((err = AddNumbToStart(&temp, val)>0)) return err;
			(*totalholder) = (*totalholder)->next;
		}
		err = DelAll(totalholder);
		(*totalholder) = temp;
		temp = NULL;
	}
	return 0;
}

short LongNumber(numb *tail1, numb *tail2, char action, numb **answ)
{
	numb *curr;
	short sign1 = 1, sign2 = 1, err, total_sign;
	numb *head1 = NULL;
	if ((tail1 == NULL) || (tail2 == NULL)) return 3;
	GetSign(&sign1, tail1);
	GetSign(&sign2, tail2);
	if (*answ == NULL)
	{
		if ((err = SignArithmetics(answ, tail1, tail2, sign1, sign2, action, &total_sign))>0) return err;
	}
	else
	{
		curr = (numb *)malloc(sizeof(numb));
		curr = NULL;
		if ((err = SignArithmetics(&curr, tail1, tail2, sign1, sign2, action, &total_sign))>0) return err;
		*answ = curr;
	}
	AddSignToAnsw(*answ, total_sign);
	if ((err = PrevToNext(answ))>0) return err;
	TwisterMul(answ);
	return 0;
}

short PrintAll(numb * curr)
{
	if (curr == NULL) return 3;
	printf("Jusu atsakymas: ");
	PrevToNext(&curr);
	while (curr->next)
	{
		printf("%d", curr->val);
		curr = curr->next;
	}
	printf("%d", curr->val);
	printf("\n");
	return 0;
}

short Factorial(numb **answ, numb *skaicius)
{
	numb *vienas = NULL;
	short err;

	err = CreateNumber(&vienas, "1");
	err = CreateNumber(answ, "1");

	while (IsGreater(skaicius, vienas))
	{
		err = Multiplication(*answ, skaicius, answ);
		err = Subtraction(skaicius, vienas, &skaicius);
	}
	return err;
}