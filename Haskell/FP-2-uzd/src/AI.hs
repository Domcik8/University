module AI
where

import Types
import MExpression.Parse
import MExpression.Serialize
import Data.Bool
import Text.Read (readMaybe)

attack :: [Move] -> [(Int, Int)] -> Char -> Move
attack moves freeSpots symbol = 
  let
    newMove = if (spotStatus moves 1 1 == ' ')
      then Move 1 1 symbol
      else 
        let
          mustMove = getMustMove moves freeSpots symbol
          newMove' = if (extractX mustMove /= 5)
            then mustMove
            else getFreeMove freeSpots symbol
        in
          newMove' 
  in
    newMove

getFreeSpots :: [Move] -> [(Int, Int)]
getFreeSpots moves = do
  x <- [0 .. 2]
  y <- [0 .. 2]
  p <- if (spotStatus moves x y == ' ') then [(x,y)] else []
  return p
  
getFreeMove :: [(Int, Int)] -> Char -> Move
getFreeMove freeSpots symbol = 
  if (isSpotFree freeSpots 0 0)
    then Move 0 0 symbol
    else if (isSpotFree freeSpots 2 0)
      then Move 2 0 symbol
      else if (isSpotFree freeSpots 0 2)
        then Move 0 2 symbol
        else if (isSpotFree freeSpots 2 2)
            then Move 2 2 symbol
            else if (isSpotFree freeSpots 0 1)
              then Move 0 1 symbol
          else if (isSpotFree freeSpots 1 0)
              then Move 1 0 symbol
            else if (isSpotFree freeSpots 1 2)
              then Move 1 2 symbol
              else Move 2 1 symbol
  
isSpotFree :: [(Int, Int)] -> Int -> Int -> Bool
isSpotFree [] x y = False
isSpotFree (h:t) x y = 
  if (getSpotX h == x && getSpotY h == y)
    then True
    else isSpotFree t x y
  
getSpotX :: (Int, Int) -> Int
getSpotX (x,y) = x

getSpotY :: (Int, Int) -> Int
getSpotY (x,y) = y

getMustMove :: [Move] -> [(Int, Int)] -> Char -> Move
getMustMove move freePositions symbol =
  let
    (x, y) = getMustPosition move freePositions symbol
    mustMove = if ((x, y) == (5, 5)) then Move 5 2 symbol else Move x y symbol
  in
    mustMove

getMustPosition :: [Move] -> [(Int, Int)] -> Char -> (Int, Int)
getMustPosition moves [] symbol = (5,5)
getMustPosition moves (h:t) symbol = 
  if (isAMustSpot moves h symbol)
    then h
    else getMustPosition moves t symbol

isAMustSpot :: [Move] -> (Int, Int) -> Char -> Bool
isAMustSpot moves (x, y) symbol = 
  let
    mustDefendHorizontaly = willWinHorizontally moves (x, y) symbol 2
    mustDefendVerticaly = willWinVertically moves (x, y) symbol 2
    mustDefendDiaggonaly = willWinDiagonally moves (x, y) symbol 2
    result = mustDefendHorizontaly || mustDefendVerticaly || mustDefendDiaggonaly
  in
    result

willWinHorizontally :: [Move] -> (Int, Int) -> Char -> Int -> Bool
willWinHorizontally moves (x, y) symbol int = 
  let
    spotStatus1 = (spotStatus moves 0 y)
    spotStatus2 = (spotStatus moves 1 y)
    spotStatus3 = (spotStatus moves 2 y)
    position1 = if (spotStatus1 /= symbol && spotStatus1 /= ' ') then 1 else 0
    position2 = if (spotStatus2 /= symbol && spotStatus2 /= ' ') then 1 else 0
    position3 = if (spotStatus3 /= symbol && spotStatus3 /= ' ') then 1 else 0
    result = if (position1 + position2 + position3 >= int) then True else False
  in
    result

willWinVertically :: [Move] -> (Int, Int) -> Char -> Int -> Bool
willWinVertically moves (x, y) symbol int = 
 let
   spotStatus1 = (spotStatus moves x 0)
   spotStatus2 = (spotStatus moves x 1)
   spotStatus3 = (spotStatus moves x 2)
   position1 = if (spotStatus1 /= symbol && spotStatus1 /= ' ') then 1 else 0
   position2 = if (spotStatus2 /= symbol && spotStatus2 /= ' ') then 1 else 0
   position3 = if (spotStatus3 /= symbol && spotStatus3 /= ' ') then 1 else 0
   result = if (position1 + position2 + position3 >= int) then True else False
 in
   result

willWinDiagonally :: [Move] -> (Int, Int) -> Char -> Int -> Bool
willWinDiagonally moves (x, y) symbol int = 
 let
  spotStatus1 = (spotStatus moves 0 0)
  spotStatus2 = (spotStatus moves 2 2)
  spotStatus3 = (spotStatus moves 1 1)
  spotStatus4 = (spotStatus moves 0 2)
  spotStatus5 = (spotStatus moves 2 0)
  position1 = if (spotStatus1 /= symbol && spotStatus1 /= ' ') then 1 else 0
  position2 = if (spotStatus2 /= symbol && spotStatus2 /= ' ') then 1 else 0
  position3 = if (spotStatus3 /= symbol && spotStatus3 /= ' ') then 1 else 0
  position4 = if (spotStatus4 /= symbol && spotStatus4 /= ' ') then 1 else 0
  position5 = if (spotStatus5 /= symbol && spotStatus5 /= ' ') then 1 else 0
  result = if ((((x == 0 && y == 0) || (x == 2 && y == 2)) && (position1 + position2 + position3 >= int))
    || (((x == 2 && y == 0) || (x == 0 && y == 2)) && (position3 + position4 + position5 >= int)))
    then True
    else False
 in
   result
 
spotStatus :: [Move] -> Int -> Int -> Char
spotStatus [] x y = ' '
spotStatus (h:t) x y =
  if (extractX h == x && extractY h == y)
    then extractPlayer h
    else spotStatus t x y

isGameOver :: [Move] -> Bool
isGameOver moves =
  let
    xWon = didWin moves 'x'
    oWon = didWin moves 'o'
  in
    (xWon || oWon)

didWin :: [Move] -> Char -> Bool
didWin moves symbol = 
  let
    wonHorizontaly1 = willWinHorizontally moves (0, 0) symbol 3
    wonHorizontaly2 = willWinHorizontally moves (1, 0) symbol 3
    wonHorizontaly3 = willWinHorizontally moves (2, 0) symbol 3
    wonVerticaly1 = willWinVertically moves (0, 0) symbol 3
    wonVerticaly2 = willWinVertically moves (0, 1) symbol 3
    wonVerticaly3 = willWinVertically moves (0, 2) symbol 3
    wonDiaggonaly1 = willWinDiagonally moves (0, 0) symbol 3
    wonDiaggonaly2 = willWinDiagonally moves (0, 2) symbol 3
    result = wonHorizontaly1 || wonHorizontaly2 || wonHorizontaly3 || wonVerticaly1 || wonVerticaly2 || wonVerticaly3 || wonDiaggonaly1 || wonDiaggonaly2
  in
    result