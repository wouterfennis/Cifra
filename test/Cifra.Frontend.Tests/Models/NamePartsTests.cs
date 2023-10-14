using Cifra.Frontend.Models;
using FluentAssertions;

namespace Cifra.Frontend.Tests.Extensions
{
    [TestClass]
    public class NamePartsTests
    {
        [TestMethod]
        [DataRow("Luuk Berg", "Luuk", "Berg")]
        [DataRow("Perez y Lucientes", "Perez", "y Lucientes")] 
        public void Create_WithNoDutchInfix_ReturnsFirstnameAndLastnameSeparate(string completeName, string expectedFirstName, string expectedLastName)
        {
            // Act
            var result = NameParts.Create(completeName);

            // Assert
            result.FirstName.Should().Be(expectedFirstName);
            result.Infix.Should().BeNull();
            result.LastName.Should().Be(expectedLastName);
        }

        [TestMethod]
        [DataRow("Luuk van Berg", "Luuk", "van", "Berg")]
        [DataRow("Van van Berg", "Van", "van", "Berg")]
        public void Create_WithOneWordDutchInfix_ReturnsFirstnameInfixAndLastnameSeparate(string completeLastName, string expectedFirstName, string expectedInfix, string expectedLastName)
        {
            // Act
            var result = NameParts.Create(completeLastName);

            // Assert
            result.FirstName.Should().Be(expectedFirstName);
            result.Infix.Should().Be(expectedInfix);
            result.LastName.Should().Be(expectedLastName);
        }

        [TestMethod]
        [DataRow("Luuk van der Berg", "Luuk", "van der", "Berg")]
        [DataRow("Louise De La Fontaine", "Louise", "De La", "Fontaine")]
        public void Create_WithTwoWordsDutchInfix_ReturnsInfixAndLastnameSeparate(string completeLastName, string expectedFirstName, string expectedInfix, string expectedLastName)
        {
            // Act
            var result = NameParts.Create(completeLastName);

            // Assert
            result.FirstName.Should().Be(expectedFirstName);
            result.Infix.Should().Be(expectedInfix);
            result.LastName.Should().Be(expectedLastName);
        }
    }
}
