using Cifra.Frontend.Models;
using FluentAssertions;

namespace Cifra.Frontend.Tests.Extensions
{
    [TestClass]
    public class LastNamePartsTests
    {
        [TestMethod]
        [DataRow("Berg")]
        [DataRow("y Lucientes")] 
        public void Create_WithNoDutchInfix_ReturnsInfixAndLastnameSeparate(string lastName)
        {
            // Act
            var result = LastNameParts.Create(lastName);

            // Assert
            result.Infix.Should().BeNull();
            result.LastName.Should().Be(lastName);
        }

        [TestMethod]
        public void Create_WithOneWordDutchInfix_ReturnsInfixAndLastnameSeparate()
        {
            // Act
            var result = LastNameParts.Create("van Berg");

            // Assert
            result.Infix.Should().Be("van");
            result.LastName.Should().Be("Berg");
        }

        [TestMethod]
        [DataRow("van der Berg", "van der", "Berg")]
        [DataRow("De La Fontaine", "De La", "Fontaine")]
        public void Create_WithTwoWordsDutchInfix_ReturnsInfixAndLastnameSeparate(string completeLastName, string expectedInfix, string expectedLastName)
        {
            // Act
            var result = LastNameParts.Create(completeLastName);

            // Assert
            result.Infix.Should().Be(expectedInfix);
            result.LastName.Should().Be(expectedLastName);
        }
    }
}
