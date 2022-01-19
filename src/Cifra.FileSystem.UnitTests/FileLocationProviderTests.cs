using AutoFixture;
using Cifra.Core.Models.ValueTypes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cifra.FileSystem.UnitTests
{
    [TestClass]
    public class FileLocationProviderTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void GetClassRepositoryPath_PreviousSetPath_ReturnsPath()
        {
            // Arrange
            Path expectedPath = _fixture.Create<Path>();
            var sut = new FileLocationProvider(expectedPath, null, null, null);

            // Act
            Path result = sut.GetClassRepositoryPath();

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(expectedPath);
        }


        [TestMethod]
        public void GetTestRepositoryPath_PreviousSetPath_ReturnsPath()
        {
            // Arrange
            Path expectedPath = _fixture.Create<Path>();
            var sut = new FileLocationProvider(null, expectedPath, null, null);

            // Act
            Path result = sut.GetTestRepositoryPath();

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(expectedPath);
        }

        [TestMethod]
        public void GetSpreadsheetDirectoryPath_PreviousSetPath_ReturnsPath()
        {
            // Arrange
            Path expectedPath = _fixture.Create<Path>();
            var sut = new FileLocationProvider(null, null, expectedPath, null);

            // Act
            Path result = sut.GetSpreadsheetDirectoryPath();

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(expectedPath);
        }

        [TestMethod]
        public void GetClassesDirectoryPath_PreviousSetPath_ReturnsPath()
        {
            // Arrange
            Path expectedPath = _fixture.Create<Path>();
            var sut = new FileLocationProvider(null, null, null, expectedPath);

            // Act
            Path result = sut.GetClassesDirectoryPath();

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(expectedPath);
        }
    }
}
