using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP3
{
    public class Program
    {
        static void Main(string[] args)
        {
            string testMessage = "m[\"0\";   m[\"x\"; 0; \"y\";  2;   \"v\";  \"x\"];  \"1\";  m[\"x\";  0; \"y\";  0; \"v\"; \"o\"]; \"2\";   m[\"x\"; 2;  \"y\"; 2;   \"v\"; \"x\"];   \"3\";  m[\"x\"; 0;  \"y\"; 1; \"v\";  \"o\"];   \"4\";   m[\"x\";  1; \"y\";   0;   \"v\";   \"x\"]]";
            Console.WriteLine("Enter game id:");
            string gameID = Console.ReadLine();
            Console.WriteLine("Select player id (1 or 2)");
            string playerID = ReadInput("1", "2", "Invalid player, try again!");
            Console.WriteLine("Select tile ('x' or 'o'):");
            string tile = ReadInput("x", "o", "Invalid tile, try againnnn!");
            string end = Game.StartGame(gameID, playerID, tile[0]);
            Console.WriteLine(end);
            Console.ReadLine();
            Console.WriteLine("End");
        }

        private static string ReadInput(string val1, string val2, string errorMessage)
        {
            string result = Console.ReadLine();
            if (result != val1 && result != val2)
            {
                Console.WriteLine(errorMessage);
                return ReadInput(val1, val2, errorMessage);
            }
            return result;
        }
    }
}
