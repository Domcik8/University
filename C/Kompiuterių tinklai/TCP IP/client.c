#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <string.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <netdb.h> 					//structure hostent

int getScore(int sockfd);

void error(const char *msg)
{
    perror(msg);
    exit(0);
}

int main(int argc, char *argv[])
{
    int sockfd, portno, n, play = 1, choosen = 0;
    char c[2] = {0, '\n'};
    struct sockaddr_in serv_addr;
    struct hostent *server;				//defines host 			
    if (argc < 3) {
       fprintf(stderr,"usage %s hostname port\n", argv[0]);
       exit(0);
    }
    portno = atoi(argv[2]);
    sockfd = socket(AF_INET, SOCK_STREAM, 0);
    if (sockfd < 0) 
        error("ERROR opening socket");
    server = gethostbyname(argv[1]);
    if (server == NULL) {
        fprintf(stderr,"ERROR, no such host\n");
        exit(0);
    }
    bzero((char *) &serv_addr, sizeof(serv_addr));
    serv_addr.sin_family = AF_INET;
    bcopy((char *)server->h_addr, 
         (char *)&serv_addr.sin_addr.s_addr,
         server->h_length);
    serv_addr.sin_port = htons(portno);
    if (connect(sockfd,(struct sockaddr *) &serv_addr,sizeof(serv_addr)) < 0) 
        error("ERROR connecting");

    while(play)
    {
        choosen = 0;
        play = getScore(sockfd);
        if(play == 1)
        {
            while(!choosen)
            {
                scanf("%c", &c);
                if(c[0] == 'y' || c[0] == 'Y' || c[0] =='n' || c[0] == 'N' || c[0] == 'l' || c[0] == 'L')
                {
                    n = write(sockfd, c, 1);
                    if (n < 0) 
                        error("ERROR writing to socket");
                    choosen = 1;
                }
                else if(c[0] != '\n') 
                         printf("Please choose once more\n");
            }
        }
    }
    close(sockfd);
    return 0;
}

int getScore(int sockfd)
{
    int n, i, ret = 1;
    char buffer[256];
    bzero(buffer, 256);
    n = read(sockfd, buffer, 255);
    if(n < 0) 
         error("ERROR reading from socket");
    if(buffer[0] == 'E') printf("I am E");
    for(i = 0; i < 255; i++) 
        printf("%c", buffer[i]);
    bzero(buffer, 256);
    n = read(sockfd, buffer, 255);
    if(n < 0) 
         error("ERROR reading from socket");
    if((buffer[0] == 'E') && (buffer[1] == 'n') && (buffer[2] == 'd')) ret = 0;
    for(i = 0; i < 255; i++) 
        printf("%c", buffer[i]);
    return ret;
}
