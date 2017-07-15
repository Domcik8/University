using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FP3
{
    public struct Move
    {
        public readonly int x;
        public readonly int y;
        public readonly char z;

        public Move(int x, int y, char z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override string ToString()
        {
            return String.Format("Move [x: {0}, y: {1}, z: '{2}']", x, y, z);
        }
    }

    public struct Spot
    {
        public int x;
        public int y;

        public Spot(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct IntString
    {
        public int i;
        public string str;

        public IntString(int i, string str)
        {
            this.i = i;
            this.str = str;
        }
    }

    public struct CharString
    {
        public char c;
        public string str;

        public CharString(char c, string str)
        {
            this.c = c;
            this.str = str;
        }
    }
     
    public struct MoveString
    {
        public Move move;
        public string str;

        public MoveString(Move move, string str)
        {
            this.move = move;
            this.str = str;
        }
    }

    public struct MovesString
    {
        public List<Move> moves;
        public string str;

        public MovesString(List<Move> move, string str)
        {
            this.moves = move;
            this.str = str;
        }
    }
}
