using System;
using System.Linq;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            if (stringValue is null)
                throw new ArgumentNullException();

            TrimSpaces(ref stringValue);

            if (!IsValidNumber(stringValue))
                throw new FormatException();

            var sign = GetSign(stringValue);
            TrimSign(ref stringValue);

            return ConvertToInt(stringValue, sign);
        }

        static int ConvertToInt(string stringValue, int sign)
        {
            var accumulator = 0;

            for (int i = 0; i < stringValue.Length; i++)
            {
                accumulator = checked(accumulator + sign 
                    * (int)Math.Pow(10, stringValue.Length - i - 1) 
                    * ToIntNumber(stringValue[i]).Value);
            }

            return accumulator;
        }

        void TrimSpaces(ref string stringValue)
        {
            stringValue = stringValue.Trim();
        }

        int GetSign(string stringValue)
        {
            return stringValue.FirstOrDefault() switch
            {
                '-' => -1,
                _ => 1,
            };
        }

        void TrimSign(ref string stringValue)
        {
            if (IsSign(stringValue[0]))
                stringValue = stringValue[1..];
        }

        bool IsValidNumber(string stringValue)
        {
            if (stringValue == string.Empty)
                return false;

            if (!IsSign(stringValue[0]) && !IsNumber(stringValue[0]))
                return false;

            for (int i = 1; i < stringValue.Length; i++)
            {
                if (!IsNumber(stringValue[i])) 
                    return false;
            }

            return true;
        }

        static bool IsSign(char x)
        {
            return x == '+' || x == '-';
        }

        static bool IsNumber(char charNumber)
        {
            return ToIntNumber(charNumber) != null;
        }

        static int? ToIntNumber(char charNumber)
        {
            return charNumber switch
            {
                '0' => 0,
                '1' => 1,
                '2' => 2,
                '3' => 3,
                '4' => 4,
                '5' => 5,
                '6' => 6,
                '7' => 7,
                '8' => 8,
                '9' => 9,
                _ => null
            };
        }
    }
}