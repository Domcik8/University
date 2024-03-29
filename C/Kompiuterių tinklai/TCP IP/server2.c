/* Black Jack Game, made by Dominik Lisowski */
#include <stdio.h>
#include <unistd.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h> 
#include <sys/socket.h>
#include <netinet/in.h>
#include <time.h>

struct card
{
    char name;
    int value;
    char* type;
};
typedef struct card Deck;

int checkScore(Deck deck[], int* Cards, int quantity);
int chooseCard(int* hostCards, int *playerCards, int nHostCards, int nPlayerCards);
void sendScore(Deck deck[], int* hostCards, int* playerCards, int nHostCards, int nPlayerCards, int sock);
void createdeck(Deck deck[]);
void game(int); /* function prototype */
void error(const char *msg)
{
    perror(msg);
    exit(1);
}

int main(int argc, char *argv[])
{
     int sockfd, newsockfd, portno, pid;
     socklen_t clilen;
     struct sockaddr_in serv_addr, cli_addr;

     if (argc < 2) {
         fprintf(stderr,"ERROR, no port provided\n");
         exit(1);
     }
     sockfd = socket(AF_INET, SOCK_STREAM, 0);
     if (sockfd < 0) 
        error("ERROR opening socket");
     bzero((char *) &serv_addr, sizeof(serv_addr));
     portno = atoi(argv[1]);
     serv_addr.sin_family = AF_INET;
     serv_addr.sin_addr.s_addr = INADDR_ANY;
     serv_addr.sin_port = htons(portno);
     if (bind(sockfd, (struct sockaddr *) &serv_addr,
              sizeof(serv_addr)) < 0) 
              error("ERROR on binding");
     listen(sockfd,5);
     clilen = sizeof(cli_addr);
     while (1) {
         newsockfd = accept(sockfd, 
               (struct sockaddr *) &cli_addr, &clilen);
         if (newsockfd < 0) 
             error("ERROR on accept");
         pid = fork();
         if (pid < 0)
             error("ERROR on fork");
         if (pid == 0)  {
             close(sockfd);
             game(newsockfd);
             exit(0);
         }
         else close(newsockfd);
     } /* end of while */
     close(sockfd);
     return 0; /* we never get here */
}

/******** DOSTUFF() *********************
 There is a separate instance of this function 
 for each connection.  It handles all communication
 once a connnection has been established.
 *****************************************/
