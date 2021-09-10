using System;

namespace LeapYear.Lib
{
    public class LeapYearChecker : ILeapYearChecker
    {
        public bool IsLeapYear(int year)
        {
            if (year < 0)
                throw new ArgumentOutOfRangeException(nameof(year));

            return IsDivisibleBy(year, 4) && (!IsDivisibleBy(year, 100) || IsDivisibleBy(year, 400));
        }

        static bool IsDivisibleBy(int number, int divisor)
        {
            return number % divisor == 0;
        }
    }
}
