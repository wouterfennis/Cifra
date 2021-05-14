using AutoFixture;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.Spreadsheet;
using Cifra.TestUtilities.Application;
using Cifra.TestUtilities.SpreadsheetWriter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.File;
using SpreadsheetWriter.Abstractions.Formula;
using SpreadsheetWriter.Test;
using System.Threading.Tasks;

namespace Cifra.FileSystem.UnitTests.Spreadsheet
{
    [TestClass]
    public class TestResultsSpreadsheetBuilderTests
    {
        private Fixture _fixture;
        private Mock<IFileLocationProvider> _fileLocationProvider;
        private Mock<ISpreadsheetFileFactory> _spreadsheetFileFactory;
        private Mock<IFormulaBuilderFactory> _formulaBuilderFactory;
        private Mock<IFormulaBuilder> _formulaBuilder;
        private string[,] _spreadsheet;
        private TestResultsSpreadsheetBuilder _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _fileLocationProvider = new Mock<IFileLocationProvider>();
            _spreadsheetFileFactory = new Mock<ISpreadsheetFileFactory>();
            _formulaBuilderFactory = new Mock<IFormulaBuilderFactory>();
            _formulaBuilder = new Mock<IFormulaBuilder>();
            _spreadsheet = new string[30, 30];

            SetupFormulaBuilderFactory();

            _sut = new TestResultsSpreadsheetBuilder(
                _fileLocationProvider.Object,
                _spreadsheetFileFactory.Object,
                _formulaBuilderFactory.Object);
        }

        [TestMethod]
        public async Task CreateTestResultsSpreadsheetAsync_WithValidInput_CreatesSpreadsheet()
        {
            // Arrange
            var metadata = _fixture.Create<Application.Models.Spreadsheet.Metadata>();
            SetupSpreadsheetFileBuilder();
            FormulaBuilderTestUtilities.SetupFormulaBuilder(_formulaBuilder, _fixture.Create<string>());

            Class @class = _fixture.Create<Class>();
            Test test = new TestBuilder()
                .WithNumberOfVersions(2)
                .WithMinimumGrade(Grade.CreateFromByte(1))
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
            var path = _fixture.Create<Path>();
            _fileLocationProvider.Setup(x => x.GetSpreadsheetDirectoryPath())
                .Returns(path);

            var testSpreadsheetWriter = new ArrayContentSpreadsheetWriter(_spreadsheet);

            var spreadsheetFile = new Mock<ISpreadsheetFile>();
            spreadsheetFile.Setup(x => x.GetSpreadsheetWriter())
                .Returns(testSpreadsheetWriter);

            _spreadsheetFileFactory
                .Setup(x => x.Create(path.Value, It.IsAny<Metadata>()))
                .Returns(spreadsheetFile.Object);
        }
    }
}
