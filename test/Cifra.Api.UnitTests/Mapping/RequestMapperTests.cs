using AutoFixture;
using Cifra.Api.Mapping;
using Cifra.Api.V1.Models.Class.Requests;
using Cifra.Api.V1.Models.Spreadsheet.Requests;
using Cifra.Api.V1.Models.Test.Requests;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cifra.Api.UnitTests.Mapping
{
    [TestClass]
    public class RequestMapperTests
    {
        private Fixture _fixture = default!;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void MapToCommand_WithCreateClassRequest_MapsToOutput()
        {
            // Arrange
            var input = _fixture.Create<CreateClassRequest>();

            // Act
            var result = input.MapToCommand();

            // Assert
            result.Name.Should().Be(input.Name);
        }

        [TestMethod]
        public void MapToCommand_WithCreateTestRequest_MapsToOutput()
        {
            // Arrange
            var input = _fixture.Create<CreateTestRequest>();

            // Act
            var result = input.MapToCommand();

            // Assert
            result.Name.Should().Be(input.Name);
            result.MinimumGrade.Should().Be(input.MinimumGrade);
            result.NumberOfVersions.Should().Be(input.NumberOfVersions);
            result.StandardizationFactor.Should().Be(input.StandardizationFactor);
        }

        [TestMethod]
        public void MapToCommand_WithCreateTestResultsSpreadsheetRequest_MapsToOutput()
        {
            // Arrange
            var input = _fixture.Create<CreateTestResultsSpreadsheetRequest>();

            // Act
            var result = input.MapToCommand();

            // Assert
            result.ClassId.Should().Be(input.ClassId);
            result.TestId.Should().Be(input.TestId);
            result.Metadata.ApplicationVersion.Should().Be(input.Metadata.ApplicationVersion);
            result.Metadata.Author.Should().Be(input.Metadata.Author);
            result.Metadata.Subject.Should().Be(input.Metadata.Subject);
            result.Metadata.Title.Should().Be(input.Metadata.Title);
            result.Metadata.Created.Should().Be(input.Metadata.Created);
            result.Metadata.FileName.Should().Be(input.Metadata.FileName);
        }
    }
}
