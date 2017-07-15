using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayList
{
    struct Song : IEquatable<Song>
    {
        private int nr;
        public int Nr 
        {
            get
            {
                return nr;
            }
            set
            {
                if ((value >= 0000) && (value <= 9999))
                    nr = value;
            }
        }
        public string name { get; set; }
        public string genre { get; set; }
        public string mood { get; set; }

        public Song(string line) : this()
        {
            Nr = System.Convert.ToInt32(line.Substring(0, 4));
            name = line.Substring(5, 40);
            genre = line.Substring(45, 10);
            mood = line.Substring(55, 5);
        }

        bool IEquatable<Song>.Equals(Song other)
        {
            if ((name.Equals(other.name)) && (nr != other.nr))
            {
                return true;
            }
            else return false;
        }
    }
}