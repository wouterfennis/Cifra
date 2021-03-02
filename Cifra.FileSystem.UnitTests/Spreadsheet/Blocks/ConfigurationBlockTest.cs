using System.Drawing;
using AutoFixture;
using Cifra.Application.Models.ValueTypes;
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
        private Fixture _fixture;
        private ArraySpreadsheetWriter _spreadsheetWriter;

        [TestInitialize]
        public void Initialize()
        {
            spreadsheet = new string[10, 10];
            _startpoint = new Point(0, 0);
            _fixture = new Fixture();
            _spreadsheetWriter = new ArraySpreadsheetWriter(spreadsheet);
        }

        [TestMethod]
        public void Write_WithConfiguration_PutsTitleOnRightPosition()
        {
            // Arrange
            var standardizationFactor = _fixture.Create<StandardizationFactor>();
            var minimumGrade = Grade.CreateFromByte(1);
            var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, standardizationFactor, minimumGrade);
            var sut = new ConfigurationBlock(configurationBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            spreadsheet[0, 0].Should().Be("Configuratie");
        }

        [TestMethod]
        public void Write_WithStandardizationFactor_PutsDataOnRightPosition()
        {
            // Arrange
            var expectedStandardizationFactor = _fixture.Create<StandardizationFactor>();
            var minimumGrade = Grade.CreateFromByte(1);
            var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, expectedStandardizationFactor, minimumGrade);
            var sut = new ConfigurationBlock(configurationBlockInput);

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
            var standardizationFactor = _fixture.Create<StandardizationFactor>();
            var minimumGrade = Grade.CreateFromByte(1);
            var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, standardizationFactor, minimumGrade);
            var sut = new ConfigurationBlock(configurationBlockInput);

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
            var standardizationFactor = _fixture.Create<StandardizationFactor>();
            var expectedMinimumGrade = Grade.CreateFromByte(1);
            var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, standardizationFactor, expectedMinimumGrade);
            var sut = new ConfigurationBlock(configurationBlockInput);

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
            var standardizationFactor = _fixture.Create<StandardizationFactor>();
            var minimumGrade = Grade.CreateFromByte(1);
            var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, standardizationFactor, minimumGrade);
            var sut = new ConfigurationBlock(configurationBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            sut.MinimumGradePosition.X.Should().Be(1);
            sut.MinimumGradePosition.Y.Should().Be(2);
        }
    }
}
