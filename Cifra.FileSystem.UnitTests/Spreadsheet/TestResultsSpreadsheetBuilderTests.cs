using AutoFixture;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.Spreadsheet;
using Cifra.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpreadsheetWriter.Abstractions;
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
        private string[,] _worksheet;
        private TestResultsSpreadsheetBuilder _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _fileLocationProvider = new Mock<IFileLocationProvider>();
            _spreadsheetFileFactory = new Mock<ISpreadsheetFileFactory>();
            _formulaBuilderFactory = new Mock<IFormulaBuilderFactory>();
            _worksheet = new string[20, 20];

            _sut = new TestResultsSpreadsheetBuilder(
                _fileLocationProvider.Object,
                _spreadsheetFileFactory.Object,
                _formulaBuilderFactory.Object);
        }

        [TestMethod]
        public async Task dfAsync()
        {
            // Arrange
            var metadata = _fixture.Create<Application.Models.Spreadsheet.Metadata>();
            SetupSpreadsheetFileBuilder();

            Class @class = _fixture.Create<Class>();
            Test test = new TestBuilder()
                .WithMinimumGrade(Grade.CreateFromByte(1))
                .WithRandomAssignments()
                .Build();

            // Act
            await _sut.CreateTestResultsSpreadsheetAsync(@class, test, metadata);

            // Assert
            WorksheetTestUtilities.PrintArrayWorksheet(_worksheet);
        }

        private void SetupSpreadsheetFileBuilder()
        {
            var path = _fixture.Create<Path>();
            _fileLocationProvider.Setup(x => x.GetSpreadsheetDirectoryPath())
                .Returns(path);

            var testSpreadsheetWriter = new ArraySpreadsheetWriter(_worksheet);

            var spreadsheetFile = new Mock<ISpreadsheetFile>();
            spreadsheetFile.Setup(x => x.GetSpreadsheetWriter())
                .Returns(testSpreadsheetWriter);

            _spreadsheetFileFactory
                .Setup(x => x.Create(path.Value, It.IsAny<Metadata>()))
                .Returns(spreadsheetFile.Object);
        }
    }
}
