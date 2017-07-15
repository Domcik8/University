module MExpression.Parse where

import Types
import Data.Bool

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
       
       (property1, mExpression5) = readProperty mExpression4
       mExpression6 = removeSemicolon mExpression5
       
       (propertyName2, mExpression7) = readPropertyName mExpression6
       mExpression8 = removeSemicolon mExpression7
       (property2, mExpression9) = readProperty mExpression8
       mExpression10 = removeSemicolon mExpression9
       
       (propertyName3, mExpression11) = readPropertyName mExpression10
       mExpression12 = removeSemicolon mExpression11
       (property3, mExpression13) = readProperty mExpression12
       mExpression14 = removeClosingSquareBracket mExpression13
       
       rest = mExpression14
       
       x = if (propertyName1 == 'x')
         then property1
         else if (propertyName2 == 'x')
           then property2
           else property3
       y = if (propertyName1 == 'y')
         then property1
         else if (propertyName2 == 'y')
           then property2
           else property3
       v = if (propertyName1 == 'v')
         then property1
         else if (propertyName2 == 'v')
           then property2
           else property3
       trueV = if (v == 3)
         then 'x'
         else 'o'
    in
        (Move x y trueV, rest)

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
--readPropertyName ('\"':'x':'\"':t) = ('x', t)
--readPropertyName ('\"':'X':'\"':t) = ('x', t)
--readPropertyName ('\"':'y':'\"':t) = ('y', t)
--readPropertyName ('\"':'Y':'\"':t) = ('y', t)
--readPropertyName ('\"':'v':'\"':t) = ('v', t)
--readPropertyName ('\"':'V':'\"':t) = ('v', t)
readPropertyName ('\"':x:'\"':t) = (x, t)
readPropertyName _  = error "Property name expected"

--readDigit :: String -> (Int, String)
--readDigit ('0':rest) = (0, rest)
--readDigit ('1':rest) = (1, rest) 
--readDigit ('2':rest) = (2, rest) 
--readDigit _ = error "Digit expected"
--
--readPlayer :: String -> (Char, String)
--readPlayer ('\"': 'x' : '\"': rest) = ('x', rest)
--readPlayer ('\"': 'X' : '\"': rest) = ('x', rest)
--readPlayer ('\"': 'o' : '\"': rest) = ('o', rest)
--readPlayer ('\"': 'O' : '\"': rest) = ('o', rest)
--readPlayer _ = error "Player expected"

readProperty :: String -> (Int, String)
readProperty ('0':rest) = (0, rest)
readProperty ('1':rest) = (1, rest)
readProperty ('2':rest) = (2, rest)
readProperty ('\"': 'x' : '\"': rest) = (3, rest)
readProperty ('\"': 'X' : '\"': rest) = (3, rest)
readProperty ('\"': 'o' : '\"': rest) = (4, rest)
readProperty ('\"': 'O' : '\"': rest) = (4, rest)
readProperty _ = error "Property expected"