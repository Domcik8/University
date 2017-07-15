using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayList
{
    class MyList : IEnumerable<Song>
    {
        LinkedList<Song> songs;
        Song last;

        public MyList()
        {
            songs = new LinkedList<Song>();
        }

        public void Add(Song item)
        { 
            songs.AddLast(item);
            last = item;
        }

        public void Delete(Song item)
        {
            songs.RemoveLast();
        }

        public IEnumerator<Song> GetEnumerator()
        {
            foreach (Song song in songs)
            {
                yield return song;
            }
        }

        // We must implement this method because  
        // IEnumerable<T> inherits IEnumerable 
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {

            return this.GetEnumerator();
        }

        public Song Last() { return last; }

        public void CleanSongs(MyList playList, Song last)
        {
            foreach (Song song1 in playList)
            {
                if (((IEquatable<Song>)song1).Equals(last))
                {
                    playList.Delete(last);
                    break;
                }
            }
        }
    }
}
