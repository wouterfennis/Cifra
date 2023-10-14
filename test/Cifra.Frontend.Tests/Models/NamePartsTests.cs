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
            var isSuccess = NameParts.TryCreate(completeName, out NameParts? nameParts);

            // Assert
            isSuccess.Should().BeTrue();
            nameParts.Should().NotBeNull();
            nameParts!.FirstName.Should().Be(expectedFirstName);
            nameParts.Infix.Should().BeNull();
            nameParts.LastName.Should().Be(expectedLastName);
        }

        [TestMethod]
        [DataRow("Luuk van Berg", "Luuk", "van", "Berg")]
        [DataRow("Van van Berg", "Van", "van", "Berg")]
        public void Create_WithOneWordDutchInfix_ReturnsFirstnameInfixAndLastnameSeparate(string completeName, string expectedFirstName, string expectedInfix, string expectedLastName)
        {
            // Act
            var isSuccess = NameParts.TryCreate(completeName, out NameParts? nameParts);

            // Assert
            isSuccess.Should().BeTrue();
            nameParts.Should().NotBeNull();
            nameParts!.FirstName.Should().Be(expectedFirstName);
            nameParts.Infix.Should().Be(expectedInfix);
            nameParts.LastName.Should().Be(expectedLastName);
        }

        [TestMethod]
        [DataRow("Luuk van der Berg", "Luuk", "van der", "Berg")]
        [DataRow("Louise De La Fontaine", "Louise", "De La", "Fontaine")]
        public void Create_WithTwoWordsDutchInfix_ReturnsInfixAndLastnameSeparate(string completeName, string expectedFirstName, string expectedInfix, string expectedLastName)
        {
            // Act
            var isSuccess = NameParts.TryCreate(completeName, out NameParts? nameParts);

            // Assert
            isSuccess.Should().BeTrue();
            nameParts.Should().NotBeNull();
            nameParts!.FirstName.Should().Be(expectedFirstName);
            nameParts.Infix.Should().Be(expectedInfix);
            nameParts.LastName.Should().Be(expectedLastName);
        }

        [TestMethod]
        [DataRow("Luuk")]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void Create_WithInvalidName_ReturnsFalse(string completeName)
        {
            // Act
            var isSuccess = NameParts.TryCreate(completeName, out NameParts? nameParts);

            // Assert
            isSuccess.Should().BeFalse();
            nameParts.Should().BeNull();
        }
    }
}