void game(int sock)
{
    int i, n, x, hostScore, playerScore;
    char c[2] = {0, '\n'};
    Deck deck[52];
    int hostCards[8], nHostCards = 0;
    int playerCards[8], nPlayerCards = 0;
    int play = 1, choosen = 1;
   
    printf("A new player has connected.\n");
    srand(time(NULL));
    createdeck(deck);
    /*for(i = 0; i < 52; i++)
    {
        printf("Deck[%d] name = %s, type = %s, value = %d\n", i, deck[i].name, deck[i].type, deck[i].value);
    }*/
    x = chooseCard(hostCards, playerCards, nHostCards, nPlayerCards);
    nHostCards += 2;
    nPlayerCards += 2;
    hostCards[0] = x;
    x = chooseCard(hostCards, playerCards, nHostCards, nPlayerCards);
    hostCards[1] = x;
    x = chooseCard(hostCards, playerCards, nHostCards, nPlayerCards);
    playerCards[0] = x;
    x = chooseCard(hostCards, playerCards, nHostCards, nPlayerCards);
    playerCards[1] = x;
    

    while(play)
    {   
       
        sendScore(deck, hostCards, playerCards, nHostCards, nPlayerCards, sock);
        printf("\nSending him score\n");
        printf("Asking if he wants to draw more\n");
        n = write(sock, "Do you want to draw additional card? Or change last one? (Y/N/L)\n", 66);
        if(n < 0) error("ERROR reading from socker");
        n = read(sock, c, 1);
        if (n < 0) error("ERROR reading from socket");
        printf("He said: %c\n",c[0]);
        if(c[0] == 'y' || c[0] == 'Y')
        {
            hostScore = checkScore(deck, hostCards, nHostCards);
            playerScore = checkScore(deck, playerCards, nPlayerCards);
            printf("Host score = %d, player score = %d\n", hostScore, playerScore);

            x = chooseCard(hostCards, playerCards, nHostCards, nPlayerCards);
            playerCards[nPlayerCards] = x;
            nPlayerCards++;
            playerScore = checkScore(deck, playerCards, nPlayerCards);
            printf("Host score = %d, player score = %d\n", hostScore, playerScore);

            if(playerScore > 21)
            {
                printf("Player has lost");
                sendScore(deck, hostCards, playerCards, nHostCards, nPlayerCards, sock);
                n = write(sock, "End: You have lost\n", 20);
                if(n < 0) error("ERROR reading from socker");
                play = 0;
            }
            else if(hostScore < playerScore)
            {
                x = chooseCard(hostCards, playerCards, nHostCards, nPlayerCards);
                hostCards[nHostCards] = x;
                nHostCards++;
                hostScore = checkScore(deck, hostCards, nHostCards); 
                printf("Host score = %d, player score = %d\n", hostScore, playerScore);
                if(hostScore > 21)
                {
                    printf("Host has lost");
                    sendScore(deck, hostCards, playerCards, nHostCards, nPlayerCards, sock);
                    n = write(sock, "End: You have won!\n", 20);
                    if(n < 0) error("ERROR reading from socker");
                    play = 0;
                }
            }
        }
        else 
        {
           if(c[0] == 'n' || c[0] == 'N')
           {
               hostScore = checkScore(deck, hostCards, nHostCards);
               playerScore = checkScore(deck, playerCards, nPlayerCards);
               printf("Host score = %d, player score = %d\n", hostScore, playerScore);
               while(hostScore < playerScore)
               {
                   x = chooseCard(hostCards, playerCards, nHostCards, nPlayerCards);
                   hostCards[nHostCards] = x;
                   nHostCards++;
                   hostScore = checkScore(deck, hostCards, nHostCards);
                   printf("Host score = %d, player score = %d\n", hostScore, playerScore);
               }
               if(hostScore > playerScore && hostScore <= 21)
               {
                   printf("Player has lost");
                   printf("Host score = %d, player score = %d\n", hostScore, playerScore);
                   sendScore(deck, hostCards, playerCards, nHostCards, nPlayerCards, sock);
                   n = write(sock, "End: You have lost\n", 20);
                   if(n < 0) error("ERROR reading from socker");
                   play = 0;
               }
               else if(hostScore < playerScore || hostScore > 21)
               {
                    printf("Host has lost");
                    sendScore(deck, hostCards, playerCards, nHostCards, nPlayerCards, sock);
                    n = write(sock, "End: You have won!\n", 20);
                    if(n < 0) error("ERROR reading from socker");
                    play = 0;
               }
               else if(hostScore == playerScore)
               {
                   printf("It is a tie");
                   sendScore(deck, hostCards, playerCards, nHostCards, nPlayerCards, sock);
                   n = write(sock, "End: it was a tie!\n", 20);
                   if(n < 0) error("ERROR reading from socker");
                   play = 0;
               }
               play = 0;
           }
           if(c[0] == 'l' || c[0] == 'L')
           {
                nPlayerCards--;
           }
        }
            
    }

}

