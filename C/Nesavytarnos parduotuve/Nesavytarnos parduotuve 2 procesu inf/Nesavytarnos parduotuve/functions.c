#include  <stdio.h>
#include  <stdlib.h>
#include "functions.h"

// prideda elementa i eiles gala
int Enqueue(kint x, q *ptr) {
	queue *temp = (queue*)malloc(sizeof(queue));
	if (temp == NULL) {
		return(1); //neiseina sukurti naujo temp
	}
	temp->data = x;
	temp->next = NULL;
	if ((*ptr)->head == NULL) {
		(*ptr)->head = temp;
		(*ptr)->tail = temp;
		return(0);
	}
	else {
		((*ptr)->tail)->next = temp;
		(*ptr)->tail = temp;
	}
	return(0);
}

// istrina elementa is eiles priekio
int Dequeue(kint* x, q *ptr) {
	queue* temp = (*ptr)->head;
	if ((*ptr)->head == NULL) {
		return(1);
	}
	*x = ((*ptr)->head)->data;
	temp = temp->next;
	free((*ptr)->head);
	(*ptr)->head = temp;
	return(0);
}

// tikrina ar eile pilna/tuscia
int Check(q *ptr) {
	if ((*ptr)->head == NULL){ //eile tuscia
		return(1);
	}
	else {
		queue* temp = (queue *)malloc(sizeof(queue));
		if (temp == NULL)  //eile pilna
			return(2);
	}
	return(0); // nei tuscia, nei pilna
}

// gauna pirmo elemento duomenis
int Get_First(kint *x, q *ptr) {
	if ((*ptr)->head == NULL) {
		return(1); //klaida, eile tuscia
	}
	else {
		*x = ((*ptr)->head)->data;
		return(0); //eile nera tuscia
	}
}
// sukuria (nunulina) tuscia eile
int Create_Empty(q *ptr) {
	(*ptr) = (queue *)malloc(sizeof(queue));
	(*ptr)->head = NULL;
	(*ptr)->tail = NULL;
	return(0);
}

//suskaiciuoja kiek elementu yra eileje
int Get_Count(kint *x, q *ptr) {
	queue *temp;
	*x = 0;
	temp = (*ptr)->head;
	while (temp != NULL) {
		(*x)++;
		temp = temp->next;
	}
	return(0);
}

//istrine eile
int Delete_Queue(q *ptr) {
	queue* temp = (*ptr)->head;
	if ((*ptr)->head == NULL) //eile tuscia
		return(1);
	else {
		queue *temp = (*ptr)->head;
		queue *temp2;
		while (temp != NULL) {
			temp2 = temp;
			temp = temp->next;
			free(temp2);
		}
	}
	free((*ptr));
	ptr = NULL;
	return(0);
}