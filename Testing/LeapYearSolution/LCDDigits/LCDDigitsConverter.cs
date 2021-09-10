using System;
using System.Text;

namespace LCDDigits
{
    public class LCDDigitsConverter : ILCDDigitsConverter
    {
        const int LinesAmount = 3;
        const char DigitDivider = ' ';
        const string LineSeparator = "\r\n";

        public string ConvertToLCDDigit(int number)
        {
            var builder = new StringBuilder();

            if (number < 0)
                throw new ArgumentOutOfRangeException(nameof(number));

            for (int line = 0; line < LinesAmount; line++)
            {
                BuildLine(builder, number.ToString(), line);
                builder.Append(LineSeparator);
            }

            return builder.ToString();
        }

        void BuildLine(StringBuilder builder, string number, int lineNumber)
        {
            foreach (var digit in number)
            {
                var lcdDigitPart = lcdDigits[CharToIntValue(digit), lineNumber];

                builder.Append(lcdDigitPart);
                builder.Append(DigitDivider);
            }
        }

        static int CharToIntValue(char digit)
        {
            return (int)char.GetNumericValue(digit);
        }

        readonly string[,] lcdDigits =
        {
            {
                " _ ",
                "| |",
                "|_|"
            },
            {
                "   ",
                "  |",
                "  |"
            },
            {
                " _ ",
                " _|",
                "|_ "
            },
            {
                " _ ",
                " _|",
                " _|"
            },
            {
                "   ",
                "|_|",
                "  |"
            },
            {
                " _ ",
                "|_ ",
                " _|"
            },
            {
                " _ ",
                "|_ ",
                "|_|"
            },
            {
                " _ ",
                "  |",
                "  | "
            },
            {
                " _ ",
                "|_|",
                "|_|"
            },
            {
                " _ ",
                "|_|",
                " _|"
            }
        };
    }
}
