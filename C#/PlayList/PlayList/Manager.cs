using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PlayList
{
    class Program
    {
        static void Main(string[] args)
        {
            int menu = 0;
            System.Console.WriteLine("Thanks for using Dominik's playlist maker!");
            while(menu != 3)
            {
                System.Console.WriteLine("\nWhat would you like to do?");
                System.Console.WriteLine("1: Create new favorite song list");
                System.Console.WriteLine("2: Open existing song list");
                System.Console.WriteLine("3: Exit");
                System.Console.Write("Menu : ");
                Int32.TryParse(System.Console.ReadLine(), out menu);
                System.Console.WriteLine();
                switch(menu)
                {
                    case 1:
                    {
                        string input;
                        Program manager = new Program();
                        MyList playList = new MyList();
                        FileReader reader = new FileReader();
                        input = reader.ReadFile(playList);
                        //manager.ShowPlayList(playList);
                        new SongSelector(playList, input);
                        break;
                    }

                    case 2:
                    {

                        Protection protection = new Protection();
                        string key = protection.CreateKey();
                        protection.DecryptFile(key);
                        break;
                    }
                    
                    case 3:
                    {
                        System.Console.WriteLine("Goodbye");
                        break;
                    }

                    default:
                    {
                        System.Console.WriteLine("Please select a valid option");
                        break;
                    }

                }
            }
        }

        protected void ShowPlayList(MyList playList)
        {
            foreach(Song song in playList)
            {
                Console.WriteLine(song.Nr.ToString("D4") + " " + song.name + song.genre + song.mood);
            }
        }
    }
}