using System;
using System.Collections;

using FluentAssertions;
using NUnit.Framework;

namespace LCDDigits.Lib.Tests
{
    public class LCDDigitsConverterTests
    {
        LCDDigitsConverter sut;

        [SetUp]
        public void Setup()
        {
            sut = new LCDDigitsConverter();
        }

        [Test]
        public void Should_throw_exception_for_negative_number()
        {
            Action act = () => sut.ConvertToLCDDigit(-100);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestCaseSource(nameof(GetTestCases))]
        public void Should_return_valid_lcd_digit(int year, string expectedResult)
        {
            var actual = sut.ConvertToLCDDigit(year);

            actual.Should().Be(expectedResult);
        }

        static IEnumerable GetTestCases()
        {
            yield return new object[] { 0, 
                " _  \r\n" +
                "| | \r\n" +
                "|_| \r\n"
                };
            yield return new object[] { 23,
                " _   _  \r\n" +
                " _|  _| \r\n" +
                "|_   _| \r\n"
                };
            yield return new object[] { 96,
                " _   _  \r\n" +
                "|_| |_  \r\n" +
                " _| |_| \r\n"
                };
            yield return new object[] { 100,
                "     _   _  \r\n" +
                "  | | | | | \r\n" +
                "  | |_| |_| \r\n"
                };
        }
    }
}