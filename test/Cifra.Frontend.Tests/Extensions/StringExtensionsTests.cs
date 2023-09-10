using Cifra.Frontend.Extensions;
using FluentAssertions;

namespace Cifra.Frontend.Tests.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        [DataRow(null, null)]
        [DataRow("",  "")]
        [DataRow("string",  "String")]
        [DataRow("stringString",  "String String")]
        public void ToDisplayFormat_WithInput_ReturnsCorrectOutput(string input, string expectedOutput)
        {
            // Act
            var result = input.ToDisplayFormat();

            // Assert
            result.Should().Be(expectedOutput);
        }
    }
}
