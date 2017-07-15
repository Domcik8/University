using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HigLabo.Net.Mail;
using HigLabo.Net.Smtp;
using HigLabo.Net.Pop3;
using HigLabo.Net.Imap;


namespace HigLabo.Mail.SampleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            int error = 0;
            bool run = true;
            string nickname, password;
            Console.WriteLine("Welcome to Dmail reader.\n");
            while (run)
            {
               /* Console.WriteLine("Please enter your email adress: ");
                nickname = Console.ReadLine();
                Console.WriteLine("Please enter your password: ");
                password = Returnpassword();
                Console.WriteLine("\nLoading your mail: \n");*/

                error = ImapMailReceive();
                //error = ImapMailReceive(nickname, password);
                if (error == 0)
                    run = false;
            }
        }

        public static string Returnpassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                Console.Write("*");
                if (info.Key != ConsoleKey.Backspace)
                {
                    password += info.KeyChar;
                    info = Console.ReadKey(true);
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring
                        (0, password.Length - 1);
                    }
                    info = Console.ReadKey(true);
                }
            }
            return password;
        }

        private static int ImapMailReceive() //string nickname, string password
        //private static int ImapMailReceive(string nickname, string password)
        {
            bool run = true, choosen;
            int mail = 0, i;
            string data;
            MailMessage mg = null;
            using (ImapClient cl = new ImapClient("imap.gmail.com", 993, "6969test", "czerwonykaktus"))
            //using (ImapClient cl = new ImapClient("imap.gmail.com", 993, nickname, password))
            {
                cl.Ssl = true;
                var bl = cl.Authenticate();
                if (bl == true)
                {
                    //Select folder
                    var folder = cl.SelectFolder("INBOX");
                    //Get all mail from folder
                    while (run)
                    {
                        Console.Clear();
                        for (i = 0; i < folder.MailCount; i++)
                        {
                                mg = cl.GetMessage(i + 1);
                                Console.Write(i + 1);
                                Console.Write(". Date: " + mg.Date.LocalDateTime + "\nFrom: " + mg.From + "\nSubject: " + mg.Subject + "\n\n");
                                //Console.WriteLine(mg.BodyText);
                        }
                        choosen = false;
                        Console.Write("Do you want to open mail? (Y/N): ");
                        data = Console.ReadLine();
                        if(data == "n" || data == "N")
                        {
                            Console.WriteLine("Goodbye");
                            Console.ReadLine();
                            run = false;
                        }
                        if (data == "y" || data == "Y")
                        {
                            while (!choosen)
                            {
                                Console.Write("Which mail do you want to open: ");
                                data = Console.ReadLine();
                                Int32.TryParse(data, out mail);
                                if (mail > 0 && mail <= i)
                                    choosen = true;
                            }
                            Console.Clear();
                            mg = cl.GetMessage(mail);
                            Console.Write(mail);
                            Console.Write(". Date: " + mg.Date.LocalDateTime + "\nFrom: " + mg.From + "\nSubject: " + mg.Subject + "\n\n");
                            Console.Write(mg.BodyText);
                            Console.ReadLine();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Could not connect");
                    Console.ReadKey();
                    Console.Clear();
                    return 1;
                }
                return 0;
            }
        }
    }
}
