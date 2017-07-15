module Types
where

data Move = Move {
    posX :: Int
  , posY :: Int
  , z :: Char 
} deriving (Show, Eq)

extractX :: Move -> Int
extractX (Move x _ _) = x

extractY :: Move -> Int
extractY (Move _ y _) = y

extractPlayer :: Move -> Char
extractPlayer (Move _ _ z) = z