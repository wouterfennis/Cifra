using AutoFixture;
using Cifra.FileSystem.Mapping;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetWriter.Abstractions.File;
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

        [TestMethod]
        public void MapToModel_WithSaveResultNullInput_ThrowsException()
        {
            // Arrange
            SaveResult model = null;

            // Act
            Action action = () => model.MapToModel();

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void MapToModel_WithValidSaveResultLibraryModel_MapsToModel()
        {
            // Arrange
            SaveResult model = _fixture.Create<SaveResult>();

            // Act
            Domain.Spreadsheet.SaveResult result = model.MapToModel();

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().Be(model.IsSuccess);
            result.Exception.Should().Be(model.Exception);
        }
    }
}
