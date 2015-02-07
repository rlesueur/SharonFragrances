using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace SharonFragrances.Specifications
{
    [TestFixture]
    public class NavigateFeederStackSpecifications
    {
        [Test]
        public void CanIQueryStackID()
        {
            //arrange
            const int expectedPosition = 0;
            var stackFeeder = Substitute.For<iStackFeeder>();
            stackFeeder.CurrentStackPosition.Returns(expectedPosition);

            //act
            var stackPostition = stackFeeder.CurrentStackPosition;

            //assert
            stackPostition.Should().Be(expectedPosition);
        }
    }
}
