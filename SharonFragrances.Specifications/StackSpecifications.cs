using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SharonFragrances.API;

namespace SharonFragrances.Specifications
{
    [TestFixture]
    public class StackSpecifications
    {
        [Ignore("BM 08022015 hived off to create concept of stack and coupling" )]
        [TestCase("rdddddi", Result = "c54675890")]
        [TestCase("rdddddi", Result = "c54675890")]
        [TestCase("rdddddi", Result = "c54675890")]
        [TestCase("rdddddi", Result = "c54675890")]
        [TestCase("rdddddi", Result = "c54675890")]
        public void CanInitialiseStackFromString(string commandSequence)
        {
            //Arrange
            const string expectedResult = "c897865";
            const string stock = "1:4:c54675890:10;1:8:c7867546:5;1:5:c897865:100;1:5:c786765:3;1:5:c445673:10";
            var stacks = new List<IStack> { new Stack(), new Stack(), new Stack(), new Stack() };
            var stackFeeder = new StackFeeder(stacks, stock);

            //Act
            var result = stackFeeder.Command("rdddddi");

            //Assert
            result.Should().Be(expectedResult);
        }

        [Test]
        public void CanLoadItemInToAStackAtBinPosition()
        {
            //Arrange
            const string expectedProductCode = "c54675890";
            const string stock = "1:5:c54675890:10";
            var stacks = new List<IStack> {new Stack(), new Stack(), new Stack(), new Stack()};

            //Act
            var stackFeeder = new StackFeeder(stacks, stock);
            var result = stackFeeder.Command("rdddddi");
            
            //Assert
            result.Should().Be(expectedProductCode);
        }
    }
}
