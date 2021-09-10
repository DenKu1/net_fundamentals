using System;

using FluentAssertions;
using NUnit.Framework;

namespace MineField.Lib.Tests
{
    public class MineFieldProcessorTests
    {
        MineFieldProcessor sut;

        [SetUp]
        public void Setup()
        {
            sut = new MineFieldProcessor();
        }

        [Test]
        public void Should_throw_exception_for_null_mine_field()
        {
            Action act = () => sut.GetHintField(null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_exception_for_empty_mine_field()
        {
            Action act = () => sut.GetHintField(new char[0, 0]);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Should_throw_exception_for_mine_field_with_invalid_characters()
        {
            var mineField = new[,] {
                { 'a', '0', '.' }, 
                { '&', '-', ';' }
            };

            Action act = () => sut.GetHintField(mineField);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Should_return_valid_hint_field()
        {
            var mineField = new[,] {
                { '*', '.', '.', '.' }, 
                { '.', '.', '*', '.' },
                { '.', '.', '.', '.' } 
            };

            var hintField = new[,] {
                { '*', '2', '1', '1' },
                { '1', '2', '*', '1' },
                { '0', '1', '1', '1' }
            };

            var actualHintField = sut.GetHintField(mineField);

            actualHintField.Should().BeEquivalentTo(hintField);
        }
    }
}