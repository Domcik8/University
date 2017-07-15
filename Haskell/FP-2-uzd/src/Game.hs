module Game
where

import AI
import Types
import MExpression.Parse
import MExpression.Serialize
import Http
import Data.Bool
import Text.Read (readMaybe)

startGame :: String -> String -> Char -> IO String
startGame gameID "1" symbol = makeMove [] gameID "1" symbol
--startGame gameID "2" symbol = testHttp [] gameID "1" symbol
startGame gameID _ symbol = waitForTurn gameID "2" symbol

--Kodel kai testHttp turejau ne do o let blocka tai status = httpPost url serializedMoves nieko nedare
--testHttp :: [Move] -> String -> String -> Char -> IO String
--testHttp moves gameID playerID symbol = 
--  do
--    let url = buildUrl gameID playerID
--    statusCode <- htt url "de"
--    return $ url ++ " Naujausias"
--    
--htt :: String -> String -> IO Int
--htt a b = httpPost a b


makeMove :: [Move] -> String -> String -> Char -> IO String
makeMove moves gameID playerID symbol = 
  do
    let move = takeTurn moves symbol
    if (extractX move == 5)
      then return "The End"
      else continuePlaying (moves ++ [move]) gameID playerID symbol

continuePlaying :: [Move] -> String -> String -> Char -> IO String
continuePlaying moves gameID playerID symbol =
  do
    let url = buildUrl gameID playerID
    let serializedMoves = serialize moves
    status <- httpPost url serializedMoves
    waitForTurn gameID playerID symbol

waitForTurn :: String -> String -> Char -> IO String
waitForTurn gameID playerID symbol =
  do
    let url = buildUrl gameID playerID
    response <- httpGet url
    if response == "Internal Server Error"
      then waitForTurn gameID playerID symbol
      else makeMove (parse response) gameID playerID symbol

takeTurn :: [Move] -> Char -> Move
takeTurn moves symbol =
  let
    gameOver = isGameOver moves
    move = if (gameOver)
      then Move 5 6 symbol
      else 
        let
          freeSpots = getFreeSpots moves
        in
        if (freeSpots == [])
          then Move 5 0 symbol
          else attack moves freeSpots symbol
  in
    move