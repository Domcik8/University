using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace PlayList
{
    class SongSelector : ISelector
    {
        static int genresN = 4;
        string[] genres = {"Pop       ", "Electro   ", "Pop rock  ", "Rock      "};
        string[] moodes = {"Any", "Happy", "Sad  "};
        string input;

        public SongSelector(MyList playList, string input)
        {
            this.input = input;
            string[] selectedGenres = new string[genresN];
            selectGenres(selectedGenres);
            string selectedMood = selectMood();
            selectSongs(playList, selectedGenres, selectedMood);
        }

        public void selectGenres(string[] selectedGenres)
        {
            int i = 777, j = 0;
            System.Console.WriteLine("Select genres you like 1: 'Pop', 2: 'Electro', 3: 'Pop rock', 4: 'Rock'");
            System.Console.WriteLine("Type 9 to end genre selection");
            while (i != 9)
            { 
                System.Console.Write("Genre : ");
                Int32.TryParse(System.Console.ReadLine(), out i);
                if((i == 1) || (i == 2) || (i == 3) || (i == 4))
                {
                    selectedGenres[i - 1] = genres[i - 1];
                }
                else if (i == 9) { }
                else 
                {
                    System.Console.WriteLine("Please select genre or end current selection");
                    continue;
                }
                j++;
            }
        }

        public string selectMood()
        {
            int i = 777;
            string selectedMood = " ";
            Boolean moodSelected = false;
            System.Console.WriteLine("\nSelect prefered mood 1: 'Any', 2: 'Happy', 3: 'Sad':");
            while (!moodSelected)
            {
                System.Console.Write("Mood : ");
                Int32.TryParse(System.Console.ReadLine(), out i);
                if ((i == 1) || (i == 2) || (i == 3))
                {
                    selectedMood = moodes[i - 1];
                    moodSelected = true;
                }
                else System.Console.WriteLine("Please select prefered mood");
            }
            return selectedMood;
        }

        public void selectSongs(MyList playList, string[] selectedGenres, string selectedMood)
        {
            Protection protection = new Protection();
            protection.setInput(input);
            string key = protection.CreateKey();
            protection.EncryptFile(key, playList, selectedGenres, selectedMood);
        }
    }
}
