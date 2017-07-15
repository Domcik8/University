using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP3
{
    public class AI
    {
        public static Maybe<Move> Attack(List<Move> moves, List<Spot> freeSpots, char symbol)
        {
            if (SpotStatus(moves, new Spot(1, 1)) == ' ')
                return new Maybe<Move>(new Move(1, 1, symbol));

            Maybe<Move> mustMove = GetMustMove(moves, freeSpots, symbol);
            Maybe<Move> newMove = (mustMove.IsNothing()) ? GetFreeMove(freeSpots, symbol) : mustMove;

            return newMove;
        }

        private static Maybe<Move> GetFreeMove(List<Spot> freeSpots, Char symbol)
        {
            if (IsSpotFree(freeSpots, new Spot(0, 0)))
                return new Maybe<Move>(new Move(0, 0, symbol));
            else if (IsSpotFree(freeSpots, new Spot(2, 0)))
                return new Maybe<Move>(new Move(2, 0, symbol));
            else if (IsSpotFree(freeSpots, new Spot(0, 2)))
                return new Maybe<Move>(new Move(0, 2, symbol));
            else if (IsSpotFree(freeSpots, new Spot(2, 2)))
                return new Maybe<Move>(new Move(2, 2, symbol));
            else if (IsSpotFree(freeSpots, new Spot(0, 1)))
                return new Maybe<Move>(new Move(0, 1, symbol));
            else if (IsSpotFree(freeSpots, new Spot(1, 0)))
                return new Maybe<Move>(new Move(1, 0, symbol));
            else if (IsSpotFree(freeSpots, new Spot(1, 2)))
                return new Maybe<Move>(new Move(1, 2, symbol));
            return new Maybe<Move>(new Move(2, 1, symbol));
        }

        public static List<Spot> GetFreeSpots(List<Move> moves)
        {
            List<Spot> freeSpots = new List<Spot>();
            if(SpotStatus(moves, new Spot(0, 0)) == ' ')
                freeSpots.Add(new Spot(0, 0));
            if (SpotStatus(moves, new Spot(0, 1)) == ' ')
                freeSpots.Add(new Spot(0, 1));
            if (SpotStatus(moves, new Spot(0, 2)) == ' ')
                freeSpots.Add(new Spot(0, 2));
            if (SpotStatus(moves, new Spot(1, 0)) == ' ')
                freeSpots.Add(new Spot(1, 0));
            if (SpotStatus(moves, new Spot(1, 1)) == ' ')
                freeSpots.Add(new Spot(1, 1));
            if (SpotStatus(moves, new Spot(1, 2)) == ' ')
                freeSpots.Add(new Spot(1, 2));
            if (SpotStatus(moves, new Spot(2, 0)) == ' ')
                freeSpots.Add(new Spot(2, 0));
            if (SpotStatus(moves, new Spot(2, 1)) == ' ')
                freeSpots.Add(new Spot(2, 1));
            if (SpotStatus(moves, new Spot(2, 2)) == ' ')
                freeSpots.Add(new Spot(2, 2));
            
            return freeSpots;
        }

        private static bool IsSpotFree(List<Spot> spots, Spot spot)
        {
            if (spots.Count == 0)
                return false;
            if (spots.First().x == spot.x && spots.First().y == spot.y)
                return true;
            return IsSpotFree(spots.GetRange(1, spots.Count - 1), spot);
        }

        public static Maybe<Move> GetMustMove(List<Move> moves, List<Spot> spots, char symbol)
        {
            Spot spot = GetMustPosition(moves, spots, symbol);
            Maybe<Move> mustMove = (spot.x == 5 && spot.y == 5) ? Maybe<Move>.Nothing() : new Maybe<Move>(new Move(spot.x, spot.y, symbol));
            return mustMove;
        }

        private static Spot GetMustPosition(List<Move> moves, List<Spot> spots, Char symbol)
        {
            if (spots.Count == 0)
                return new Spot(5, 5);
            if (IsAMustSpot(moves, spots[0], symbol))
                return spots.First();
            else
                return GetMustPosition(moves, spots.GetRange(1, spots.Count - 1), symbol);
        }

        public static bool IsAMustSpot(List<Move> moves, Spot spot, Char symbol)
        {
            bool mustDefendHorizontaly = WillWinHorizontally(moves, spot, symbol, 2);
            bool mustDefendVerticaly = WillWinVertically(moves, spot, symbol, 2);
            bool mustDefendDiaggonaly = WillWinDiagonally(moves, spot, symbol, 2);
            bool result = mustDefendHorizontaly || mustDefendVerticaly || mustDefendDiaggonaly;
            return result;
        }

        private static bool WillWinHorizontally(List<Move> moves, Spot spot, Char symbol, int i)
        {
            char spotStatus1 = SpotStatus(moves, new Spot(0, spot.y));
            char spotStatus2 = SpotStatus(moves, new Spot(1, spot.y));
            char spotStatus3 = SpotStatus(moves, new Spot(2, spot.y));
            int position1 = (spotStatus1 != symbol && spotStatus1 != ' ') ? 1 : 0;
            int position2 = (spotStatus2 != symbol && spotStatus2 != ' ') ? 1 : 0;
            int position3 = (spotStatus3 != symbol && spotStatus3 != ' ') ? 1 : 0;

            bool result = (position1 + position2 + position3 >= i) ? true : false;
            return result;
        }

        private static bool WillWinVertically(List<Move> moves, Spot spot, Char symbol, int i)
        {
            char spotStatus1 = SpotStatus(moves, new Spot(spot.x, 0));
            char spotStatus2 = SpotStatus(moves, new Spot(spot.x, 1));
            char spotStatus3 = SpotStatus(moves, new Spot(spot.x, 2));
            int position1 = (spotStatus1 != symbol && spotStatus1 != ' ') ? 1 : 0;
            int position2 = (spotStatus2 != symbol && spotStatus2 != ' ') ? 1 : 0;
            int position3 = (spotStatus3 != symbol && spotStatus3 != ' ') ? 1 : 0;

            bool result = (position1 + position2 + position3 >= i) ? true : false;
            return result;
        }

        private static bool WillWinDiagonally(List<Move> moves, Spot spot, Char symbol, int i)
        {
            char spotStatus1 = SpotStatus(moves, new Spot(0, 0));
            char spotStatus2 = SpotStatus(moves, new Spot(2, 2));
            char spotStatus3 = SpotStatus(moves, new Spot(1, 1));
            char spotStatus4 = SpotStatus(moves, new Spot(0, 2));
            char spotStatus5 = SpotStatus(moves, new Spot(2, 0));
            int position1 = (spotStatus1 != symbol && spotStatus1 != ' ') ? 1 : 0;
            int position2 = (spotStatus2 != symbol && spotStatus2 != ' ') ? 1 : 0;
            int position3 = (spotStatus3 != symbol && spotStatus3 != ' ') ? 1 : 0;
            int position4 = (spotStatus4 != symbol && spotStatus4 != ' ') ? 1 : 0;
            int position5 = (spotStatus5 != symbol && spotStatus5 != ' ') ? 1 : 0;

            bool result = ((((spot.x == 0 && spot.y == 0) || (spot.x == 2 && spot.y == 2))
                && (position1 + position2 + position3 >= i))
                || (((spot.x == 2 && spot.y == 0) || (spot.x == 0 && spot.y == 2))
                && (position3 + position4 + position5 >= i)))
                ? true : false;
            return result;
        }

        private static char SpotStatus(List<Move> moves, Spot spot)
        {
            if (moves.Count == 0)
                return ' ';
            if (moves[0].x == spot.x && moves[0].y == spot.y)
                return moves[0].z;
            else return SpotStatus(moves.GetRange(1, moves.Count - 1), spot);
        }

        public static bool IsGameOver(List<Move> moves)
        {
            bool xWon = DidWin(moves, 'x');
            bool oWon = DidWin(moves, 'o');
            return (xWon || oWon);
        }

        private static bool DidWin(List<Move> moves, Char symbol)
        {
            bool wonHorizontaly1 = WillWinHorizontally(moves, new Spot(0, 0), symbol, 3);
            bool wonHorizontaly2 = WillWinHorizontally(moves, new Spot(1, 0), symbol, 3);
            bool wonHorizontaly3 = WillWinHorizontally(moves, new Spot(2, 0), symbol, 3);
            bool wonVerticaly1 = WillWinVertically(moves, new Spot(0, 0), symbol, 3);
            bool wonVerticaly2 = WillWinVertically(moves, new Spot(0, 1), symbol, 3);
            bool wonVerticaly3 = WillWinVertically(moves, new Spot(0, 2), symbol, 3);
            bool wonDiaggonaly1 = WillWinDiagonally(moves, new Spot(0, 0), symbol, 3);
            bool wonDiaggonaly2 = WillWinDiagonally(moves, new Spot(0, 2), symbol, 3);
            bool result = wonHorizontaly1 || wonHorizontaly2 || wonHorizontaly3 || wonVerticaly1 || wonVerticaly2 || wonVerticaly3 || wonDiaggonaly1 || wonDiaggonaly2;
            return result;
        }

    }
}
