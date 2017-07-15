using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP3
{
    public class Serializer
    {
        public static string Serialize(List<Move> moves)
        {
            string prefix = "m[";
            string suffix = "]";
            string serializedMoves = SerializeMoves(moves);
            if(serializedMoves.Length == 0)
                return prefix + serializedMoves.Substring(0, serializedMoves.Length) + suffix;
            return prefix + serializedMoves.Substring(0, serializedMoves.Length - 1) + suffix;
        }

        private static string SerializeMoves(List<Move> moves)
        {
            return SerializeMoves1(moves, "", 0);
        }

        private static string SerializeMoves1(List<Move> moves, string acc, int i)
        {
            if (moves.Count == 0)
                return acc;
            string serializedMove = SerializeMove(moves.First(), i);
            return SerializeMoves1(moves.GetRange(1, moves.Count - 1), acc + serializedMove, i + 1);
        }

        private static string SerializeMove(Move move, int i)
        {
            string prefix = "\"" + i + "\";m[";
            string xNotation = "\"x\";";
            string yNotation = "\"y\";";
            string vNotation = "\"v\";";
            int x = move.x;
            int y = move.y;
            char v = move.z;

            return (prefix + xNotation + x + ";" + yNotation + y + ";" + vNotation + "\"" + v + "\"];" + string.Empty);
        }
    }
}
