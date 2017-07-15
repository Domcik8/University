module Uzd1
where

import Data.Bool

data Move = Move {
    posX :: Int,
	posY :: Int,
	z :: Char 
} deriving Show

extractX :: Move -> Int
extractX (Move x _ _) = x

extractY :: Move -> Int
extractY (Move _ y _) = y

extractPlayer :: Move -> Char
extractPlayer (Move _ _ z) = z

checkMoves :: String -> String
checkMoves str =
	let
		(moves) = parse str
		correstQuantity = checkQuantity (moves)
	in if (correstQuantity)
		then 
			let 
				correctAlignment = isAlignmentCorrect (moves)
			in if (correctAlignment)
				then "Lenta teisingai sudeliota"
				else "Lentoje yra klaidu!"
		else "Lentoje yra klaidu!"
	
isAlignmentCorrect :: [Move] -> Bool
isAlignmentCorrect [] = True 
isAlignmentCorrect (h:[]) = True 
isAlignmentCorrect (h:t:t1) = 
	let
		deepResult = isAlignmentCorrect (t:t1)
		result = compareMoves h t
	in
		if ((not result) && deepResult)
		then isAlignmentCorrect (h:t1)
		else False

compareMoves :: Move -> Move -> Bool
compareMoves move1 move2 = 
	if (extractX move1 == extractX move2
		&& extractY move1 == extractY move2)
		then True
		else False

checkQuantity :: [Move] -> Bool
checkQuantity move =
	checkQuantity' 0 0 move
	where
		checkQuantity' :: Int -> Int -> [Move] -> Bool
		checkQuantity' x y [] = compareQuantity x y
		checkQuantity' x y (h:t) =
			if (extractPlayer h == 'x')
				then checkQuantity' (x + 1) y t
				else checkQuantity' x (y + 1) t
				
compareQuantity :: Int -> Int -> Bool
compareQuantity x y = if (abs(x - y) < 2)
	then True
	else False

parse :: String -> [Move]
parse str = 
	let
		str1 = removeWhiteSpaces str
		str2 = removeMExpressionPrefix str1
		str3 = removeOpeningSquareBracket str2
		(moves, result) = parseMExpressions [] str3
	in reverse moves

parseMExpressions :: [Move] -> String -> ([Move], String)
parseMExpressions acc (']':t) = (acc, t)
parseMExpressions acc (';':t) = parseMExpressions acc t
parseMExpressions acc mExpressions =
	let
		(propertyName1, mExpressions1) = readPropertyName mExpressions
		mExpressions2 = removeSemicolon mExpressions1
		(move, rest) = parseMExpression mExpressions2	
	in
		parseMExpressions (move:acc) rest
	
parseMExpression :: String -> (Move, String)
parseMExpression mExpression =
	let
		mExpression1 = removeMExpressionPrefix mExpression
		mExpression2 = removeOpeningSquareBracket mExpression1
		
		(propertyName1, mExpression3) = readPropertyName mExpression2
		mExpression4 = removeSemicolon mExpression3
		(digit1, mExpression5) = readDigit mExpression4
		mExpression6 = removeSemicolon mExpression5
		
		(propertyName2, mExpression7) = readPropertyName mExpression6
		mExpression8 = removeSemicolon mExpression7
		(digit2, mExpression9) = readDigit mExpression8
		mExpression10 = removeSemicolon mExpression9
		
		(propertyName3, mExpression11) = readPropertyName mExpression10
		mExpression12 = removeSemicolon mExpression11
		(player, mExpression13) = readPlayer mExpression12
		mExpression14 = removeClosingSquareBracket mExpression13
		
		rest = mExpression14
	in
		(Move digit1 digit2 player, rest)
		
removeWhiteSpaces :: [Char] -> [Char]
removeWhiteSpaces str =
  removeWhiteSpaces' [] str
  where
    removeWhiteSpaces' :: [Char] -> [Char] -> [Char]
    removeWhiteSpaces' acc [] = acc
    removeWhiteSpaces' acc (h:t) =
		if (h /= ' ')
			then removeWhiteSpaces' (acc++[h]) t
			else removeWhiteSpaces' acc t
	-- Kodel taip neveike?
	-- removeWhiteSpaces' acc (' ':t) = removeWhiteSpaces' acc t
	-- removeWhiteSpaces' acc (t1:t2) = removeWhiteSpaces' (t1:acc) t2 

removeOpeningSquareBracket :: String -> String
removeOpeningSquareBracket ('[':t) = t
removeOpeningSquareBracket _ = error "'[' expected"

removeClosingSquareBracket :: String -> String
removeClosingSquareBracket (']':t) = t
removeClosingSquareBracket _ = error "']' expected"

removeMExpressionPrefix :: String -> String
removeMExpressionPrefix ('m':t) = t
removeMExpressionPrefix _ = error "'m' expected"

removeSemicolon :: String -> String
removeSemicolon (';':t) = t
removeSemicolon _ = error "';' expected"

readPropertyName :: String -> (Char, String)
-- readPropertyName ('\"':'x':'\"':t) = ('x', t)
-- readPropertyName ('\"':'X':'\"':t) = ('x', t)
-- readPropertyName ('\"':'y':'\"':t) = ('y', t)
-- readPropertyName ('\"':'Y':'\"':t) = ('y', t)
-- readPropertyName ('\"':'v':'\"':t) = ('v', t)
-- readPropertyName ('\"':'V':'\"':t) = ('v', t)
readPropertyName ('\"':_:'\"':t) = ('k', t)
readPropertyName _  = error "Property name expected"

readDigit :: String -> (Int, String)
readDigit ('0':rest) = (0, rest)
readDigit ('1':rest) = (1, rest) 
readDigit ('2':rest) = (2, rest) 
readDigit _ = error "Digit expected"

readPlayer :: String -> (Char, String)
readPlayer ('\"': 'x' : '\"': rest) = ('x', rest)
readPlayer ('\"': 'X' : '\"': rest) = ('x', rest)
readPlayer ('\"': 'o' : '\"': rest) = ('o', rest)
readPlayer ('\"': 'O' : '\"': rest) = ('o', rest)
readPlayer _ = error "Player expected"
