using System;

using FluentAssertions;
using NUnit.Framework;

namespace LeapYear.Lib.Tests
{
    public class LeapYearCheckerTests
    {
        LeapYearChecker sut;

        [SetUp]
        public void Setup()
        {
            sut = new LeapYearChecker();
        }

        [Test]
        public void Should_throw_exception_for_negative_year()
        {
            Action act = () => sut.IsLeapYear(-100);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestCase(1900, false)]
        [TestCase(2001, false)]
        [TestCase(1996, true)]
        [TestCase(2000, true)]
        public void Should_define_is_year_leap_correctly(int year, bool expectedResult)
        {
            var actual = sut.IsLeapYear(year);

            actual.Should().Be(expectedResult);
        }
    }
}