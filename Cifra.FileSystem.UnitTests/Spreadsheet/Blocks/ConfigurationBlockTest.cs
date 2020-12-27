using AutoFixture;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.Spreadsheet.Blocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetWriter.Test;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Cifra.FileSystem.UnitTests.Spreadsheet.Blocks
{
    [TestClass]
    public class ConfigurationBlockTest
    {
        private string[,] _worksheet;
        private Point _startpoint;
        private Fixture _fixture;
        private ArraySpreadsheetWriter _spreadsheetWriter;

        [TestInitialize]
        public void Initialize()
        {
            _worksheet = new string[10, 10];
            _startpoint = new Point(0, 0);
            _fixture = new Fixture();
            _spreadsheetWriter = new ArraySpreadsheetWriter(_worksheet);
        }

        [TestMethod]
        public void Write_WithConfiguration_PutsTitleOnRightPosition()
        {
            // Arrange
            var maximumPoints = _fixture.Create<decimal>();
            var standardizationFactor = _fixture.Create<StandardizationFactor>();
            var minimumGrade = Grade.CreateFromByte(1);
            var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, maximumPoints, standardizationFactor, minimumGrade);
            var sut = new ConfigurationBlock(configurationBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            _worksheet[0, 0].Should().Be("Configuratie");
        }

        [TestMethod]
        public void Write_WithMaximumPoints_PutsDataOnRightPosition()
        {
            // Arrange
            var expectedMaximumPoints = _fixture.Create<decimal>();
            var standardizationFactor = _fixture.Create<StandardizationFactor>();
            var minimumGrade = Grade.CreateFromByte(1);
            var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, expectedMaximumPoints, standardizationFactor, minimumGrade);
            var sut = new ConfigurationBlock(configurationBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            WorksheetTestUtilities.PrintArrayWorksheet(_worksheet);
            _worksheet[0, 1].Should().Be("Maximale punten");
            _worksheet[1, 1].Should().Be(expectedMaximumPoints.ToString());
        }

        [TestMethod]
        public void Write_WithMaximumPoints_SavesPositionOfMaximumPoints()
        {
            // Arrange
            var maximumPoints = _fixture.Create<decimal>();
            var standardizationFactor = _fixture.Create<StandardizationFactor>();
            var minimumGrade = Grade.CreateFromByte(1);
            var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, maximumPoints, standardizationFactor, minimumGrade);
            var sut = new ConfigurationBlock(configurationBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            sut.MaximumPointsPosition.X.Should().Be(1);
            sut.MaximumPointsPosition.Y.Should().Be(1);
        }

        [TestMethod]
        public void Write_WithStandardizationFactor_PutsDataOnRightPosition()
        {
            // Arrange
            var maximumPoints = _fixture.Create<decimal>();
            var expectedStandardizationFactor = _fixture.Create<StandardizationFactor>();
            var minimumGrade = Grade.CreateFromByte(1);
            var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, maximumPoints, expectedStandardizationFactor, minimumGrade);
            var sut = new ConfigurationBlock(configurationBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            _worksheet[0, 2].Should().Be("Normering");
            _worksheet[1, 2].Should().Be(expectedStandardizationFactor.Value.ToString());
        }

        [TestMethod]
        public void Write_WithStandardizationFactor_SavesPositionOfStandardizationFactor()
        {
            // Arrange
            var maximumPoints = _fixture.Create<decimal>();
            var standardizationFactor = _fixture.Create<StandardizationFactor>();
            var minimumGrade = Grade.CreateFromByte(1);
            var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, maximumPoints, standardizationFactor, minimumGrade);
            var sut = new ConfigurationBlock(configurationBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            sut.StandardizationfactorPosition.X.Should().Be(1);
            sut.StandardizationfactorPosition.Y.Should().Be(2);
        }

        [TestMethod]
        public void Write_WithMinimumGrade_PutsDataOnRightPosition()
        {
            // Arrange
            var maximumPoints = _fixture.Create<decimal>();
            var standardizationFactor = _fixture.Create<StandardizationFactor>();
            var expectedMinimumGrade = Grade.CreateFromByte(1);
            var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, maximumPoints, standardizationFactor, expectedMinimumGrade);
            var sut = new ConfigurationBlock(configurationBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            _worksheet[0, 3].Should().Be("Minimale cijfer");
            _worksheet[1, 3].Should().Be(expectedMinimumGrade.Value.ToString());
        }

        [TestMethod]
        public void Write_WithMinimumGrade_SavesPositionOfMinimumGrade()
        {
            // Arrange
            var maximumPoints = _fixture.Create<decimal>();
            var standardizationFactor = _fixture.Create<StandardizationFactor>();
            var minimumGrade = Grade.CreateFromByte(1);
            var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, maximumPoints, standardizationFactor, minimumGrade);
            var sut = new ConfigurationBlock(configurationBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            sut.MinimumGradePosition.X.Should().Be(1);
            sut.MinimumGradePosition.Y.Should().Be(3);
        }
    }
}
