/**
1 Užduotis, 2 variantas. Eilė.
Gediminas Petrikas PS6
*/

#include "functions.h"

// PAGR PROGRAMA
int main() {
	q queue1;
	kint x;
	int i = 1;
	int *t = 0;
	printf("  1. CREATE AN EMPTY QUEUE\n");
	printf("  2. ENQUEUE\n");
	printf("  3. DEQUEUE\n");
	printf("  4. CHECK\n");
	printf("  5. GET FIRST\n");
	printf("  6. COUNT HOW MANY ELEMENTS\n");
	printf("  7. DELETE QUEUE\n");
	printf("  0. END\n\n");
	while (i != 0){
		scanf("%d", &i);
		if (i == 1) {
			t = Create_Empty(&queue1);
			if (t == 0) printf("An empty Queue was created\n");
		}
		else if (i == 2) {
			printf("Enter an integer: ");
			scanf("%d", &x);
			printf("\n");
			t = Enqueue(x, &queue1);
			if (t == 1)
				printf("Cannot create Queue, no memory available\n");
			else if (t == 2) printf("Cannot add an element; You must create an empty Queue\n");
		}
		else if (i == 3) {
			t = Dequeue(&x, &queue1);
			if (t == 0)
				printf("Dequeued element with value: %d\n", x);
			else printf("Cannot Dequeue, Queue is empty\n");
		}
		else if (i == 4) {
			t = Check(&queue1);
			if (t == 0)
				printf("Queue neither empty, nor full\n");
			else if (t == 1)
				printf("Queue is empty\n");
			else if (t == 2)
				printf("Queue is full\n");
		}
		else if (i == 5) {
			t = Get_First(&x, &queue1);
			if (t == 0) printf("The first element is: %d\n", x);
			else printf("Queue is empty\n");
		}
		else if (i == 6) {
			Get_Count(&x, &queue1);
			printf("Number of elements: %d\n", x);
		}
		else if (i == 7) {
			t = Delete_Queue(&queue1);
			if (t == 1)
				printf("Queue is empty\n");
			else printf("Queue is deleted\n");
		}
		else if (i != 0) printf("Bad input.\n");

	}
	return 0;
}
