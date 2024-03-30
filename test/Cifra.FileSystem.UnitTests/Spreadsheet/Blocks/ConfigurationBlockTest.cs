using System.Drawing;
using Cifra.Domain.ValueTypes;
using Cifra.FileSystem.Spreadsheet.Blocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetWriter.Test;

namespace Cifra.FileSystem.UnitTests.Spreadsheet.Blocks
{
    [TestClass]
    public class ConfigurationBlockTest
    {
        private string[,] spreadsheet;
        private Point _startpoint;
        private ArrayContentSpreadsheetWriter _spreadsheetWriter;

        [TestInitialize]
        public void Initialize()
        {
            spreadsheet = new string[10, 10];
            _startpoint = new Point(0, 0);
            _spreadsheetWriter = new ArrayContentSpreadsheetWriter(spreadsheet);
        }

        [TestMethod]
        public void Write_WithConfiguration_PutsTitleOnRightPosition()
        {
            // Arrange
            var standardizationFactor = StandardizationFactor.CreateFromInteger(9).Value;
            var minimumGrade = Grade.CreateFromInteger(1).Value;
            var satisfactoryGrade = Grade.CreateFromDecimal(5.5m).Value;
            var sut = new ConfigurationBlock(_startpoint, standardizationFactor, minimumGrade, satisfactoryGrade);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            spreadsheet[0, 0].Should().Be("Configuratie");
        }

        [TestMethod]
        public void Write_WithStandardizationFactor_PutsDataOnRightPosition()
        {
            // Arrange
            var expectedStandardizationFactor = StandardizationFactor.CreateFromInteger(9).Value;
            var minimumGrade = Grade.CreateFromInteger(1).Value;
            var satisfactoryGrade = Grade.CreateFromDecimal(5.5m).Value;

            var sut = new ConfigurationBlock(_startpoint, expectedStandardizationFactor, minimumGrade, satisfactoryGrade);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            spreadsheet[0, 1].Should().Be("Normering");
            spreadsheet[1, 1].Should().Be(expectedStandardizationFactor.Value.ToString());
        }

        [TestMethod]
        public void Write_WithStandardizationFactor_SavesPositionOfStandardizationFactor()
        {
            // Arrange
            var standardizationFactor = StandardizationFactor.CreateFromInteger(9).Value;
            var minimumGrade = Grade.CreateFromInteger(1).Value;
            var satisfactoryGrade = Grade.CreateFromDecimal(5.5m).Value;

            var sut = new ConfigurationBlock(_startpoint, standardizationFactor, minimumGrade, satisfactoryGrade);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            sut.StandardizationfactorPosition.X.Should().Be(1);
            sut.StandardizationfactorPosition.Y.Should().Be(1);
        }

        [TestMethod]
        public void Write_WithMinimumGrade_PutsDataOnRightPosition()
        {
            // Arrange
            var standardizationFactor = StandardizationFactor.CreateFromInteger(9).Value;
            var expectedMinimumGrade = Grade.CreateFromInteger(1).Value;
            var satisfactoryGrade = Grade.CreateFromDecimal(5.5m).Value;

            var sut = new ConfigurationBlock(_startpoint, standardizationFactor, expectedMinimumGrade, satisfactoryGrade);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            spreadsheet[0, 2].Should().Be("Minimale cijfer");
            spreadsheet[1, 2].Should().Be(expectedMinimumGrade.Value.ToString());
        }

        [TestMethod]
        public void Write_WithMinimumGrade_SavesPositionOfMinimumGrade()
        {
            // Arrange
            var standardizationFactor = StandardizationFactor.CreateFromInteger(9).Value;
            var minimumGrade = Grade.CreateFromInteger(1).Value;
            var satisfactoryGrade = Grade.CreateFromDecimal(5.5m).Value;

            var sut = new ConfigurationBlock(_startpoint, standardizationFactor, minimumGrade, satisfactoryGrade);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            sut.MinimumGradePosition.X.Should().Be(1);
            sut.MinimumGradePosition.Y.Should().Be(2);
        }

        [TestMethod]
        public void Write_WithSatisfactoryGrade_PutsDataOnRightPosition()
        {
            // Arrange
            var standardizationFactor = StandardizationFactor.CreateFromInteger(9).Value;
            var minimumGrade = Grade.CreateFromInteger(1).Value;
            var expectedSatisfactoryGrade = Grade.CreateFromDecimal(5.5m).Value;
            var sut = new ConfigurationBlock(_startpoint, standardizationFactor, minimumGrade, expectedSatisfactoryGrade);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            spreadsheet[0, 3].Should().Be("Voldoende cijfer");
            spreadsheet[1, 3].Should().Be(expectedSatisfactoryGrade.Value.ToString());
        }

        [TestMethod]
        public void Write_WithSatisfactoryGrade_SavesPositionOfMinimumGrade()
        {
            // Arrange
            var standardizationFactor = StandardizationFactor.CreateFromInteger(9).Value;
            var minimumGrade = Grade.CreateFromInteger(1).Value;
            var satisfactoryGrade = Grade.CreateFromDecimal(5.5m).Value;

            var sut = new ConfigurationBlock(_startpoint, standardizationFactor, minimumGrade, satisfactoryGrade);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            sut.MinimumGradePosition.X.Should().Be(1);
            sut.MinimumGradePosition.Y.Should().Be(2);
        }
    }
}
