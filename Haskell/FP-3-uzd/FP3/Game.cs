using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP3
{
    public class Game
    {
        public static string StartGame(string gameID, string playerID, char symbol)
        {
            if (playerID == "1")
                return MakeMove(new List<Move>(), gameID, playerID, symbol);
            return WaitForTurn(gameID, playerID, symbol);
        }

        private static string MakeMove(List<Move> moves, string gameID, string playerID, char symbol)
        {
            Maybe<Move> move = TakeTurn(moves, symbol);
            if (move.IsNothing())
                return "The End!!!";

            moves.Add(move.PureValue());
            return ContinuePlaying(moves, gameID, playerID, symbol);
        }

        private static string ContinuePlaying(List<Move> moves, string gameID, string playerID, char symbol)
        {
            string url = Http.BuildUrl(gameID, playerID);

            string serializedMoves = Serializer.Serialize(moves);
            Task<String> statusTask = Http.HttpPost(url, serializedMoves);
            string status = statusTask.Result;
            return WaitForTurn(gameID, playerID, symbol);
        }

        private static String WaitForTurn(string gameID, string playerID, char symbol)
        {
            string url = Http.BuildUrl(gameID, playerID);

            Task<string> responseTask = Http.HttpGet(url);
            string response = responseTask.Result;
            if (response == "Internal Server Error")
                return WaitForTurn(gameID, playerID, symbol);
            return MakeMove(Parser.Parse(response), gameID, playerID, symbol);
        }

        private static Maybe<Move> TakeTurn(List<Move> moves, Char symbol)
        {
            bool gameOver = AI.IsGameOver(moves);

            if (gameOver)
                return Maybe<Move>.Nothing();

            List<Spot> freeSpots = AI.GetFreeSpots(moves);
            if (freeSpots.Count == 0)
                return Maybe<Move>.Nothing();
            return AI.Attack(moves, freeSpots, symbol);
        }
    }
}
