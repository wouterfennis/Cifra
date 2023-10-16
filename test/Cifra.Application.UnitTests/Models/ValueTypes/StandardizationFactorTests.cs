using Cifra.Domain.ValueTypes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cifra.Application.UnitTests.Models.ValueTypes
{
    [TestClass]
    public class StandardizationFactorTests
    {
        [TestMethod]
        public void CreateFromInteger_WithValidStandardizationFactor_ReturnsStandardizationFactor()
        {
            int input = 10;

            var result = StandardizationFactor.CreateFromInteger(input);

            result.Value.Should().Be(input);
        }

        [TestMethod]
        public void ImplicitFromStandardizationFactorToInt_WithValidValue_ConvertsToInt()
        {
            // Arrange
            StandardizationFactor input = StandardizationFactor.CreateFromInteger(10).Value;

            // Act
            int value = input;

            // Assert
            value.Should().Be(input.Value);
        }
    }
}
