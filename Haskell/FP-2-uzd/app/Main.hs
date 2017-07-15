module Main where

import Game

main :: IO ()
main = do
  putStrLn "Enter game id:"
  gameId <- getLine
  putStrLn "Select player id (1 or 2):"
  playerId <- readInput "1" "2" "Invalid player, try again!"
  putStrLn "Select tile ('x' or 'o'):"
  tile <- readInput "x" "o" "Invalid tile, try againnnn!"
  --let end = startGame gameId playerId (tile !! 0)
  --putStrLn end KOdel veikia, kai startGame grazina String bet ne IO String
  end <- startGame gameId playerId (tile !! 0)
  print end
  _ <- getLine
  putStrLn "End"
  where
    readInput :: String -> String -> String -> IO String
    readInput val1 val2 errorMessage = do
      result <- getLine
      if (result /= val1) && (result /= val2)
      then do
        putStrLn errorMessage
        readInput val1 val2 errorMessage 
      else
        return result