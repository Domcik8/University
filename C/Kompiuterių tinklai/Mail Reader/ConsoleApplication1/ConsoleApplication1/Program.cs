// Usage  
using System;    
using System.Collections.Generic;    
using System.Linq;    
using System.Text;    
   
namespace ImapDemo    
{    
    class Program    
    {    
        static void Main(string[] args)    
        {    
            /* Connect to gmail using ssl. */   
            Imap imap = new Imap("imap.gmail.com", 993, true);    
   
            /* Authenticate using google email and password */
            //imap.Authenicate("uabandern", "passwordisreal");
            imap.Authenicate("6969test", "czerwonykaktus");    
   
            /* Get a list of folders */   
            var folders = imap.GetFolders();    
   
            /* Select a mailbox */    
            imap.SelectFolder("INBOX");    
   
            /* Get message using UID */   
            /* Second parameter is section type. e.g Plain text or HTML */    
            //Console.WriteLine(imap.GetMessage("UID message number", "1"));    
   
            /* Get message using index */   
            //Console.WriteLine(imap.GetMessage(1, "1"));
            Console.WriteLine(imap.GetMessage(2, ""));
   
            /* Get total message count */
            Console.WriteLine("Messages:");
            Console.WriteLine(imap.GetMessageCount());    
   
            /* Get total unseen message count */
            Console.WriteLine("Unseen messages:");
            Console.WriteLine(imap.GetUnseenMessageCount());    
   
            Console.ReadKey();    
        }    
    }    
}  