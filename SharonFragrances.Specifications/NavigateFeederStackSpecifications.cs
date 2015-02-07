using System;
using System.Runtime.InteropServices;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace SharonFragrances.Specifications
{
    [TestFixture]
    public class NavigateFeederStackSpecifications
    {
        [Test]
        public void CanQueryStackID()
        {
            //Arrange
            const string expectedPosition = "0";
            var stackFeeder = new StackFeeder();

            //Act
            var stackPostition = stackFeeder.Command("s");

            //Assert
            stackPostition.Should().Be(expectedPosition);
        }

        [Test]
        public void FeederExecutesMoveRightCommandHorizontalStackPositionIncrements()
        {
            //Arrange 
            const string expectedPosition = "1";
            var stackFeeder = new StackFeeder();

            //Act
            stackFeeder.Command("r");

            //Assert
            stackFeeder.Command("s").Should().Be(expectedPosition);
        }

        [Test]
        public void FeederExecutesTwoRightCommands()
        {
            //Arrange
            const string expectedPosition = "2";
            var stackFeeder = new StackFeeder();

            //Act
            var result = stackFeeder.Command("rrs");

            //Assert
            result.Should().Be(expectedPosition);
        }

        [Test]
        public void FeederExecutesFourRightCommands()
        {
            //Arrange
            const string expectedPosition = "4";

            //Act
            var stackFeeder = new StackFeeder();
            var result = stackFeeder.Command("rrrrs");

            //Assert
            result.Should().Be(expectedPosition);
        }

        [Test]
        public void FromStackFourResetToRestingPosition()
        {
            //Arrange
            const string expectedResult = "0";

            //Act
            var stackFeeder = new StackFeeder();
            var result = stackFeeder.Command("qs");

            //Assert
            result.Should().Be(expectedResult);
        }

        [Test]
        public void FeederResetsToRestingPosition()
        {
            //Arrange
            const string expectedResult = "0";
            var stackFeeder = new StackFeeder();
            stackFeeder.Command("r");

            //Act
            stackFeeder.Command("q");

            //Assert
            stackFeeder.Command("s").Should().Be(expectedResult);
        }

        [Test]
        public void FeederCanMoveDown()
        {
            //Arrange
            const string expectedResult = "1";
            var stackFeeder = new StackFeeder();
            stackFeeder.Command("r");

            //Act
            stackFeeder.Command("d");

            //Assert
            stackFeeder.Command("b").Should().Be(expectedResult);
        }

        [Test]
        public void FeederCanMoveUp()
        {
            //Arrange
            const string expectedResult = "1";
            var stackFeeder = new StackFeeder();
            stackFeeder.Command("r");
            stackFeeder.Command("dd");

            //Act
            stackFeeder.Command("u");

            //Assert
            stackFeeder.Command("b").Should().Be(expectedResult);
        }

        [Test]
        [ExpectedException("SharonFragrances.Specifications.FeederNotOnStack")]
        public void FeederCannotMoveDownBinIfNotOnStack()
        {
            //Arrange
            var stackFeeder = new StackFeeder();

            //Act
            stackFeeder.Command("d");
        }

        [Test]
        [ExpectedException("SharonFragrances.Specifications.FeederNotOnStack")]
        public void FeederCannotMoveUpBinIfNotOnStack()
        {
            //Arrange
            var stackFeeder = new StackFeeder();

            //Act
            stackFeeder.Command("u");
        }
    }



    public class StackFeeder : iStackFeeder
    {
        private int _stackPosition;
        private int _binPosition;

        public string Command(string command)
        {
            string result = String.Empty;

            foreach (var c in command)
            {
                if (c == 'r')
                {
                    _stackPosition = ++_stackPosition;
                }
                if (c == 's')
                {
                    result = _stackPosition.ToString();
                }
                if (c == 'q')
                {
                    _stackPosition = 0;
                }
                if (c == 'b')
                {
                    result = _binPosition.ToString();
                }
                if (c == 'd')
                {
                    if (_stackPosition != 0)
                    {
                        _binPosition = ++_binPosition;
                    }
                    else
                    {
                        throw new FeederNotOnStack();
                    }
                }
                if (c == 'u')
                {
                    if (_stackPosition != 0)
                    {
                        _binPosition = --_binPosition;
                    }
                    else
                    {
                        throw new FeederNotOnStack();
                    }
                }
            }
            return result;
        }
    }

    public class FeederNotOnStack : Exception
    {
    }
}
