using Cifra.Frontend.Models;
using FluentAssertions;

namespace Cifra.Frontend.Tests.Extensions
{
    [TestClass]
    public class LastNamePartsTests
    {
        [TestMethod]
        public void Create_WithDutchInfix_ReturnsInfixAndLastnameSeparate()
        {
            // Act
            var result = LastNameParts.Create("van der Berg");

            // Assert
            result.Infix.Should().Be("van der");
            result.LastName.Should().Be("Berg");
        }
    }
}
