using AutoFixture;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.Spreadsheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Test;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Cifra.FileSystem.UnitTests.Spreadsheet
{
    [TestClass]
    public class TestResultsSpreadsheetBuilderTests
    {
        private Fixture _fixture;
        private Mock<IFileLocationProvider> _fileLocationProvider;
        private Mock<IDirectoryInfoWrapperFactory> _directoryInfoWrapperFactory;
        private Mock<IFileInfoWrapperFactory> _fileInfoWrapperFactory;
        private Mock<ISpreadsheetFileBuilder> _spreadsheetFileBuilder;
        private string[,] _worksheet;
        private TestResultsSpreadsheetBuilder _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _fileLocationProvider = new Mock<IFileLocationProvider>();
            _directoryInfoWrapperFactory = new Mock<IDirectoryInfoWrapperFactory>();
            _fileInfoWrapperFactory = new Mock<IFileInfoWrapperFactory>();
            _spreadsheetFileBuilder = new Mock<ISpreadsheetFileBuilder>();
            _worksheet = new string[50, 50];

            _sut = new TestResultsSpreadsheetBuilder(
                _fileLocationProvider.Object,
                _directoryInfoWrapperFactory.Object,
                _fileInfoWrapperFactory.Object,
                _spreadsheetFileBuilder.Object
                );
        }

        [TestMethod]
        public async Task dfAsync()
        {
            // Arrange
            SetupSpreadsheetDirectory();
            SetupSpreadsheetFileBuilder();

            var @class = _fixture.Create<Class>();
            var test = _fixture.Create<Test>();
            var metadata = _fixture.Create<Application.Models.Spreadsheet.Metadata>();

            // Act
            await _sut.CreateTestResultsSpreadsheetAsync(@class, test, metadata);

            // Assert

        }
        private string SetupSpreadsheetDirectory()
        {
            var path = _fixture.Create<Path>();
            _fileLocationProvider.Setup(x => x.GetSpreadsheetDirectoryPath())
                .Returns(path);

            var spreadsheetDirectory = _fixture.Create<string>();
            var directoryWrapper = new Mock<IDirectoryInfoWrapper>();
            directoryWrapper
                .SetupGet(x => x.FullName)
                .Returns(spreadsheetDirectory);

            _directoryInfoWrapperFactory.Setup(x => x.Create(path))
                .Returns(directoryWrapper.Object);

            return spreadsheetDirectory;
        }

        private void SetupSpreadsheetFileBuilder(string spreadsheetDirectory, string fileName)
        {
            FileInfo fileInfo = null;
            var fileInfoWrapper = new Mock<IFileInfoWrapper>();
            fileInfoWrapper
                .Setup( x=> x.GetFileInfo())
                .Returns(fileInfo);
            var expectedSpreadsheetFile = System.IO.Path.Combine(spreadsheetDirectory, $"{fileName}.xlsx");
            _fileInfoWrapperFactory
                .Setup(x => x.Create(It.Is<Application.Models.ValueTypes.Path>(x => x.Value == expectedSpreadsheetFile)))
                .Returns(fileInfoWrapper.Object);

            var testSpreadsheetWriter = new ArraySpreadsheetWriter(_worksheet);

            _spreadsheetFileBuilder
                .Setup(x => x.CreateSpreadsheetWriter(fileName))
                .Returns(testSpreadsheetWriter);

            _spreadsheetFileBuilder
                .Setup(x => x.CreateNew(fileInfo))
                .Returns(_spreadsheetFileBuilder.Object);
        }
    }
}
