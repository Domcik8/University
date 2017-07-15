using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayList
{
    class FileReader
    {
        private string name{ get; set; }
        public string defaultPath = @"C:\Users\Dominik\Desktop\VU\3 semestras\Taikomasis objektinis programavimas\PlayList\PlayList\";
        private string path = @"C:\Users\Dominik\Desktop\VU\3 semestras\Taikomasis objektinis programavimas\PlayList\PlayList\PlayList1.txt";
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = defaultPath + value + ".txt";
            }
        }

        public string ReadFile(MyList playList)
        {
            string line;
            int checker = 0;
            int songNumber = 0;

            while (checker == 0)
            {
                try
                {
                    System.Console.WriteLine("Enter the name of txt file with songs");
                    name = System.Console.ReadLine();
                    Path = name;
                    System.IO.StreamReader file =
                        new System.IO.StreamReader(path);
                    while ((line = file.ReadLine()) != null)
                    {
                        songNumber++;
                        if (line.Length != 60)
                        {
                            Console.WriteLine("Warning Song {0} has been skipped: Song information has to be 60 characters long", songNumber);
                            continue;
                        }
                        if (!int.TryParse(line.Substring(0, 4), out checker))
                        {
                            Console.WriteLine("Warning Song {0} has been skipped: First 4 symbols have to be be digits", songNumber);
                            continue;
                        }
                        if (!((line.Substring(45, 10) == "Pop       ") || (line.Substring(45, 10) == "Electro   ")
                            || (line.Substring(45, 10) == "Pop rock  ") || (line.Substring(45, 10) == "Rock      ")))
                        {
                            Console.WriteLine("Warning Song {0} has been skipped: Mood information has to be: 'Sad' or 'Happy'", songNumber);
                            continue;
                        }
                        if (!((line.Substring(55, 5) == "Happy") || (line.Substring(55, 5) == "Sad  ")))
                        {
                            Console.WriteLine("Warning Song {0} has been skipped: Mood information has to be: 'Sad' or 'Happy'", songNumber);
                            continue;
                        }
                        playList.Add(new Song(line));
                        playList.CleanSongs(playList, playList.Last());
                    }
                    file.Close();
                    checker = 1;
                    System.Console.WriteLine("File has been opened\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nNo file named " + name + " could be found");
                    Console.WriteLine("Try again\n");
                }
            }
            return Path;
        }
    }
}