void createdeck(Deck deck[])
{
    int i, j, n = 0;
    deck[0].name = '2';
    deck[1].name = '3';
    deck[2].name = '4';
    deck[3].name = '5';
    deck[4].name = '6';
    deck[5].name = '7';
    deck[6].name = '8';
    deck[7].name = '9';
    deck[8].name = 'X';
    deck[9].name = 'J';
    deck[10].name = 'Q';
    deck[11].name = 'K';
    deck[12].name = 'A';  
    deck[13].name = '2';
    deck[14].name = '3';
    deck[15].name = '4';
    deck[16].name = '5';
    deck[17].name = '6';
    deck[18].name = '7';
    deck[19].name = '8';
    deck[20].name = '9';
    deck[21].name = 'X';
    deck[22].name = 'J';
    deck[23].name = 'Q';
    deck[24].name = 'K';
    deck[25].name = 'A';
    deck[26].name = '2';
    deck[27].name = '3';
    deck[28].name = '4';
    deck[29].name = '5';
    deck[30].name = '6';
    deck[31].name = '7';
    deck[32].name = '8';
    deck[33].name = '9';
    deck[34].name = 'X';
    deck[35].name = 'J';
    deck[36].name = 'Q';
    deck[37].name = 'K';
    deck[38].name = 'A';
    deck[39].name = '2';
    deck[40].name = '3';
    deck[41].name = '4';
    deck[42].name = '5';
    deck[43].name = '6';
    deck[44].name = '7';
    deck[45].name = '8';
    deck[46].name = '9';
    deck[47].name = 'X';
    deck[48].name = 'J';
    deck[49].name = 'Q';
    deck[50].name = 'K';
    deck[51].name = 'A';

    for(i = 0; i < 4; i++)
    {
        for(j = 2; j < 10; j++)
        {
            deck[n].value = j;
            if(i == 0) deck[n].type = "Hearts";
                else if(i == 1) deck[n].type = "Diamonds";
                else if(i == 2) deck[n].type = "Clubs";
                else deck[n].type = "Spades";
            n++;
        }
        for(j = 0; j < 4; j++)
        {
            deck[n].value = 10;
            if(i == 0) deck[n].type = "Hearts";
                else if(i == 1) deck[n].type = "Diamonds";
                else if(i == 2) deck[n].type = "Clubs";
                else deck[n].type = "Spades";
            n++;
        }
	deck[n].value = 0;
	if(i == 0) deck[n].type = "Hearts";
            else if(i == 1) deck[n].type = "Diamonds";
            else if(i == 2) deck[n].type = "Clubs";
            else deck[n].type = "Spades";
        n++;
    }
}

int chooseCard(int* hostCards, int* playerCards, int nHostCards, int nPlayerCards)
{
    int notChoosen = 1, x, i;
    while(notChoosen)
       {
           x = rand() % 52;
           for(i = 0; i < nHostCards; i++)
               if(hostCards[i] == x)
                   notChoosen = 2;
           for(i = 0; i < nPlayerCards; i++)
               if(hostCards[i] == x)
                   notChoosen = 2;
           if(notChoosen == 2)
               notChoosen = 1;
               else notChoosen = 0;
       }
    return x;
}

void sendScore(Deck deck[], int* hostCards, int* playerCards, int nHostCards, int nPlayerCards, int sock)
{
    char buffer[256];
    bzero(buffer,256);
    int i, n, nbuffer = 0;
    strcpy(buffer, "Host hand: \n");
    nbuffer += 13;
    for(i = 0; i < nHostCards; i++)
    {
        buffer[nbuffer] = deck[hostCards[i]].name;
        nbuffer++;
        strcpy(buffer + nbuffer, deck[hostCards[i]].type);
        nbuffer += 10;
        buffer[nbuffer] = ' ';
        nbuffer++;
    }
    strcpy(buffer + nbuffer, "\n\nYour hand: \n");
    nbuffer += 17;
    for(i = 0; i < nPlayerCards; i++)
    {
        buffer[nbuffer] = deck[playerCards[i]].name;
        nbuffer++;
        strcpy(buffer + nbuffer, deck[playerCards[i]].type);
        nbuffer += 10;
        buffer[nbuffer] = ' ';
        nbuffer++;
    }
    strcpy(buffer + nbuffer, "\n\n");
    nbuffer += 2;
    for(i = 0; i < 256; i++)
        printf("%c", buffer[i]);
    n = write(sock, buffer, 255);
    if(n < 0) error("ERROR reading from socker");

   /* n = read(sock,buffer,255);
    if (n < 0) error("ERROR reading from socket");
    printf("Here is the message: %s\n",buffer);

    n = write(sock,"I got your message",18);
    if (n < 0) error("ERROR writing to socket");*/
}

int checkScore(Deck deck[], int* Cards, int quantity)
{
    int i, aces = 0, sum = 0;
    for(i = 0; i < quantity; i++)
    {
       if(deck[Cards[i]].value == 0)
       {
           aces++;
       }
       else 
       {
           sum += deck[Cards[i]].value;
       }
    }
    if(aces > 1) 
    {
        sum += aces - 1;
    }
    if(aces > 0)
    {
        if(sum + 11 > 21) sum += 1;
        else sum += 11;
    }
    return sum;
}


















