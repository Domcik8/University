using System;
using System.Collections.Generic;

namespace FP3
{
    public class Parser
    {
        public static List<Move> Parse(string str)
        {
            string str1 = RemoveWhiteSpaces(str);
            string str2 = RemoveMExpressionPrefix(str1);
            string str3 = RemoveOpeningSquareBracket(str2);
            MovesString movesString = ParseMExpressions(new List<Move>(), str3);
            return movesString.moves;
        }

        private static MovesString ParseMExpressions(List<Move> acc, string mExpression)
        {
            if (mExpression[0] == ']')
                return new MovesString(acc, mExpression);
            if (mExpression[0] == ';')
                return ParseMExpressions(acc, mExpression.Substring(1));

            CharString charString1 = ReadPropertyName(mExpression);
            string mExpression1 = RemoveSemicolon(charString1.str);
            MoveString moveString1 = ParseMExpression(mExpression1);

            acc.Add(moveString1.move);

            return ParseMExpressions(acc, moveString1.str);
        }

        private static MoveString ParseMExpression(string mExpression)
        {
            string mExpression1 = RemoveMExpressionPrefix(mExpression);
            string mExpression2 = RemoveOpeningSquareBracket(mExpression1);

            CharString charString1 = ReadPropertyName(mExpression2);
            string mExpression4 = RemoveSemicolon(charString1.str);
            IntString charString2 = ReadProperty(mExpression4);
            string mExpression6 = RemoveSemicolon(charString2.str);

            CharString charString3 = ReadPropertyName(mExpression6);
            string mExpression8 = RemoveSemicolon(charString3.str);
            IntString charString4 = ReadProperty(mExpression8);
            string mExpression10 = RemoveSemicolon(charString4.str);

            CharString charString5 = ReadPropertyName(mExpression10);
            string mExpression12 = RemoveSemicolon(charString5.str);
            IntString charString6 = ReadProperty(mExpression12);
            string rest = RemoveClosingSquareBracket(charString6.str);

            int x, y, v;
            char trueV;

            if (charString1.c == 'x')
                x = charString2.i;
            else if (charString3.c == 'x')
                x = charString4.i;
            else x = charString6.i;

            if (charString1.c == 'y')
                y = charString2.i;
            else if (charString3.c == 'y')
                y = charString4.i;
            else y = charString6.i;

            if (charString1.c == 'v')
                v = charString2.i;
            else if (charString3.c == 'v')
                v = charString4.i;
            else v = charString6.i;

            trueV = (v == 3) ? 'x' : 'o';

            return new MoveString(new Move(x, y, trueV), rest);
        }

        private static string RemoveWhiteSpaces(string message)
        {
            return RemoveWhiteSpaces2("", message);
        }

        private static string RemoveWhiteSpaces2(string acc, string message)
        {
            if (message == "")
                return acc;
            if (message[0] != ' ')
                return RemoveWhiteSpaces2(acc + message[0], message.Substring(1));
            else
                return RemoveWhiteSpaces2(acc, message.Substring(1));
        }

        private static string RemoveOpeningSquareBracket(string expression)
        {
            if (expression[0] == '[')
                return expression.Substring(1);
            throw new Exception("'[' expected");
        }

        private static string RemoveClosingSquareBracket(string expression)
        {
            if (expression[0] == ']')
                return expression.Substring(1);
            throw new Exception("']' expected");
        }

        private static string RemoveMExpressionPrefix(string expression)
        {
            if (expression[0] == 'm')
                return expression.Substring(1);
            throw new Exception("'m' expected");
        }

        private static string RemoveSemicolon(string expression)
        {
            if (expression[0] == ';')
                return expression.Substring(1);
            throw new Exception("';' expected");
        }

        private static CharString ReadPropertyName(string expression)
        {
            if (expression[0] == '\"' && expression[2] == '\"')
            {
               return new CharString(expression[1], expression.Substring(3));
            }
            throw new Exception("Property name expected");
        }

        private static IntString ReadProperty(string expression)
        {
            if (expression[0] == '0')
                return new IntString(0, expression.Substring(1));
            else if (expression[0] == '1')
                return new IntString(1, expression.Substring(1));
            else if (expression[0] == '2')
                return new IntString(2, expression.Substring(1));
            else if (expression[0] == '\"')
            {
                if (expression[1] == 'x' || expression[1] == 'X')
                {
                    if (expression[2] == '\"')
                        return new IntString(3, expression.Substring(3));
                }
                else if (expression[1] == 'o' || expression[1] == 'O')
                    if (expression[2] == '\"')
                    {
                        return new IntString(4, expression.Substring(3));
                    }
            }
            throw new Exception("Property expected");
        }
    }
}


