/* A simple server in the internet domain using TCP
   The port number is passed as an argument */
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <sys/types.h>  //definitions of data types
#include <sys/socket.h> //definitions of structures
#include <netinet/in.h> //constans and structures for internet domain addresses

void error(const char *msg)
{
    perror(msg);
    exit(1);
}

int main(int argc, d)
{
     int sockfd, newsockfd; 				
/*
*file descriptors
*These two variables store the values returned by the socket system call and the 
*accept system call.
*/
     int portno;					//port number 
     socklen_t clilen;					//size of address (for accept system call}
     char buffer[256];
     struct sockaddr_in serv_addr, cli_addr;		//address
/*
struct sockaddr_in
{
  short   sin_family; // must be AF_INET 
  u_short sin_port;
  struct  in_addr sin_addr;
  char    sin_zero[8]; // Not used, must be zero 
};
*/
     int n;						//how many characters in read() or write();
     if (argc < 2) {
         fprintf(stderr,"ERROR, no port provided\n");
         exit(1);
     }
     sockfd = socket(AF_INET, SOCK_STREAM, 0);
     if (sockfd < 0) 
        error("ERROR opening socket");
     bzero((char *) &serv_addr, sizeof(serv_addr));	//makes ser_addr to 0
     portno = atoi(argv[1]);				//argument to integer
     serv_addr.sin_family = AF_INET;			//address family
     serv_addr.sin_addr.s_addr = INADDR_ANY;		//Hosts IP // INADDR_ANY = mine	
     serv_addr.sin_port = htons(portno);		
//port number host byte order => port network byte order
     if (bind(sockfd, (struct sockaddr *) &serv_addr,
              sizeof(serv_addr)) < 0) 
//binds socket to address
              error("ERROR on binding");
     listen(sockfd,5);
     clilen = sizeof(cli_addr);
     newsockfd = accept(sockfd, 
                 (struct sockaddr *) &cli_addr, 
                 &clilen);
     if (newsockfd < 0) 
          error("ERROR on accept");
     bzero(buffer,256);
     n = read(newsockfd,buffer,255);
     if (n < 0) error("ERROR reading from socket");
     printf("Here is the message: %s\n",buffer);
     n = write(newsockfd,"I got your message",18);
     if (n < 0) error("ERROR writing to socket");
     close(newsockfd);
     close(sockfd);
     return 0; 
}
