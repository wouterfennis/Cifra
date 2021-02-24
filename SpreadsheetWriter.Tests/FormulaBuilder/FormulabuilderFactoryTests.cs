using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetWriter.Abstractions.Formula;
using SpreadsheetWriter.EPPlus.Formula;

namespace SpreadsheetWriter.EPPlus.UnitTests.FormulaBuilder
{
    [TestClass]
    public class FormulabuilderFactoryTests
    {
        [TestMethod]
        public void Create_WithInstanceOfFactory_ReturnsInstanceOfForumulaBuilder()
        {
            // Arrange
            var factory = new FormulaBuilderFactory();

            // Act
            IFormulaBuilder result = factory.Create();

            // Assert
            result.Should().BeOfType<EPPlus.Formula.FormulaBuilder>();
        }
    }
}
