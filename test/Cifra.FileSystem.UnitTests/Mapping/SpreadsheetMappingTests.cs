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
        public void MapToLibraryModel_WithNullInput_ThrowsException()
        {
            Application.Models.Spreadsheet.Metadata model = null;

            Action action = () => model.MapToLibraryModel();

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void MapToLibraryModel_WithValidModel_MapsToLibraryModel()
        {
            Application.Models.Spreadsheet.Metadata model = _fixture.Create<Application.Models.Spreadsheet.Metadata>();

            var result = model.MapToLibraryModel();

            result.Should().NotBeNull();
            result.Author.Should().Be(model.Author);
            result.Created.Should().Be(model.Created);
            result.FileName.Should().Be(model.FileName);
            result.Subject.Should().Be(model.Subject);
            result.Title.Should().Be(model.Title);
        }
    }
}
