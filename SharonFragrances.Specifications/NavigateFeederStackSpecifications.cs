using System.Collections.Generic;
using System.Runtime.InteropServices;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SharonFragrances.API;

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
            var stacks = new List<IStack> { new Stack(), new Stack(), new Stack(), new Stack() };
            var stackFeeder = new StackFeeder(stacks, string.Empty);

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
            var stacks = new List<IStack> { new Stack(), new Stack(), new Stack(), new Stack() };
            var stackFeeder = new StackFeeder(stacks, string.Empty);

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
            var stacks = new List<IStack> { new Stack(), new Stack(), new Stack(), new Stack() };
            var stackFeeder = new StackFeeder(stacks, string.Empty);

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
            var stacks = new List<IStack> { new Stack(), new Stack(), new Stack(), new Stack() };
            var stackFeeder = new StackFeeder(stacks, string.Empty);
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
            var stacks = new List<IStack> { new Stack(), new Stack(), new Stack(), new Stack() };
            var stackFeeder = new StackFeeder(stacks, string.Empty);
            var result = stackFeeder.Command("qs");

            //Assert
            result.Should().Be(expectedResult);
        }

        [Test]
        public void FeederResetsToRestingPosition()
        {
            //Arrange
            const string expectedResult = "0";
            var stacks = new List<IStack> { new Stack(), new Stack(), new Stack(), new Stack() };
            var stackFeeder = new StackFeeder(stacks, string.Empty);
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
            var stacks = new List<IStack> { new Stack(), new Stack(), new Stack(), new Stack() };
            var stackFeeder = new StackFeeder(stacks, string.Empty);
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
            var stacks = new List<IStack> { new Stack(), new Stack(), new Stack(), new Stack() };
            var stackFeeder = new StackFeeder(stacks, string.Empty);
            stackFeeder.Command("r");
            stackFeeder.Command("dd");

            //Act
            stackFeeder.Command("u");

            //Assert
            stackFeeder.Command("b").Should().Be(expectedResult);
        }

        [Test]
        [ExpectedException("SharonFragrances.API.FeederNotOnStack")]
        public void FeederCannotMoveDownBinIfNotOnStack()
        {
            //Arrange
            var stacks = new List<IStack> { new Stack(), new Stack(), new Stack(), new Stack() };
            var stackFeeder = new StackFeeder(stacks, string.Empty);

            //Act
            stackFeeder.Command("d");
        }

        [Test]
        [ExpectedException("SharonFragrances.API.FeederNotOnStack")]
        public void FeederCannotMoveUpBinIfNotOnStack()
        {
            //Arrange
            var stacks = new List<IStack> { new Stack(), new Stack(), new Stack(), new Stack() };
            var stackFeeder = new StackFeeder(stacks, string.Empty);

            //Act
            stackFeeder.Command("u");
        }

        [Test]
        [Ignore ("RLS 08022015 2047 halted to create stack")]
        public void FeederCanRetrieveItemCode()
        {
            //Arrange
            const string expectedResult = "c54675890";
            var stacks = new List<IStack> { new Stack(), new Stack(), new Stack(), new Stack() };
            var stackFeeder = new StackFeeder(stacks, string.Empty);

            //Act
            var result = stackFeeder.Command("rdddddi");

            //Assert
            result.Should().Be(expectedResult);
        }

    }
}
