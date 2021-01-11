using AutoFixture;
using Cifra.Application.Models.Class;
using Cifra.FileSystem.FileSystemInfo;
using Cifra.FileSystem.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Cifra.FileSystem.UnitTests.Repositories
{
    [TestClass]
    public class TestsRepositoryTests
    {
        private Fixture _fixture;
        private Mock<IFileLocationProvider> _fileLocationProvider;
        private Mock<IFileInfoWrapperFactory> _fileInfoWrapperFactory;
        private ClassRepository _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _fileLocationProvider = new Mock<IFileLocationProvider>();
            _fileInfoWrapperFactory = new Mock<IFileInfoWrapperFactory>();

            _sut = new ClassRepository(_fileLocationProvider.Object, _fileInfoWrapperFactory.Object);
        }
        [TestMethod]
        public void CreateAsync_WithValidClass_AddClassToRepository()
        {
            // Arrange
            Class input = _fixture.Create<Class>();

            // Act
            _sut.CreateAsync(input);

            // Assert
        }
    }
}
