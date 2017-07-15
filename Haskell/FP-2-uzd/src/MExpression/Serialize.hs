module MExpression.Serialize where

import Types

serialize :: [Move] -> String
serialize moves = 
  let
    prefix = "m["
    suffix = "]"
    serializedMoves = serializeMoves moves
  in prefix ++ (take (length serializedMoves - 1) serializedMoves) ++ suffix

serializeMoves :: [Move] -> String
serializeMoves moves = serializeMoves' moves "" 0
  where
    serializeMoves' :: [Move] -> String -> Int -> String
    serializeMoves' [] acc i = acc
    serializeMoves' (h:t) acc i = serializeMoves' t (acc ++ serializedMove) (i + 1)
      where
        serializedMove = serializeMove h i

serializeMove :: Move -> Int -> String
serializeMove move i =
  let 
    prefix = "\"" ++ show i ++ "\";m["
    xNotation = "\"x\";"
    yNotation = "\"y\";"
    vNotation = "\"v\";"
    x = extractX move
    y = extractY move
    v = extractPlayer move
  in
    prefix ++ xNotation ++ show x ++ ";" ++ yNotation ++ show y ++ ";" ++ vNotation ++ "\"" ++ [v] ++ "\"];"