using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayList
{
    interface ISelector
    {
        void selectGenres(string[] selectedGenres);
        string selectMood();
        void selectSongs(MyList playList, string[] selectedGenres, string selectedMood);
    }
}
