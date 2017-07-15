#ifndef FUNCTIONS_H
#define FUNCTIONS_H

typedef int kint;

typedef struct structure {
	kint data;
	struct structure *next;
} queue;

typedef struct q {
	queue *head;
	queue *tail;
} *q;

int Enqueue(int, q*);
int Dequeue(int*, q*);
int Check(q*);
int Get_First(int*, q*);
int Create_Empty(q*);
int Get_Count(int*, q*);
int Delete_Queue(q*);

#endif