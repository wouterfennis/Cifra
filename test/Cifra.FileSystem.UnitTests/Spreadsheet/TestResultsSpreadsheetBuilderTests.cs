using AutoFixture;
using Cifra.Domain;
using Cifra.Domain.ValueTypes;
using Cifra.FileSystem.Options;
using Cifra.FileSystem.Spreadsheet;
using Cifra.TestUtilities.Domain;
using Cifra.TestUtilities.SpreadsheetWriter;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.File;
using SpreadsheetWriter.Abstractions.Formula;
using SpreadsheetWriter.Test;
using System;
using System.Threading.Tasks;

namespace Cifra.FileSystem.UnitTests.Spreadsheet
{
    [TestClass]
    public class TestResultsSpreadsheetBuilderTests
    {
        private Fixture _fixture;
        private Mock<IOptions<SpreadsheetOptions>> _spreadsheetOptions;
        private SpreadsheetOptions _spreadsheetOptionsValues;
        private Mock<ISpreadsheetFileFactory> _spreadsheetFileFactory;
        private Mock<IFormulaBuilderFactory> _formulaBuilderFactory;
        private Mock<IFormulaBuilder> _formulaBuilder;
        private string[,] _spreadsheet;
        private TestResultsSpreadsheetBuilder _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _spreadsheetOptions = new Mock<IOptions<SpreadsheetOptions>>();
            _spreadsheetOptionsValues = new SpreadsheetOptions{ TestResultsDirectory = Path.CreateFromString(_fixture.Create<string>()).Value };

            _spreadsheetOptions
                .SetupGet(x => x.Value)
                .Returns(_spreadsheetOptionsValues);

            _spreadsheetFileFactory = new Mock<ISpreadsheetFileFactory>();
            _formulaBuilderFactory = new Mock<IFormulaBuilderFactory>();
            _formulaBuilder = new Mock<IFormulaBuilder>();
            _spreadsheet = new string[30, 30];

            SetupFormulaBuilderFactory();

            _sut = new TestResultsSpreadsheetBuilder(
                _spreadsheetOptions.Object,
                _spreadsheetFileFactory.Object,
                _formulaBuilderFactory.Object);
        }

        [TestMethod]
        public async Task CreateTestResultsSpreadsheetAsync_WithValidInput_CreatesSpreadsheet()
        {
            // Arrange
            var metadata = Domain.Spreadsheet.Metadata.TryCreate(_fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<DateTime>(), _fixture.Create<string>(), _fixture.Create<string>()).Value;
            SetupSpreadsheetFileBuilder();
            FormulaBuilderTestUtilities.SetupFormulaBuilder(_formulaBuilder, _fixture.Create<string>());

            Class @class = Class.TryCreate(_fixture.Create<string>()).Value;
            Test test = new TestBuilder()
                .WithNumberOfVersions(2)
                .WithMinimumGrade(1)
                .WithRandomAssignments()
                .Build();

            // Act
            await _sut.CreateTestResultsSpreadsheetAsync(@class, test, metadata);

            // Assert
            SpreadsheetTestUtilities.PrintArraySpreadsheet(_spreadsheet);
        }

        private void SetupFormulaBuilderFactory()
        {
            _formulaBuilderFactory.Setup(x => x.Create())
                .Returns(_formulaBuilder.Object);
        }

        private void SetupSpreadsheetFileBuilder()
        {
            var testSpreadsheetWriter = new ArrayContentSpreadsheetWriter(_spreadsheet);
            var spreadsheetFile = new Mock<ISpreadsheetFile>();
            spreadsheetFile
                .Setup(x => x.GetSpreadsheetWriter())
                .Returns(testSpreadsheetWriter);

            spreadsheetFile
                .Setup(x => x.SaveAsync())
                .ReturnsAsync(new SaveResult());

            _spreadsheetFileFactory
                .Setup(x => x.Create(_spreadsheetOptionsValues.TestResultsDirectory, It.IsAny<Metadata>()))
                .Returns(spreadsheetFile.Object);
        }
    }
}
