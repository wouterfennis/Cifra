using AutoFixture;
using Cifra.FileSystem.Mapping;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cifra.FileSystem.UnitTests.Mapping
{
    [TestClass]
    public class SpreadsheetMappingTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void MapToLibraryModel_WithMetadataNullInput_ThrowsException()
        {
            // Arrange
            Domain.Spreadsheet.Metadata model = null;

            // Act
            Action action = () => model.MapToLibraryModel();

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void MapToLibraryModel_WithValidMetadataModel_MapsToLibraryModel()
        {
            // Arrange
            Domain.Spreadsheet.Metadata model = _fixture.Create<Domain.Spreadsheet.Metadata>();

            // Act
            SpreadsheetWriter.Abstractions.Metadata result = model.MapToLibraryModel();

            // Assert
            result.Should().NotBeNull();
            result.Author.Should().Be(model.Author);
            result.Created.Should().Be(model.Created);
            result.FileName.Should().Be(model.FileName);
            result.Subject.Should().Be(model.Subject);
            result.Title.Should().Be(model.Title);
            result.ApplicationVersion.Should().Be(model.ApplicationVersion);
        }
    }
}
